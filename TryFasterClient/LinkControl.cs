using System.Windows;
using System.Windows.Controls;

namespace TryFasterClient
{
    class LinkControl
    {
        public static void Link(UserControl UserControlName)//перемещение между окнами
        {
            foreach (Window window in Application.Current.Windows)//поиск окна
            {
                if (window.GetType() == typeof(MainWindow))//проверка класса окна
                {
                    (window as MainWindow).MainGrid.Children.Clear();//очистка дочернеих объектов
                    (window as MainWindow).MainGrid.Children.Add(UserControlName);//присвоение в дочерние объекты заданный юхзерконтрол
                }
            }
        }
    }
}
