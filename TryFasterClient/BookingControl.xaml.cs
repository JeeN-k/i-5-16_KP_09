using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Threading;

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
        string docDate, docTime, docClient;

        int timeCompare; // переменная для сравнения времени
        DateTime fullDateTime; // полное время бронирования
        string[] newTimes; // массив без значений равные null

        public BookingControl()
        {
            InitializeComponent();
        }

        private void ReserveRace() // Процесс брони
        {
            try
            {
                this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate ()
                {

                    int clientId; // id клиента из таблицы бронирование            
                    int checkBook; // проверка на существующее бронирование на это время            
                    string timeHourBook = cbTimeHourBooking.SelectedValue.ToString(); // время начала бронирования
                    string timeMinBook = cbTimeMinBooking.SelectedValue.ToString(); // время начала бронирования
                    string dateBook = cbDateBooking.SelectedValue.ToString(); // дата бронирования
                    DBConAct.cmd.CommandText = "select id_client from client join [appuser] on id_appuser = [appuser_Id] where [user_login] = '" + AuthorizControl.UserLogin + "'";
                    clientId = Convert.ToInt32(DBConAct.execScalar());
                    DBConAct.cmd.CommandText = "select count(*) from booking where client_id = " + clientId.ToString() + " and booking_time = '" + timeHourBook + ":" + timeMinBook +
                        "' and booking_date = '" + dateBook + "'";
                    checkBook = Convert.ToInt32(DBConAct.execScalar());
                    if (checkBook == 0)
                    {
                        DBConAct.Booking_Insert(dateBook, timeHourBook + ":" + timeMinBook, Convert.ToInt32(cbCountPeople.SelectedValue.ToString()), clientId);
                        MessageBox.Show("Вы забронированы на дату: " + dateBook + "\nВремя: " + timeHourBook + ":" + timeMinBook, "Успешное бронирование!",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        DBConAct.cmd.CommandText = "select CONCAT(name_Client + ' ', surn_client) from client where id_client = " + clientId;
                        string clientName = DBConAct.execScalar();
                        docDate = dateBook;
                        docTime = timeHourBook + ":" + timeMinBook;
                        docClient = clientName;

                        Thread trd = new Thread(getDocs)
                        {
                            IsBackground = true,
                            Priority = ThreadPriority.Normal
                        };
                        trd.Start();
                    }
                    else MessageBox.Show("У вас уже имеется бронирование на это время!\nИзмените дату или время!", "Повторное бронирование!", MessageBoxButton.OK, MessageBoxImage.Warning);

                }));
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

        private void getDocs() // Поулчение документов
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(delegate ()
            {
                DataWord.BookDocument(docDate, docTime, cbCountPeople.SelectedValue.ToString(), docClient);
                DataPDF.BookDocument(docDate, docTime, cbCountPeople.SelectedValue.ToString(), docClient);
            }));
        }

        private void BtnReserve_Click(object sender, RoutedEventArgs e) // Запуск потока бронирования
        {
            Thread trd = new Thread(new ThreadStart(ReserveRace));
            trd.SetApartmentState(ApartmentState.STA);
            trd.Start();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) // В меню
        {
            LinkControl.Link(new MenuControl());
        }

        private void ToBookingList_Click(object sender, RoutedEventArgs e) // К листу бронирований
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

        private void UcBooking_Loaded(object sender, RoutedEventArgs e) // Загрузка  страницы
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

        private void CbDateBooking_SelectionChanged(object sender, SelectionChangedEventArgs e) // Изменение данных в комбобоксе
        {
            cbCheck();
        }
    }
}
