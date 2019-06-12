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
using System.Net;

namespace TryFasterClient
{
    /// <summary>
    /// Логика взаимодействия для AuthorizControl.xaml
    /// </summary>
    public partial class AuthorizControl : UserControl
    {

        public static int Role;
        public static string RoleName;
        public static string UserLogin;

        public AuthorizControl()
        {
            InitializeComponent();
            if (!CheckForInternetConnection())
            {
                MessageBox.Show("Не имеется интернет соединения!", "Internet connection error");
                Application.Current.Shutdown();
            }
            Registr.Registry_Get();//получение значений из реестра
            if (Registr.SE == "1")//если запоминание пароля работает
            {
                TbLogin.Text = Registr.UI;//заполнение логина и пароля
                TbPassword.Password = Registr.PW;
                cbAutoEnter.IsChecked = true;
            }
            else
                cbAutoEnter.IsChecked = false;
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private void RegBut_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LinkControl.Link(new RegistrationControl());
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DBConAct.cmd.CommandType = System.Data.CommandType.Text;
                Int32 logchk;
                // Проверка наличия учетной записи с указанным логином
                DBConAct.cmd.CommandText = "SELECT Count(*) FROM [AppUser] WHERE User_Login= '" + TbLogin.Text + "' COLLATE Cyrillic_General_CS_AS";
                logchk = Convert.ToInt32(DBConAct.execScalar());
                // Если учетная запись существует
                if (logchk > 0)
                {
                    // Проверяем в соответствии с учетной записью заданный пароль
                    DBConAct.cmd.CommandText = "SELECT Count(*) FROM [AppUser] WHERE User_Login= '" + TbLogin.Text + "' and User_Pass= '" + Crypt.GetHash(TbPassword.Password) + "' COLLATE Cyrillic_General_CS_AS";
                    int PasswordCount = Convert.ToInt32(DBConAct.execScalar());
                    // Если пароль соответствует
                    if (PasswordCount > 0)
                    {
                        DBConAct.cmd.CommandText = "SELECT role_ID from [appuser] where User_Login = '" + TbLogin.Text + "'";
                        Role = Convert.ToInt32(DBConAct.execScalar());
                        DBConAct.cmd.CommandText = "select Role_Name from role where id_role = " + Role;
                        RoleName = DBConAct.execScalar();
                        UserLogin = TbLogin.Text;
                        string se = (bool)cbAutoEnter.IsChecked ? "1" : "0";
                        Registr.Registry_Set(TbLogin.Text, TbPassword.Password, se);
                        LinkControl.Link(new MenuControl());
                        foreach (Window window in Application.Current.Windows)//поиск окна
                        {
                            if (window.GetType() == typeof(MainWindow))//проверка класса окна
                            {
                                (window as MainWindow).WPNav.Visibility = Visibility.Visible;
                                (window as MainWindow).TBlUserInfo.Visibility = Visibility.Visible;
                                (window as MainWindow).TBlUserInfo.Text = UserLogin + " (" + RoleName + ")";
                            }
                        }

                    }
                    else // Если пароль не подходит
                    {
                        lblErr.Visibility = Visibility.Visible;
                    }

                }
                else lblErr.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
            finally
            {
                DBConAct.sql.Close();
            }
        }

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnEnter_Click(sender, e);
                e.Handled = true;
            }
            else
                e.Handled = false;
        }
    }
}
