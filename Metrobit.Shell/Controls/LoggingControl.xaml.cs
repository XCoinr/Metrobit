using System.Windows.Controls;
using System.Windows.Input;

namespace Metrobit.Shell.Controls
{
    /// <summary>
    /// Interaction logic for LogginControl.xaml
    /// </summary>
    public partial class LoggingControl
    {
        public LoggingControl()
        {
            InitializeComponent();
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
