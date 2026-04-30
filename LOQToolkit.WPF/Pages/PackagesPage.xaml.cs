using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using LOQToolkit.Lib;
using LOQToolkit.Lib.Extensions;
using LOQToolkit.Lib.PackageDownloader;
using LOQToolkit.Lib.Settings;
using LOQToolkit.Lib.System;
using LOQToolkit.Lib.Utils;
using LOQToolkit.WPF.Controls.Packages;
using LOQToolkit.WPF.Extensions;
using LOQToolkit.WPF.Resources;
using LOQToolkit.WPF.Utils;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using MenuItem = Wpf.Ui.Controls.MenuItem;

namespace LOQToolkit.WPF.Pages;

public partial class PackagesPage : IProgress<float>
{
    private readonly PackageDownloaderSettings _packageDownloaderSettings = IoCContainer.Resolve<PackageDownloaderSettings>();
    private readonly PackageDownloaderFactory _packageDownloaderFactory = IoCContainer.Resolve<PackageDownloaderFactory>();

    private IPackageDownloader? _packageDownloader;

    private CancellationTokenSource? _getPackagesTokenSource;
    private CancellationTokenSource? _batchDownloadTokenSource;

    private CancellationTokenSource? _filterDebounceCancellationTokenSource;

    private List<Package>? _packages;
    private List<Package> _visiblePackages = [];
    private bool _isFetchingPackages;
    private bool _isBatchDownloading;

    public PackagesPage()
    {
        Initialized += PackagesPage_Initialized;

        InitializeComponent();
    }

    private async void PackagesPage_Initialized(object? sender, EventArgs e)
    {
        _machineTypeTextBox.Text = (await Compatibility.GetMachineInformationAsync()).MachineType;
        _osComboBox.SetItems(Enum.GetValues<OS>(), OSExtensions.GetCurrent(), os => os.GetDisplayName());

        var downloadsFolder = KnownFolders.GetPath(KnownFolder.Downloads);
        _downloadToText.PlaceholderText = downloadsFolder;
        _downloadToText.Text = Directory.Exists(_packageDownloaderSettings.Store.DownloadPath)
            ? _packageDownloaderSettings.Store.DownloadPath
            : downloadsFolder;

        _downloadPackagesButton.IsEnabled = true;
        _cancelDownloadPackagesButton.IsEnabled = true;

        _sourcePrimaryRadio.Tag = PackageDownloaderFactory.Type.Vantage;
        _sourceSecondaryRadio.Tag = PackageDownloaderFactory.Type.PCSupport;

        UpdatePageState();
    }

    public void Report(float value) => Dispatcher.Invoke(() =>
    {
        _loader.IsIndeterminate = value < 0;
        _loader.Progress = value;
    });

    private void DownloadToText_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var location = _downloadToText.Text;

        if (!Directory.Exists(location))
            return;

