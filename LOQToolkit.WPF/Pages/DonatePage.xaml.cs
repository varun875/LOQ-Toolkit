using System.Windows;
using LOQToolkit.WPF.Extensions;

namespace LOQToolkit.WPF.Pages;

public partial class DonatePage
{
    public DonatePage()
    {
        InitializeComponent();
    }

    private void PayPalDonateButton_Click(object sender, RoutedEventArgs e)
    {
        Constants.PayPalUri.Open();
        e.Handled = true;
    }
}
