using System;
using Microsoft.Win32;
using System.Windows;

namespace TryFasterClient
{
    class Registr
    {
        public static string UI = "Empty", PW = "Empty", SE = "Empty";//логин пароль и запоминанее пароля в реестре
        public static string OrganizationName = "", DirPath = "";//название организации и путь сохранения файлов
        public static double DocLM = 0, DocTM = 0, DocRM = 0, DocBM = 0;//отсутпы сохраненные в реестре

        static public void Registry_Get()//получение параметров
        {
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey key = registry.CreateSubKey("TryFaster");
            try
            {
                UI = key.GetValue("UI").ToString();
                PW = key.GetValue("PW").ToString();
                SE = key.GetValue("SE").ToString();
            }
            catch
            {
                key.SetValue("UI", String.Empty);
                key.SetValue("PW", String.Empty);
                key.SetValue("SE", String.Empty);
            }
        }

        static public void Registry_Set(string ui, string pw, string se)//установка параметров пароля
        {
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey key = registry.CreateSubKey("TryFaster");
            try
            {
                key.SetValue("UI", ui);
                key.SetValue("PW", pw);
                key.SetValue("SE", se);
                Registry_Get();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        static public void SaveEnterReg(string Login, string Password)//сохранение пароля и логина в реестре
        {
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey key = registry.CreateSubKey("TryFaster");
            try
            {
                key.SetValue("Pa", Password);
                key.SetValue("Id", Login);
                Registry_Get();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