        _packageDownloaderSettings.Store.DownloadPath = location;
        _packageDownloaderSettings.SynchronizeStore();
    }

    private void OpenDownloadToButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var location = GetDownloadLocation();

            if (!Directory.Exists(location))
                return;

            Process.Start("explorer", location);
        }
        catch (Exception ex)
        {
            if (Log.Instance.IsTraceEnabled)
                Log.Instance.Trace($"Failed to open download location.", ex);
        }
    }

    private void DownloadToButton_Click(object sender, RoutedEventArgs e)
    {
        using var ofd = new FolderBrowserDialog();
        ofd.InitialDirectory = _downloadToText.Text;

        if (ofd.ShowDialog() != DialogResult.OK)
            return;

        var selectedPath = ofd.SelectedPath;
        _downloadToText.Text = selectedPath;
        _packageDownloaderSettings.Store.DownloadPath = selectedPath;
        _packageDownloaderSettings.SynchronizeStore();
    }

    private async void DownloadPackagesButton_Click(object sender, RoutedEventArgs e)
    {
        if (!await ShouldInterruptDownloadsIfRunning())
            return;

        var errorOccurred = false;
        try
        {
            _isFetchingPackages = true;
            UpdatePageState();

            _downloadPackagesButton.Visibility = Visibility.Collapsed;
            _cancelDownloadPackagesButton.Visibility = Visibility.Visible;
            _loader.Visibility = Visibility.Visible;
            _loader.IsLoading = true;
            _packages = null;
            _visiblePackages = [];
            UpdateResultsSummary();

            _packagesStackPanel.Children.Clear();
            _scrollViewer.ScrollToHome();

            _filterTextBox.Text = string.Empty;
            _sortingComboBox.SelectedIndex = 2;

            var machineType = _machineTypeTextBox.Text.Trim().ToUpperInvariant();
            _machineTypeTextBox.Text = machineType;

            if (string.IsNullOrWhiteSpace(machineType) || machineType.Length != 4 ||
                !_osComboBox.TryGetSelectedItem(out OS os))
            {
                await SnackbarHelper.ShowAsync(Resource.PackagesPage_DownloadFailed_Title,
                    Resource.PackagesPage_DownloadFailed_Message);
                return;
            }

            if (_getPackagesTokenSource is not null)
                await _getPackagesTokenSource.CancelAsync();

            _getPackagesTokenSource = new();

            var token = _getPackagesTokenSource.Token;

            var packageDownloaderType = new[] { _sourcePrimaryRadio, _sourceSecondaryRadio }
                .Where(r => r.IsChecked == true)
                .Select(r => (PackageDownloaderFactory.Type)r.Tag)
                .First();

            switch (packageDownloaderType)
            {
                case PackageDownloaderFactory.Type.Vantage:
                    _onlyShowUpdatesCheckBox.Visibility = Visibility.Visible;
                    _onlyShowUpdatesCheckBox.IsChecked = _packageDownloaderSettings.Store.OnlyShowUpdates;
                    break;
                default:
                    _onlyShowUpdatesCheckBox.Visibility = Visibility.Hidden;
                    _onlyShowUpdatesCheckBox.IsChecked = false;
                    break;
            }

            _packageDownloader = _packageDownloaderFactory.GetInstance(packageDownloaderType);
            var packages = await _packageDownloader.GetPackagesAsync(machineType, os, this, token);

            _packages = packages;

            Reload();
            _operationStatusTextBlock.Text = "Packages loaded. You can download individual drivers or the full visible list.";
        }
        catch (UpdateCatalogNotFoundException ex)
        {
            if (Log.Instance.IsTraceEnabled)
                Log.Instance.Trace($"Update catalog not found.", ex);

            await SnackbarHelper.ShowAsync(Resource.PackagesPage_UpdateCatalogNotFound_Title, Resource.PackagesPage_UpdateCatalogNotFound_Message, SnackbarType.Info);

            errorOccurred = true;
        }
        catch (OperationCanceledException)
        {
            errorOccurred = true;
        }
        catch (HttpRequestException ex)
        {
            if (Log.Instance.IsTraceEnabled)
                Log.Instance.Trace($"Error occurred when downloading packages.", ex);

            await SnackbarHelper.ShowAsync(Resource.PackagesPage_Error_Title, Resource.PackagesPage_Error_CheckInternet_Message, SnackbarType.Error);

            errorOccurred = true;
        }
        catch (Exception ex)
        {
            if (Log.Instance.IsTraceEnabled)
                Log.Instance.Trace($"Error occurred when downloading packages.", ex);

            await SnackbarHelper.ShowAsync(Resource.PackagesPage_Error_Title, ex.Message, SnackbarType.Error);

            errorOccurred = true;
        }
        finally
        {
            _isFetchingPackages = false;
            _downloadPackagesButton.Visibility = Visibility.Visible;
            _cancelDownloadPackagesButton.Visibility = Visibility.Collapsed;
            _loader.IsLoading = false;
            _loader.Progress = 0;
            _loader.IsIndeterminate = true;

            if (errorOccurred)
            {
                _packagesStackPanel.Children.Clear();
                _loader.Visibility = Visibility.Collapsed;
                _loader.IsLoading = true;
                _operationStatusTextBlock.Text = "Could not load packages. Check your inputs and try again.";
            }

            UpdatePageState();
        }
    }

    private void CancelDownloadPackagesButton_Click(object sender, RoutedEventArgs e) => _getPackagesTokenSource?.Cancel();

    private async void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!await ShouldInterruptDownloadsIfRunning())
            return;

        try
        {
            if (_packages is null)
                return;

            if (_filterDebounceCancellationTokenSource is not null)
                await _filterDebounceCancellationTokenSource.CancelAsync();

            _filterDebounceCancellationTokenSource = new();

            await Task.Delay(500, _filterDebounceCancellationTokenSource.Token);

            _packagesStackPanel.Children.Clear();
            _scrollViewer.ScrollToHome();

            Reload();
        }
        catch (OperationCanceledException) { }
    }

    private async void OnlyShowUpdatesCheckBox_OnChecked(object sender, RoutedEventArgs e)
    {
        if (!await ShouldInterruptDownloadsIfRunning())
            return;

        if (_packages is null)
            return;

        _packageDownloaderSettings.Store.OnlyShowUpdates = _onlyShowUpdatesCheckBox.IsChecked ?? false;
        _packageDownloaderSettings.SynchronizeStore();

        _packagesStackPanel.Children.Clear();
        _scrollViewer.ScrollToHome();

        Reload();
        UpdatePageState();
    }

    private async void SortingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!await ShouldInterruptDownloadsIfRunning())
            return;

        if (_packages is null)
            return;

        _packagesStackPanel.Children.Clear();
        _scrollViewer.ScrollToHome();

        Reload();
        UpdatePageState();
    }

    private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
    {
        if (_packages is null)
            return;

        _filterTextBox.Text = string.Empty;
        _sortingComboBox.SelectedIndex = 2;
        _onlyShowUpdatesCheckBox.IsChecked = false;
        _scrollViewer.ScrollToHome();
        Reload();
    }

    private async void DownloadVisibleButton_Click(object sender, RoutedEventArgs e)
    {
        if (_packageDownloader is null || _visiblePackages.Count == 0)
            return;

        if (!await ShouldInterruptDownloadsIfRunning())
            return;

        var location = GetDownloadLocation();
        var completed = 0;
        var failed = 0;

        try
        {
            _isBatchDownloading = true;
            UpdatePageState();
            _operationStatusTextBlock.Text = $"Downloading {_visiblePackages.Count} package(s) to {location}";

            if (_batchDownloadTokenSource is not null)
                await _batchDownloadTokenSource.CancelAsync();

            _batchDownloadTokenSource = new();
            var token = _batchDownloadTokenSource.Token;
            var snapshot = _visiblePackages.ToList();

            foreach (var package in snapshot)
            {
                token.ThrowIfCancellationRequested();
                _operationStatusTextBlock.Text = $"Downloading {completed + failed + 1}/{snapshot.Count}: {package.FileName}";
                try
                {
                    await _packageDownloader.DownloadPackageFileAsync(package, location, this, token);
                    completed++;
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    failed++;
                    if (Log.Instance.IsTraceEnabled)
                        Log.Instance.Trace($"Batch download failed for {package.FileName}.", ex);
                }
            }

            await SnackbarHelper.ShowAsync("Batch download complete", $"Downloaded {completed} package(s), failed {failed}.");
            _operationStatusTextBlock.Text = $"Batch download finished. Downloaded {completed}, failed {failed}.";
        }
        catch (OperationCanceledException)
        {
            _operationStatusTextBlock.Text = $"Batch download canceled. Downloaded {completed}, failed {failed}.";
        }
        finally
        {
            _isBatchDownloading = false;
            _loader.Progress = 0;
            _loader.IsIndeterminate = true;
            UpdatePageState();
        }
    }

    private void CancelBatchDownloadButton_Click(object sender, RoutedEventArgs e) => _batchDownloadTokenSource?.Cancel();

    private string GetDownloadLocation()
    {
        var location = _downloadToText.Text.Trim();

        if (!Directory.Exists(location))
        {
            var downloads = KnownFolders.GetPath(KnownFolder.Downloads);
            location = downloads;
            _downloadToText.Text = downloads;
            _packageDownloaderSettings.Store.DownloadPath = downloads;
            _packageDownloaderSettings.SynchronizeStore();
        }

        return location;
    }

    private ContextMenu? GetContextMenu(Package package, IEnumerable<Package> packages)
    {
        if (_packageDownloaderSettings.Store.HiddenPackages.Contains(package.Id))
            return null;

        var hideMenuItem = new MenuItem
        {
            SymbolIcon = SymbolRegular.EyeOff24,
            Header = Resource.Hide,
        };
        hideMenuItem.Click += (_, _) =>
        {
            _packageDownloaderSettings.Store.HiddenPackages.Add(package.Id);
            _packageDownloaderSettings.SynchronizeStore();

            Reload();
        };

        var hideAllMenuItem = new MenuItem
        {
            SymbolIcon = SymbolRegular.EyeOff24,
            Header = Resource.HideAll,
        };
        hideAllMenuItem.Click += (_, _) =>
        {
            foreach (var id in packages.Select(p => p.Id))
                _packageDownloaderSettings.Store.HiddenPackages.Add(id);
            _packageDownloaderSettings.SynchronizeStore();

            Reload();
        };

        var cm = new ContextMenu();
        cm.Items.Add(hideMenuItem);
        cm.Items.Add(hideAllMenuItem);
        return cm;
    }

    private async Task<bool> ShouldInterruptDownloadsIfRunning()
    {
        if (_packagesStackPanel?.Children is null)
            return true;

        if (_packagesStackPanel.Children.ToArray().OfType<PackageControl>().Where(pc => pc.IsDownloading).IsEmpty())
            return true;

        return await MessageBoxHelper.ShowAsync(this, Resource.PackagesPage_DownloadInProgress_Title, Resource.PackagesPage_DownloadInProgress_Message);
    }

    private void Reload()
    {
        if (_packageDownloader is null)
            return;

        _packagesStackPanel.Children.Clear();

        if (_packages is null || _packages.Count == 0)
        {
            _visiblePackages = [];
            UpdateResultsSummary();
            return;
        }

        var packages = SortAndFilter(_packages);
        _visiblePackages = packages;
        UpdateResultsSummary();

        foreach (var package in packages)
        {
            var control = new PackageControl(_packageDownloader, package, GetDownloadLocation)
            {
                ContextMenu = GetContextMenu(package, packages)
            };
            _packagesStackPanel.Children.Add(control);
        }

        if (packages.IsEmpty())
        {
            var tb = new TextBlock
            {
                Text = Resource.PackagesPage_NoMatchingDownloads,
                Foreground = (SolidColorBrush)FindResource("TextFillColorSecondaryBrush"),
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new(0, 32, 0, 32),
                Focusable = true
            };
            _packagesStackPanel.Children.Add(tb);
        }

        if (_packageDownloaderSettings.Store.HiddenPackages.Count != 0)
        {
            var clearHidden = new Hyperlink
            {
                Icon = SymbolRegular.Eye24,
                Content = "Show hidden downloads",
                HorizontalAlignment = HorizontalAlignment.Right,
            };
            clearHidden.Click += (_, _) =>
            {
                _packageDownloaderSettings.Store.HiddenPackages.Clear();
                _packageDownloaderSettings.SynchronizeStore();

                Reload();
            };
            _packagesStackPanel.Children.Add(clearHidden);
        }

        UpdatePageState();
    }

    private List<Package> SortAndFilter(List<Package> packages)
    {
        var result = _sortingComboBox.SelectedIndex switch
        {
            0 => packages.OrderBy(p => p.Title),
            1 => packages.OrderBy(p => p.Category),
            2 => packages.OrderByDescending(p => p.ReleaseDate),
            _ => packages.AsEnumerable(),
        };

        result = result.Where(p => !_packageDownloaderSettings.Store.HiddenPackages.Contains(p.Id));

        if (_onlyShowUpdatesCheckBox.IsChecked ?? false)
            result = result.Where(p => p.IsUpdate);

        if (!string.IsNullOrWhiteSpace(_filterTextBox.Text))
            result = result.Where(p => p.Index.Contains(_filterTextBox.Text, StringComparison.InvariantCultureIgnoreCase));

        return result.ToList();
    }

    private void UpdateResultsSummary()
    {
        var totalCount = _packages?.Count ?? 0;
        var visibleCount = _visiblePackages.Count;
        var updateCount = _visiblePackages.Count(p => p.IsUpdate);

        _resultsSummaryTextBlock.Text = totalCount == 0
            ? "No packages loaded"
            : $"{visibleCount} visible of {totalCount} total ({updateCount} update(s))";

        if (!_isFetchingPackages && !_isBatchDownloading && totalCount > 0)
            _operationStatusTextBlock.Text = $"Ready to download to: {GetDownloadLocation()}";
    }

    private void UpdatePageState()
    {
        var hasPackages = _packages is { Count: > 0 };
        var hasVisiblePackages = _visiblePackages.Count > 0;
        var hasFilters = !string.IsNullOrWhiteSpace(_filterTextBox?.Text)
            || (_onlyShowUpdatesCheckBox?.IsChecked ?? false)
            || (_sortingComboBox?.SelectedIndex ?? 2) != 2;

        _clearFiltersButton.IsEnabled = hasPackages && hasFilters && !_isFetchingPackages && !_isBatchDownloading;
        _downloadVisibleButton.IsEnabled = hasVisiblePackages && !_isFetchingPackages && !_isBatchDownloading;
        _cancelBatchDownloadButton.Visibility = _isBatchDownloading ? Visibility.Visible : Visibility.Collapsed;
    }
}
