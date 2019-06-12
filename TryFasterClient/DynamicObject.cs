namespace TryFasterClient
{
    class DynamicObject
    {
        public static int count; // Переменная, используется для указания количества создаваемых динамически объектов
        public static string[] Parameters = new string[9]; // Массив, используется для заполнения значениями label
        public static int[] CheckBoxes = new int[9]; // Массив, используется для указания типа динамически созданного объекта (0 - TextBox, 1 - ComboBox)
        public static string qrTable = "", NameTable = ""; //qrTable - запрос вывода таблицы, NameTable - хранит название таблицы
        public static string[] GetFKID = new string[3]; //Запросы на получение внешних ключей из комбобоксов
        public static string[] qrCombobox = new string[3];//Запросы на вывод данных в ComboBox
        public static string[] Masked = new string[8];// Массив масок для MaskedTextBox
        public static int chckBox = 0;
        public static void SelectedTable(string SelectedTable) // Процедуры, передает параметры для создания объектов на форме динамически
        {
            switch (SelectedTable)
            {
                case "Пользователи"://✓
                    Parameters[0] = "Логин";
                    Parameters[1] = "Пароль";
                    Parameters[2] = "Код роли";
                    qrTable = "select id_appuser, User_Login as 'Логин', User_Pass as 'Пароль'," +
                        " Role_Name as 'Название роли' from AppUser" +
                        " join role on role_id = id_role";
                    CheckBoxes = new int[] { 0, 0, 1 };
                    qrCombobox = new string[] { "select [role_Name] from [role] " };
                    Masked = new string[] { "", "", "" };
                    GetFKID = new string[] { "", "", "select id_role from role where Role_Name = " };
                    NameTable = "AppUser";
                    count = 3;
                    break;
                case "Бронирование"://✓
                    Parameters[0] = "Время бронирования";
                    Parameters[1] = "Дата бронирования";
                    Parameters[2] = "Количество клиентов";
                    Parameters[3] = "Код клиента";
                    qrTable = "select id_booking, Booking_Time as 'Время бронирования', Booking_Date as 'Дата бронирования', " +
                        "Booking_Count as 'Количество людей', CONCAT(Surn_Client + ' ', Name_Client) as 'Клиент' from booking " +
                        "join client on client_id = id_client";
                    CheckBoxes = new int[] { 3, 4, 0, 1 };
                    Masked = new string[] { "99:99", "", "", "" };
                    qrCombobox = new string[] { "select  concat(Surn_Client+ ' ' ,name_client) from client" };
                    NameTable = "booking";
                    GetFKID = new string[] { "", "", "", "select id_client from [client] where concat(Surn_Client+ ' ' ,name_client) = " };
                    count = 4;
                    break;
                case "Клиенты"://✓
                    Parameters[0] = "Фамилия клиента";
                    Parameters[1] = "Имя клиента";
                    Parameters[2] = "Мобильный \n номер клиента";
                    Parameters[3] = "Email клиента";
                    Parameters[4] = "Код пользователя";
                    qrTable = "select id_client, Surn_Client as 'Фамилия', Name_Client as 'Имя', Mob_Num_Client as 'Номер телефона', " +
                        "Email_Client as 'Email', User_Login as 'Логин' from client " +
                        "join appuser on id_AppUser = AppUser_Id";
                    CheckBoxes = new int[] { 0, 0, 0, 0, 1 };
                    Masked = new string[] { "", "", "+7(000)000-00-00", "", "" };
                    qrCombobox = new string[] { "select [User_Login] from [AppUser]" };
                    NameTable = "client";
                    GetFKID = new string[] { "", "", "", "", "select id_appuser from [AppUser] where [User_Login] = " };
                    count = 5;
                    break;
                case "Поставки"://✓
                    Parameters[0] = "Номер накладной";
                    Parameters[1] = "Дата поставки";
                    Parameters[2] = "Время поставки";
                    Parameters[3] = "Тип поставки";
                    qrTable = "select id_delivery, invoice_num as 'Номер накладной', Delivery_Date as 'Дата поставки', " +
                        "Delivery_time as 'Время поставки', Delivery_type as 'Тип поставки' from Delivery";
                    CheckBoxes = new int[] { 0, 4, 3, 0 };
                    Masked = new string[] { "9999999999", "", "00:00", "" };
                    NameTable = "Delivery";
                    GetFKID = new string[] { };
                    count = 4;
                    break;
                case "Сотрудники"://✓
                    Parameters[0] = "Фамилия сотрудника";
                    Parameters[1] = "Имя сотрудника";
                    Parameters[2] = "Отчество сотрудника";
                    Parameters[3] = "Мобильный телефон \n  сотрудника";
                    Parameters[4] = "Номер трудового \n договора сотрудника";
                    Parameters[5] = "Номер и серия паспорта";
                    Parameters[6] = "Код должности";
                    Parameters[7] = "Код пользователя";
                    qrTable = "select id_employee, Surn_Employee as 'Фамилия', Name_Employee as 'Имя', Midn_Employee as 'Отчество', Mob_Num_Employee as 'Номер телефона'," +
                        "Employee_Contr_Num as 'Номер контракта', Employee_Passport_Num as 'Серия и номер паспорта', Position_Name as 'Название должности', User_Login as 'Логин' from Employee " +
                        "join position on Position_Id = id_Position " +
                        "join AppUser on AppUser_Id = id_AppUser";
                    CheckBoxes = new int[] { 0, 0, 0, 0, 0, 0, 1, 1 };
                    Masked = new string[] { "", "", "", "+7(000)000-00-00", "999999999", "9999 999999", "", "" };
                    qrCombobox = new string[] { "select [Position_Name] from [Position]", "select user_Login from appuser" };
                    NameTable = "Employee";
                    GetFKID = new string[] { "", "", "", "", "", "", "select id_position from position where position_Name = ", "select id_appuser from appuser where user_login = " };
                    count = 8;
                    break;
                case "Чеки"://✓
                    Parameters[0] = "Дата оплаты";
                    Parameters[1] = "Время оплаты";
                    Parameters[2] = "Номер чека";
                    Parameters[3] = "Сумма оплаты";
                    Parameters[4] = "Код сотрудника";
                    qrTable = "select id_paymentcheck, Check_Date as 'Дата оплаты', Check_time as 'Время оплаты', Check_Num as 'Номер чека', Check_Sum as 'Общая сумма'," +
                        " CONCAT(Surn_Employee + ' ', Name_Employee + ' ', Midn_Employee) as 'Сотрудник' from Payment_Check " +
                        "join Employee on employee_id = id_Employee";
                    CheckBoxes = new int[] { 4, 3, 0, 0, 1 };
                    qrCombobox = new string[] { "select  concat(surn_employee + ' ', name_employee+ ' ' , midn_employee) from Employee" };
                    NameTable = "Payment_Check";
                    Masked = new string[] { "", "99:99:99", "№9999999", "", "" };
                    GetFKID = new string[] { "", "", "", "", "select id_employee from Employee where CONCAT(Surn_Employee + ' ', Name_Employee + ' ', Midn_Employee) = " };
                    count = 5;
                    break;
                case "Должности"://✓
                    Parameters[0] = "Название должности";
                    Parameters[1] = "Зарплата";
                    qrTable = "select id_position, Position_Name as 'Название должности', Position_Salary as 'Зарплата' from [dbo].[Position]";
                    CheckBoxes = new int[] { 0, 0 };
                    Masked = new string[] { "", "" };
                    NameTable = "Position";
                    GetFKID = new string[] { };
                    count = 2;
                    break;
                case "Товары на складе"://✓
                    Parameters[0] = "Наименование товара";
                    Parameters[1] = "Количество товара";
                    Parameters[2] = "Тип товара";
                    Parameters[3] = "Код поставки";
                    qrTable = "select id_product, Product_Name as 'Наименование товара', Product_Count as 'Количество товара', " +
                        "Product_Type as 'Тип товара', invoice_num as 'Поставка'  from Product " +
                        "join delivery on Delivery_Id = id_Delivery";
                    CheckBoxes = new int[] { 0, 0, 0, 1 };
                    qrCombobox = new string[] { "select invoice_num from delivery" };
                    NameTable = "Product";
                    Masked = new string[] { "", "", "", "" };
                    GetFKID = new string[] { "", "", "", "select id_delivery from delivery where invoice_num = " };
                    count = 4;
                    break;
                case "Заезд"://✓
                    Parameters[0] = "Дата заезда";
                    Parameters[1] = "Время заезда";
                    Parameters[2] = "Продолжительность заезда";
                    Parameters[3] = "Лучшее время круга заезда";
                    Parameters[4] = "Ответственный сотрудник";
                    qrTable = "select id_race, Race_Date as 'Дата заезда', Race_time as 'Время начала заезда'," +
                        " Race_Duration as 'Продолжительность заезда', Race_Best_Time as 'Лучшее время заезда', " +
                        "CONCAT(Surn_Employee + ' ', Name_Employee) as 'Ответственный заезда' from Race " +
                        "join employee on id_employee = employee_Id";
                    CheckBoxes = new int[] { 4, 3, 3, 3, 1 };
                    Masked = new string[] { "", "99:99:99", "99:99", "99:99" };
                    qrCombobox = new string[] { "select concat(Surn_Employee + ' ', Name_employee) from employee" };
                    NameTable = "Employee_Type";
                    GetFKID = new string[] { "", "", "", "", "select id_employee from employee where concat(surn_employee + ' ', name_employee) = " };
                    count = 5;
                    break;
                case "Заезды клиентов"://✓
                    Parameters[0] = "Код заезда";
                    Parameters[1] = "Код клиента";
                    qrTable = "select id_race_Client, concat(Race_Date + ' ', Race_time) as 'Заезд', concat(surn_client + ' ', name_client) as 'Клиент' " +
                        " from race_client " +
                        "join race on race_id = id_race " +
                        "join client on client_id = id_client";
                    CheckBoxes = new int[] { 1, 1 };
                    Masked = new string[] { "", "" };
                    qrCombobox = new string[] { "select concat(Race_Date + ' ', Race_time) from Race", "select concat(surn_client + ' ', name_client) from client" };
                    NameTable = "Race_Client";
                    GetFKID = new string[] { "select Id_race from [race] where concat(Race_Date + ' ', Race_time) = ", "select id_client from client where concat(surn_client + ' ', name_client) = " };
                    count = 2;
                    break;
                case "Ремонт"://✓
                    Parameters[0] = "Замененная деталь";
                    Parameters[1] = "Дата ремонта";
                    Parameters[2] = "Готов к заездам?";
                    Parameters[3] = "Код транспорта";
                    qrTable = "select id_repair, Repair_Detail as 'Замененная деталь', Repair_Date as 'Дата ремонта'," +
                        " Repair_Ready as 'Готовность транспорта', Transport.Transport_Name as 'Транспорт' from Repair" +
                        " join Transport on Id_Transport = Transport_Id";
                    CheckBoxes = new int[] { 0, 4, 2, 1 };
                    Masked = new string[] { "", "", "", "" };
                    chckBox = 1;
                    qrCombobox = new string[] { "select Transport_Name from Transport" };
                    NameTable = "Repair";
                    GetFKID = new string[] { "", "", "", "select id_transport from transport where transport_Name = " };
                    count = 4;
                    break;
                case "Транспорт"://✓
                    Parameters[0] = "Наименование";
                    Parameters[1] = "ПТС";
                    Parameters[2] = "Состояние";
                    Parameters[3] = "Мощность (л.с.)";
                    Parameters[4] = "Код поставки";
                    qrTable = "select Id_Transport, Transport_Name as 'Наименование', Tp_Num as 'ПТС', transport_state as 'Состояние', " +
                        "transport_power as 'Мощность', invoice_num as 'Поставка' from Transport " +
                        "join delivery on id_delivery = delivery_id";
                    CheckBoxes = new int[] { 0, 0, 0, 0, 1 };
                    Masked = new string[] { "", "00 LL000000", "", "", "" };
                    qrCombobox = new string[] { "select invoice_num from [delivery]" };
                    NameTable = "Transport";
                    GetFKID = new string[] { "", "", "", "", "select id_delivery from [delivery] where invoice_num = " };
                    count = 5;
                    break;
                case "Транспорт заезда"://✓
                    Parameters[0] = "Код транспорта";
                    Parameters[1] = "Код заезда";
                    qrTable = "select Id_Transport_Race, Transport_Name as 'Транспорт'," +
                        " concat(race_date + ' ', race_time) as 'Заезд' from Transport_Race " +
                        "join Transport on Id_Transport = Transport_id " +
                        "join Race on race_id = id_race";
                    CheckBoxes = new int[] { 1, 1 };
                    Masked = new string[] { "", "" };
                    NameTable = "Transport_Race";
                    qrCombobox = new string[] { "select Transport_Name from transport", "select concat(race_date + ' ', race_time) from race" };
                    GetFKID = new string[] { "select id_transport from transport where transport_Name = ", "select id_race from race where concat(race_date + ' ', race_time) = " };
                    count = 2;
                    break;
            }
        }
    }
}
