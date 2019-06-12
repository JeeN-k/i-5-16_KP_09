using System.Windows;
using System.Windows.Input;


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

        public void ImgVisibility()
        {
            BtnHome.Visibility = Visibility.Visible;
            BtnChangeUser.Visibility = Visibility.Visible;
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
