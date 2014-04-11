using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using GalaSoft.MvvmLight.Messaging;
using Metrobit.Shell.Messages;
using Metrobit.Shell.Utils;

namespace Metrobit.Shell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            try
            {
                // Load window placement details for previous application session from application settings
                // Note - if window was closed on a monitor that is now disconnected from the computer,
                //        SetWindowPlacement will place the window onto a visible monitor.
                WINDOWPLACEMENT wp = (WINDOWPLACEMENT)Properties.Settings.Default.WindowPlacement;
                wp.length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));
                wp.flags = 0;
                wp.showCmd = (wp.showCmd == Constants.SW_SHOWMINIMIZED ? Constants.SW_SHOWNORMAL : wp.showCmd);
                IntPtr hwnd = new WindowInteropHelper(this).Handle;
                WinAPI.SetWindowPlacement(hwnd, ref wp);
            }
            catch
            { }
        }

        // WARNING - Not fired when Application.SessionEnding is fired
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            // Persist window placement details to application settings
            WINDOWPLACEMENT wp = new WINDOWPLACEMENT();
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            WinAPI.GetWindowPlacement(hwnd, out wp);
            Properties.Settings.Default.WindowPlacement = wp;
            Properties.Settings.Default.Save();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            var msg = new ShutdownNotificationMessage();
            Messenger.Default.Send(msg);

            e.Cancel = msg.IsCancelled;
        }
    }
}
