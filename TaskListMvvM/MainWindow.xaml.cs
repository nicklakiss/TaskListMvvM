using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Hardcodet.Wpf.TaskbarNotification;


namespace TaskListMvvM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            notifyIcon = new TaskbarIcon();
            notifyIcon.IconSource =
                new BitmapImage(new Uri("pack://application:,,,/TaskListMvvM;component/list.ico"));
            notifyIcon.ToolTipText = "TodoList";
            notifyIcon.TrayMouseDoubleClick += (sender, e) =>
            {
                Show();
                WindowState = WindowState.Normal;
            };
        }

        private TaskbarIcon notifyIcon;

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonHide_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Top = AppSettings.Default.WindowTop;
            Left = AppSettings.Default.WindowLeft;

        }

        private void MainWindow_OnClosed(object? sender, EventArgs e)
        {
            AppSettings.Default.WindowTop = Top;
            AppSettings.Default.WindowLeft = Left;
            AppSettings.Default.Save();
        }

        private void MainWindow_OnStateChanged(object? sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                Hide();
            else if (WindowState == WindowState.Normal) Topmost = true;
        }
    }
}
