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
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : UserControl
    {

        string[] tables = new string[] {"Клиенты", "Бронирование", "Чеки", "Сотрудники", "Поставки", "Должности",
            "Товары на складе", "Транспорт", "Ремонт", "Заезд", "Заезды клиентов", "Транспорт заезда", "Пользователи"};
        public AdminPanel()
        {
            InitializeComponent();
        }

        public void ObjectsAdd() //Процедура создания объектов динамически
        {
            try
            {
                for (int i = 0; i < tables.Length; i++)
                {
                    TextBlock newLabel = new TextBlock();
                    newLabel.Text = tables[i];
                    newLabel.FontSize = 12;
                    newLabel.Visibility = Visibility.Visible;
                    newLabel.HorizontalAlignment = HorizontalAlignment.Right;
                    newLabel.VerticalAlignment = VerticalAlignment.Center;
                    Grid.SetRow(newLabel, i);
                    this.GridLabel.Children.Add(newLabel);

                    CheckBox newCB = new CheckBox();
                    newCB.Visibility = Visibility.Visible;
                    newCB.Width = 15;
                    newCB.Height = 15;
                    newCB.HorizontalAlignment = HorizontalAlignment.Left;
                    newCB.VerticalAlignment = VerticalAlignment.Center;
                    Grid.SetRow(newCB, i);
                    this.GridChckBoxes.Children.Add(newCB);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void UcAdmin_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectsAdd();
        }
    }
}
