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
using System.Text.RegularExpressions;

namespace TryFasterClient
{
    /// <summary>
    /// Логика взаимодействия для RegistrationControl.xaml
    /// </summary>
    public partial class RegistrationControl : UserControl
    {
        public RegistrationControl()
        {
            InitializeComponent();
        }

        private void TbSurnClient_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsLetter(e.Text, 0)) return;
            e.Handled = true;
        }

        private void TbLoginClient_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text, 0)) return;
            e.Handled = true;
            Regex regex = new Regex("[^A-z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            LinkControl.Link(new AuthorizControl());
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            if (tbLoginClient.Text == "" || tbPassCleint.Text == "" || tbPassCheckClient.Text == "" || tbSurnClient.Text == ""
                || tbNameClient.Text == "" || tbEmailClient.Text == "" || tbMobNum.Text == "") // если одно из поле пустое            
                MessageBox.Show("Поля не заполнены!\nЗаполните все поля!", "Поля не заполнены!");
            else
            {
                if (tbPassCleint.Text != tbPassCheckClient.Text)
                    MessageBox.Show("Пароли не совпадают!", "Повторите ввод!"); // проверка на соответствие полей паролей
                else
                {
                    Int32 logchk; // проверка на уникальность логина
                    Int32 MobChk;
                    Int32 EmailChk;
                    DBConAct.cmd.CommandText = "select count(*) from [dbo].[appuser]" +
                                "where " +
                                "(User_Login ='" + tbLoginClient.Text + "')"; // возвращает запрос на проверку занятости введенного логина                    
                    logchk = Convert.ToInt32(DBConAct.execScalar());
                    DBConAct.cmd.CommandText = "select count(*) from [dbo].[client]" +
                                "where " +
                                "(Mob_Num_Client ='" + tbMobNum.Text + "')"; // возвращает запрос на проверку занятости введенного логина                    
                    MobChk = Convert.ToInt32(DBConAct.execScalar());
                    DBConAct.cmd.CommandText = "select count(*) from [dbo].[client]" +
                                "where " +
                                "(Email_Client ='" + tbEmailClient.Text + "')"; // возвращает запрос на проверку занятости введенного логина                    
                    EmailChk = Convert.ToInt32(DBConAct.execScalar());

                    if (logchk == 1)
                        MessageBox.Show("Данный логин уже существует!", "Существующий логин!");
                    else if (MobChk == 1)
                        MessageBox.Show("Данный мобильный номер уже существует!", "Существующий номер!");
                    else if (EmailChk == 1)
                        MessageBox.Show("Данный Email уже существует!", "Существующий Email!");
                    else
                    {
                        try
                        {
                            logchk = 0;
                            DBConAct.AppUser_Insert(tbLoginClient.Text, tbPassCleint.Password, 1); // добавляет зарегестрированные данные в бд                             
                            DBConAct.cmd.CommandText = "select id_appuser from [appuser] where [User_login] = '" + tbLoginClient.Text + "'";
                            logchk = Convert.ToInt32(DBConAct.execScalar());
                            if (logchk != 0)
                            {
                                DBConAct.Client_Insert(tbSurnClient.Text, tbNameClient.Text, tbMobNum.Text, tbEmailClient.Text, logchk);
                                MessageBox.Show("Регистрация успешно завершена!", "Регистрация успешна!");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }

                    }

                }
            }
        }

        private void TbEmailClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            string patEm = @"^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$";
            if (!Regex.IsMatch(tbEmailClient.Text, patEm, RegexOptions.IgnoreCase) || tbLoginClient.Text == "" ||
                tbPassCleint.Text == "" || tbPassCheckClient.Text == "" || tbSurnClient.Text == ""
                || tbNameClient.Text == "" || tbEmailClient.Text == "" || tbMobNum.Text == "" || tbMobNum.Text[15] == '_' || tbPassCleint.Text != tbPassCheckClient.Text)
                BtnReg.IsEnabled = false;
            else BtnReg.IsEnabled = true;
        }
    }
}
