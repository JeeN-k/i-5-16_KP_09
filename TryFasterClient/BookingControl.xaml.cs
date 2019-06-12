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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TryFasterClient
{
    /// <summary>
    /// Логика взаимодействия для BookingControl.xaml
    /// </summary>
    public partial class BookingControl : UserControl
    {
        string[] timesMin = new string[] { "00", "15", "30", "45" }; // массив со временем для бронирования        
        string[] hours = new string[13];
        string[] dates = new string[] { DateTime.Now.ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"),
            DateTime.Now.ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy") }; // массив с датами для бронирования

        int timeCompare; // переменная для сравнения времени
        DateTime fullDateTime; // полное время бронирования
        string[] newTimes; // массив без значений равные null

        public BookingControl()
        {
            InitializeComponent();
        }

        private void BtnReserve_Click(object sender, RoutedEventArgs e)
        {
            int clientId; // id клиента из таблицы бронирование            
            int checkBook; // проверка на существующее бронирование на это время            
            string timeHourBook = cbTimeHourBooking.SelectedValue.ToString(); // время начала бронирования
            string timeMinBook = cbTimeMinBooking.SelectedValue.ToString(); // время начала бронирования
            string dateBook = cbDateBooking.SelectedValue.ToString(); // дата бронирования            

            try
            {
                DBConAct.cmd.CommandText = "select id_client from client join [appuser] on id_appuser = [appuser_Id] where [user_login] = '" + AuthorizControl.UserLogin + "'";
                clientId = Convert.ToInt32(DBConAct.execScalar());
                DBConAct.cmd.CommandText = "select count(*) from booking where client_id = " + clientId.ToString() + " and booking_time = '" + timeHourBook + ":" + timeMinBook +
                    "' and booking_date = '" + dateBook + "'";
                checkBook = Convert.ToInt32(DBConAct.execScalar());
                if (checkBook == 0)
                {
                    DBConAct.Booking_Insert(dateBook, timeHourBook + ":" + timeMinBook, Convert.ToInt32(cbCountPeople.SelectedValue.ToString()), clientId);
                    MessageBox.Show("Вы забронированы на дату:" + dateBook + "\nВремя: " + timeHourBook + ":" + timeMinBook, "Успешное бронирование!",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else MessageBox.Show("У вас уже имеется бронирование на это время!\nИзмените дату или время!", "Повторное бронирование!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "SqlException");
            }
            finally
            {
                DBConAct.sql.Close();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            LinkControl.Link(new MenuControl());
        }

        private void ToBookingList_Click(object sender, RoutedEventArgs e)
        {
            LinkControl.Link(new BookingList());
        }

        private string days(string arrDate, int countDays) // прибавление дней
        {
            string newDate = (DateTime.ParseExact(arrDate, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture).AddDays(countDays)).ToString("dd.MM.yyyy");
            return newDate;
        }

        private void cbCheck() // заполняет комбобоксы
        {
            for (int i = 10; i <= 22; i++)
            {
                hours[i - 10] = i.ToString();

            }
            try
            {
                for (int i = 0; hours.Length > i; i++)
                {
                    fullDateTime = DateTime.Parse(cbDateBooking.SelectedValue + " " + hours[i] + ":15");
                    timeCompare = DateTime.Compare(fullDateTime, DateTime.Now);
                    if (timeCompare <= 0)
                    {
                        hours[i] = null;
                    }
                    else break;
                }

                newTimes = (from elem in hours where elem != null select elem).ToArray();

                cbTimeMinBooking.ItemsSource = timesMin;
                cbTimeMinBooking.SelectedIndex = 0;
                cbTimeHourBooking.ItemsSource = newTimes;
                cbTimeMinBooking.SelectedIndex = 0;
                cbDateBooking.ItemsSource = dates;
                cbTimeHourBooking.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка");
            }
        }

        private void UcBooking_Loaded(object sender, RoutedEventArgs e)
        {
            lblUserName.Content = "Бронь для " + AuthorizControl.UserLogin;
            for (int i = 1; dates.Length > i; i++)
            {
                dates[i] = days(dates[i], i);
            }

            for (int i = 1; 10 >= i; i++)
            {
                cbCountPeople.Items.Add(i);
            }

            for (int i = 10; i <= 22; i++)
            {
                hours[i - 10] = i.ToString();

            }

            cbDateBooking.SelectedIndex = 0;
            cbCheck();
            cbCountPeople.SelectedIndex = 0;
            cbTimeHourBooking.SelectedIndex = 0;
            cbTimeMinBooking.SelectedIndex = 0;
        }

        private void CbDateBooking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbCheck();
        }
    }
}
