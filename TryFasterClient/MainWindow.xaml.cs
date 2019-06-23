using System.Windows;
using System;
using System.Windows.Input;
using System.Windows.Media;

namespace TryFasterClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double w = SystemParameters.PrimaryScreenWidth;
        public MainWindow()
        {
            InitializeComponent();

            //if (File.Exists(@"F:\Licens"))
            MainGrid.Children.Add(new AuthorizControl());//создание изначального окна авторизации 
            Registr.ConfigurationGet();
            switch (Registr.BordColor)
            {
                case "Red":
                    Application.Current.MainWindow.BorderBrush = Brushes.Red;
                    break;
                case "Blue":
                    Application.Current.MainWindow.BorderBrush = Brushes.Blue;
                    break;
                case "Yellow":
                    Application.Current.MainWindow.BorderBrush = Brushes.Yellow;
                    break;
                case "Green":
                    Application.Current.MainWindow.BorderBrush = Brushes.Green;
                    break;
                case "Black":
                    Application.Current.MainWindow.BorderBrush = Brushes.Black;
                    break;
                case "White":
                    Application.Current.MainWindow.BorderBrush = Brushes.White;
                    break;
                case "Pink":
                    Application.Current.MainWindow.BorderBrush = Brushes.Pink;
                    break;
                case "Orange":
                    Application.Current.MainWindow.BorderBrush = Brushes.Orange;
                    break;
            }
            Application.Current.MainWindow.BorderThickness = new Thickness(Convert.ToDouble(Registr.BordThik));
            switch (Registr.WinRes)
            {
                case "0":
                    Application.Current.MainWindow.Width = 1440;
                    Application.Current.MainWindow.Height = 800;
                    break;
                case "1":
                    Application.Current.MainWindow.Width = 1720;
                    Application.Current.MainWindow.Height = 800;
                    break;
                case "2":
                    Application.Current.MainWindow.Width = 1920;
                    Application.Current.MainWindow.Height = 1080;
                    Application.Current.MainWindow.Top = 0;
                    Application.Current.MainWindow.Left = 0;
                    break;
            }

            //else
            //{
            //    MessageBox.Show("У вас нет лицензии!!");
            //    Close();
            //}
        }

        private void BtnClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnMax_MouseDown(object sender, MouseButtonEventArgs e)//кнопка на весь экран
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;

                PosPanel.Width = 1190;
            }
            else
            {
                WindowState = WindowState.Maximized;
                PosPanel.Width = w - 90;
            }
        }

        private void BtnMin_MouseDown(object sender, MouseButtonEventArgs e)//кнопка сворачивания
        {
            WindowState = WindowState.Minimized;
        }

        private void PosPanel_MouseDown(object sender, MouseButtonEventArgs e)//перемещение окна
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void BtnHome_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LinkControl.Link(new MenuControl());
        }

        private void BtnChangeUser_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult s = MessageBox.Show("Вы уверены, что хотите сменить пользователя?", "Смена пользователя", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (s == MessageBoxResult.Yes)
            {
                LinkControl.Link(new AuthorizControl());
                WPNav.Visibility = Visibility.Hidden;
                TBlUserInfo.Visibility = Visibility.Hidden;
            }
        }
    }
}
