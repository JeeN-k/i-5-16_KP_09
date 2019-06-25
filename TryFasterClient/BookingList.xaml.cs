using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;


namespace TryFasterClient
{
    /// <summary>
    /// Логика взаимодействия для BookingList.xaml
    /// </summary>
    public partial class BookingList : UserControl
    {
        string qrBooking = "select id_booking, Booking_Date as 'Дата бронирования', Surn_Client as 'Фамилия', Name_Client as  'Имя', " +
                "Booking_Time as 'Время бронирования', " +
                "Booking_Count as 'Количество людей' from booking join client on client_id = id_Client"; // запрос для вывода таблицы
        public BookingList() 
        {
            InitializeComponent();
            lblCaptionList.Content = "Список бронирований пользователя " + AuthorizControl.UserLogin;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) // На страницу бронирований
        {
            LinkControl.Link(new BookingControl());
        }

        private void ChbFutureBook_Unchecked(object sender, RoutedEventArgs e) // Изменение чекбоксов
        {
            qrBooking = "select id_booking, Booking_Date as 'Дата бронирования', Surn_Client as 'Фамилия', Name_Client as  'Имя', " +
                "Booking_Time as 'Время бронирования', " +
                "Booking_Count as 'Количество людей' from booking join client on client_id = id_Client";
            if (AuthorizControl.Role != 2)
                qrBooking += " join appuser on appuser_id = id_appuser where user_login = '" + AuthorizControl.UserLogin + "'";
            try
            {
                if (chbFutureBook.IsChecked == true && chbPastBook.IsChecked == false)
                {
                    qrBooking += " and (Booking_Date + ' ' + Booking_Time) > format(getdate(), 'dd.MM.yy hh:mm')";
                }
                else if (chbFutureBook.IsChecked == false && chbPastBook.IsChecked == true)
                {
                    qrBooking += " and (Booking_Date + ' ' + Booking_Time) < format(getdate(), 'dd.MM.yy hh:mm')";
                }
                else if (chbFutureBook.IsChecked == false && chbPastBook.IsChecked == false)
                {
                    qrBooking += " and id_booking = 0";
                }
                qrBooking += " order by (Booking_Date + ' ' + Booking_Time)  desc";
                loadToDg();
            }
            catch (Exception)
            {
                //msg here
            }
        }

        private void loadToDg() // заполненте компонента datagrid
        {
            try
            {
                DBConAct.sql.Open();
                DataTable datatbl = new DataTable();

                SqlCommand command = new SqlCommand(qrBooking, DBConAct.sql);

                datatbl.Load(command.ExecuteReader());
                dgBookList.ItemsSource = datatbl.DefaultView;
                dgBookList.Columns[0].Visibility = Visibility.Hidden;
            }
            catch (Exception)
            {
                //msg here
            }
            finally
            {
                DBConAct.sql.Close();
            }
        }

        private void DgBookList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void DgBookList_Loaded(object sender, RoutedEventArgs e) // загрузка таблицы
        {
            loadToDg();
        }
    }
}
