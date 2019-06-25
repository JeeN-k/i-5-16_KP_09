using System;
using System.Windows;
using System.Windows.Controls;

namespace TryFasterClient
{
    /// <summary>
    /// Логика взаимодействия для ConnectControl.xaml
    /// </summary>
    public partial class ConnectControl : UserControl
    {
        public ConnectControl()
        {
            InitializeComponent();
        }


        private void BtnCon_Click(object sender, RoutedEventArgs e) // Сохранения подключения
        {
            Registr.DSIP = tbServerIP.Text;
            Registr.DBUL = tbUserName.Text;
            Registr.DBUP = tbPassword.Password;
            Registr.IC = tbInitialCatalog.Text;
            DBConAct.sql.ConnectionString = "Data Source = " + Registr.DSIP + "; Initial Catalog = " + Registr.IC + "; Persist Security Info = true; " +
                    "User ID = " + Registr.DBUL + "; Password = \"" + Registr.DBUP + "\"";
            try
            {
                DBConAct.sql.Open();

                Registr.Connect_Set(Registr.DSIP, Registr.IC, Registr.DBUL, Registr.DBUP);
                MessageBox.Show("Данные подключения сохранены", "Данные покдлючены", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception)
            {
                MessageBoxResult s = MessageBox.Show("Ошибка подключения\nВсе равно сохранить данные?\n", "Ошибка подключения", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (s == MessageBoxResult.Yes)
                {
                    Registr.Connect_Set(Registr.DSIP, Registr.IC, Registr.DBUL, Registr.DBUP);
                    MessageBox.Show("Данные подключения сохранены", "Данные сохранены", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            finally
            {
                DBConAct.sql.Close();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) // К авторизации
        {
            LinkControl.Link(new AuthorizControl());
        }

        private void BtnCheckCon_Click(object sender, RoutedEventArgs e) // Проверка подключения
        {
            Registr.DSIP = tbServerIP.Text;
            Registr.DBUL = tbUserName.Text;
            Registr.DBUP = tbPassword.Password;
            Registr.IC = tbInitialCatalog.Text;
            DBConAct.sql.ConnectionString = "Data Source = " + Registr.DSIP + "; Initial Catalog = " + Registr.IC + "; Persist Security Info = true; " +
                    "User ID = " + Registr.DBUL + "; Password = \"" + Registr.DBUP + "\"";
            try
            {
                DBConAct.sql.Open();
                MessageBoxResult s = MessageBox.Show("Подключение возможно\nСохранить?", "Connection info", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (s == MessageBoxResult.Yes)
                {
                    Registr.Connect_Set(Registr.DSIP, Registr.IC, Registr.DBUL, Registr.DBUP);
                    MessageBox.Show("Данные подключения сохранены", "Данные сохранены", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка подключения к БД\nПроверьте данные или ваше подключение к интернету\n", "Ошибка подключения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                DBConAct.sql.Close();
            }
        }

        private void UcConnectToDB_Loaded(object sender, RoutedEventArgs e) // Загрузка страницы
        {
            Registr.Connect_Get();
            tbServerIP.Text = Registr.DSIP;
            tbUserName.Text = Registr.DBUL;
            tbPassword.Password = Registr.DBUP;
            tbInitialCatalog.Text = Registr.IC;
        }
    }
}
