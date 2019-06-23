using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media;
using System.Linq;

namespace TryFasterClient
{
    /// <summary>
    /// Логика взаимодействия для SettingsControl.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        Grid MainGrid = Application.Current.MainWindow.Content as Grid;
        double value = 0;
        string[] srcRes = new string[] { "1440x800", "1720x800", "1920x1080" };
        string[] brdColor = new string[] { "Red", "Blue", "Yellow", "Green", "Black", "White", "Pink", "Orange" };
        double Topp, Bottomm, Leftt, Rightt;
        public SettingsControl()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Registr.ConfigurationGet();
            tbPath.Text = Registr.DirPath;
            Tb1.Text = ((decimal)Registr.DocTM).ToString();
            Tb3.Text = ((decimal)Registr.DocRM).ToString();
            Tb4.Text = ((decimal)Registr.DocBM).ToString();
            Tb2.Text = ((decimal)Registr.DocLM).ToString();
            Topp = Convert.ToDouble(Tb1.Text);
            Bottomm = Convert.ToDouble(Tb4.Text);
            Leftt = Convert.ToDouble(Tb2.Text);
            Rightt = Convert.ToDouble(Tb3.Text);
            pList.Height = p1List.Height - 50;
            //pList.Width = Math.Round(p1List.Width - 690);
            pnText.Margin = new Thickness(Leftt * 10, Topp * 10, Rightt * 10, Bottomm * 10);
            cbResolution.ItemsSource = srcRes;
            cbResolution.SelectedIndex = 0;
            cbBordColor.ItemsSource = brdColor;
            string[] bordThik = new string[11];
            for (int i = 0; i < 10; i++)
                bordThik[i] = i.ToString();
            cbThikness.ItemsSource = bordThik;
            cbThikness.SelectedValue = Registr.BordThik;
            cbBordColor.SelectedValue = Registr.BordColor;
            cbResolution.SelectedValue = Registr.WinRes;

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            LinkControl.Link(new MenuControl());
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string document_default_path = "";

                switch (tbPath.Text == "")
                {
                    case (true):
                        ;
                        document_default_path = Environment.CurrentDirectory;
                        if (!Directory.Exists(document_default_path))
                            Directory.CreateDirectory(document_default_path);
                        break;
                    case (false):
                        document_default_path = tbPath.Text;
                        if (!Directory.Exists(document_default_path))
                            Directory.CreateDirectory(document_default_path);
                        break;
                }
                switch (cbBordColor.SelectedValue.ToString())
                {
                    case "Red":
                        Application.Current.MainWindow.BorderBrush = Brushes.Red;
                        break;
                    case "Blue":
                        Application.Current.MainWindow.BorderBrush = Brushes.Blue;
                        break;
                    case "Yellow":
                        Application.Current.MainWindow.BorderBrush = Brushes.Yellow;
                        break;
                    case "Green":
                        Application.Current.MainWindow.BorderBrush = Brushes.Green;
                        break;
                    case "Black":
                        Application.Current.MainWindow.BorderBrush = Brushes.Black;
                        break;
                    case "White":
                        Application.Current.MainWindow.BorderBrush = Brushes.White;
                        break;
                    case "Pink":
                        Application.Current.MainWindow.BorderBrush = Brushes.Pink;
                        break;
                    case "Orange":
                        Application.Current.MainWindow.BorderBrush = Brushes.Orange;
                        break;
                }
                Application.Current.MainWindow.BorderThickness = new Thickness(Convert.ToDouble(cbThikness.SelectedValue.ToString()));
                switch (cbResolution.SelectedIndex)
                {
                    case 0:
                        Application.Current.MainWindow.Width = 1440;
                        Application.Current.MainWindow.Height = 800;
                        break;
                    case 1:
                        Application.Current.MainWindow.Width = 1720;
                        Application.Current.MainWindow.Height = 800;
                        break;
                    case 2:
                        Application.Current.MainWindow.Width = 1920;
                        Application.Current.MainWindow.Height = 1080;
                        Application.Current.MainWindow.Top = 0;
                        Application.Current.MainWindow.Left = 0;
                        break;
                }
                Registr.DocumentConfigurationSet(document_default_path, Convert.ToDecimal(Tb2.Text), Convert.ToDecimal(Tb1.Text),
                    Convert.ToDecimal(Tb3.Text), Convert.ToDecimal(Tb4.Text), cbResolution.SelectedValue.ToString(),
                    cbThikness.SelectedValue.ToString(), cbBordColor.SelectedValue.ToString());

                MessageBox.Show("Успешно сохранено!", "Сохранено", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch { MessageBox.Show("Ошибка сохранения!"); }
        }

        private void BtBrowse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                tbPath.Text = dialog.SelectedPath + "\\Отчёты\\";
            }
        }

        private void BtnUpDown_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "btUP1":
                    value = Convert.ToDouble(Tb1.Text);
                    value = value + 0.1;
                    Topp = value;
                    Tb1.Text = value.ToString();
                    break;
                case "btUP2":
                    value = Convert.ToDouble(Tb2.Text);
                    value = value + 0.1;
                    Leftt = value;
                    Tb2.Text = value.ToString();
                    break;
                case "btUP3":
                    value = Convert.ToDouble(Tb3.Text);
                    value = value + 0.1;
                    Rightt = value;
                    Tb3.Text = value.ToString();
                    break;
                case "btUP4":
                    value = Convert.ToDouble(Tb4.Text);
                    value = value + 0.1;
                    Bottomm = value;
                    Tb4.Text = value.ToString();
                    break;
                case "btDOWN1":
                    value = Convert.ToDouble(Tb1.Text);
                    if (value > 0)
                        value = value - 0.1;
                    else value = 0;
                    Topp = value;
                    Tb1.Text = value.ToString();
                    break;
                case "btDOWN2":
                    value = Convert.ToDouble(Tb2.Text);
                    if (value > 0)
                        value = value - 0.1;
                    else value = 0;
                    Leftt = value;
                    Tb2.Text = value.ToString();
                    break;
                case "btDOWN3":
                    value = Convert.ToDouble(Tb3.Text);
                    if (value > 0)
                        value = value - 0.1;
                    else value = 0;
                    Rightt = value;
                    Tb3.Text = value.ToString();
                    break;
                case "btDOWN4":
                    value = Convert.ToDouble(Tb4.Text);
                    if (value > 0)
                        value = value - 0.1;
                    else value = 0;
                    Bottomm = value;
                    Tb4.Text = value.ToString();
                    break;
            }
            pnText.Margin = new Thickness(Leftt * 10, Topp * 10, Rightt * 10, Bottomm * 10);
        }
    }
}
