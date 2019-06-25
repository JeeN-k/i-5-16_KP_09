using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace TryFasterClient
{
    /// <summary>
    /// Логика взаимодействия для MenuControl.xaml
    /// </summary>
    public partial class MenuControl : UserControl
    {
        public MenuControl()
        {
            InitializeComponent();
        }

        private void ChangeImage(Image img, string ImageUrl) //Смена картинки
        {
            img.Source = new BitmapImage(new Uri(ImageUrl, UriKind.RelativeOrAbsolute));
        }

        private void ImgTableWork_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeImage(ImgTableWork, "Resources/TableWorkText.png");
        }

        private void ImgTableWork_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeImage(ImgTableWork, "Resources/TableWorkLogo.png");
        }

        private void ImgAccount_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeImage(ImgAccount, "Resources/AccountText.png");
        }

        private void ImgAccount_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeImage(ImgAccount, "Resources/AccountLogo.png");
        }

        private void ImgTableWork_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LinkControl.Link(new TableEditControl());
        }

        private void ImgAccount_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LinkControl.Link(new AccountControl());
        }

        private void ImgDelivery_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LinkControl.Link(new DeliveryControl());
        }

        private void ImgDelivery_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeImage(ImgDelivery, "Resources/DeliveryText.png");
        }

        private void ImgDelivery_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeImage(ImgDelivery, "Resources/DeliveryLogo.png");
        }

        private void ImgBooking_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LinkControl.Link(new BookingControl());
        }

        private void ImgBooking_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeImage(ImgBooking, "Resources/BookingText.png");
        }

        private void ImgBooking_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeImage(ImgBooking, "Resources/BookingLogo.png");
        }

        private void ImgSettings_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LinkControl.Link(new SettingsControl());
        }

        private void ImgSettings_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeImage(ImgSettings, "Resources/SettingsText.png");
        }

        private void ImgSettings_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeImage(ImgSettings, "Resources/SettingsLogo.png");
        }

        private void ImgAdmin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LinkControl.Link(new AdminPanel());
        }

        private void ImgAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeImage(ImgAdmin, "Resources/AdminText.png");
        }

        private void ImgAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeImage(ImgAdmin, "Resources/AdminLogo.png");
        }

        private void UcMenu_Loaded(object sender, RoutedEventArgs e) // Проверка ролей
        {
            if (AuthorizControl.RoleName == "Механик")
            {
                ImgDelivery.Visibility = Visibility.Visible;
                //ImgRace.Visibility = Visibility.Visible;
                ImgAccount.Visibility = Visibility.Visible;
                ImgTableWork.Visibility = Visibility.Visible;
                ImgSettings.Visibility = Visibility.Visible;
            }
            else if (AuthorizControl.RoleName == "Директор")
            {
                ImgBooking.Visibility = Visibility.Visible;
                ImgDelivery.Visibility = Visibility.Visible;
                ImgSettings.Visibility = Visibility.Visible;
                ImgTableWork.Visibility = Visibility.Visible;
                //ImgRace.Visibility = Visibility.Visible;
            }
            else if (AuthorizControl.RoleName == "Кассир")
            {
                ImgBooking.Visibility = Visibility.Visible;
                ImgTableWork.Visibility = Visibility.Visible;
                ImgSettings.Visibility = Visibility.Visible;
            }
            else if (AuthorizControl.RoleName == "Администратор")
            {
                ImgTableWork.Visibility = Visibility.Visible;
                //ImgRace.Visibility = Visibility.Visible;
                ImgDelivery.Visibility = Visibility.Visible;
                ImgBooking.Visibility = Visibility.Visible;
                ImgAdmin.Visibility = Visibility.Visible;
                ImgSettings.Visibility = Visibility.Visible;
            }
            else if (AuthorizControl.RoleName == "Клиент")
            {
                ImgBooking.Visibility = Visibility.Visible;
                ImgSettings.Visibility = Visibility.Visible;
            }
        }
    }
}
