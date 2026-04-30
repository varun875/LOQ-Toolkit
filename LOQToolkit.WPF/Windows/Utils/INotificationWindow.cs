namespace LOQToolkit.WPF.Windows.Utils;

public interface INotificationWindow
{
    public void Show(int closeAfter);
    public void Close(bool immediate);
}
