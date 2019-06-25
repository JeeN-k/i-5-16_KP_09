using System;
using Microsoft.Win32;
using System.Windows;

namespace TryFasterClient
{
    class Registr
    {
        public static string UI = "Empty", PW = "Empty", SE = "Empty";//логин пароль и запоминанее пароля в реестре
        public static string OrganizationName = "", DirPath = "", WinRes = "", BordThik = "", BordColor = "";//название организации и путь сохранения файлов
        public static double DocLM = 0, DocTM = 0, DocRM = 0, DocBM = 0;//отсутпы сохраненные в реестре        
        public static string DSIP = "Empty", IC = "Empty", DBUL = "Empty", DBUP = "Empty";

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

        static public void Connect_Get() //Получение данных подключения
        {
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey key = registry.CreateSubKey("TryFaster");
            try
            {
                DSIP = key.GetValue("DSIP").ToString();
                IC = key.GetValue("IC").ToString();
                DBUL = key.GetValue("DBUL").ToString();
                DBUP = key.GetValue("DBUP").ToString();
            }
            catch
            {
                key.SetValue("DSIP", String.Empty);
                key.SetValue("IC", String.Empty);
                key.SetValue("DBUL", String.Empty);
                key.SetValue("DBUP", String.Empty);
            }
        }

        static public void Connect_Set(string dsip, string ic, string dbul, string dbup)//установка параметров подключения
        {
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey key = registry.CreateSubKey("TryFaster");
            try
            {
                key.SetValue("DSIP", dsip);
                key.SetValue("IC", ic);
                key.SetValue("DBUL", dbul);
                key.SetValue("DBUP", dbup);
                Registry_Get();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

        public static void ConfigurationGet()//сохранение отступов
        {
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey registryKey = registry.CreateSubKey("TryFaster");
            RegistryKey subKey = registryKey.CreateSubKey("Configuration");
            try
            {
                DirPath = subKey.GetValue("DirPath").ToString();
                DocLM = Convert.ToDouble(subKey.GetValue("DocLM").ToString());
                DocTM = Convert.ToDouble(subKey.GetValue("DocTM").ToString());
                DocRM = Convert.ToDouble(subKey.GetValue("DocRM").ToString());
                DocBM = Convert.ToDouble(subKey.GetValue("DocBM").ToString());
                WinRes = subKey.GetValue("WinRes").ToString();
                BordThik = subKey.GetValue("BordThik").ToString();
                BordColor = subKey.GetValue("BordColor").ToString();
            }
            catch
            {
                subKey.SetValue("DirPath", String.Empty);
                subKey.SetValue("DocLM", 0.0);
                subKey.SetValue("DocTM", 0.0);
                subKey.SetValue("DocRM", 0.0);
                subKey.SetValue("DocBM", 0.0);
                subKey.SetValue("WinRes", 0);
                subKey.SetValue("BordThik", 0);
                subKey.SetValue("BordColor", "Black");
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

        public static void DocumentConfigurationSet(string Path, decimal DocLM, decimal DocTM,//пусть сохранения документа
    decimal DocRM, decimal DocBM, string WinRes, string BordThik, string BordColor)
        {
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey registryKey = registry.CreateSubKey("TryFaster");
            RegistryKey subKey = registryKey.CreateSubKey("Configuration");

            subKey.SetValue("DirPath", Path);
            subKey.SetValue("DocLM", DocLM);
            subKey.SetValue("DocTM", DocTM);
            subKey.SetValue("DocRM", DocRM);
            subKey.SetValue("DocBM", DocBM);
            subKey.SetValue("WinRes", WinRes);
            subKey.SetValue("BordThik", BordThik);
            subKey.SetValue("BordColor", BordColor);
            ConfigurationGet();
        }
    }
}
