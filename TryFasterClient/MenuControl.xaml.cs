﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void ChangeImage(Image img, string ImageUrl)
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
            ChangeImage(ImgBooking, "Resources/DeliveryLogo.png");
        }

        private void ImgRace_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LinkControl.Link(new RaceControl());
        }

        private void ImgRace_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeImage(ImgRace, "Resources/RaceText.png");
        }

        private void ImgRace_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeImage(ImgRace, "Resources/RaceLogo.png");
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
    }
}