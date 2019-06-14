using System;
using System.Data.SqlClient;
using System.Windows;

namespace TryFasterClient
{
    class DBConAct
    {
        public static SqlConnection sql = new SqlConnection("Data Source = 31.31.198.31; Initial Catalog = u0695785_KarateBase;" +
             "Persist Security Info = true; User ID = u0695785_KaratePatsan; Password = \"Karate228\""); //строка подключения
        public static SqlCommand cmd = new SqlCommand(string.Empty, sql);
        //Добавление названия и типа процедуры 
        private static void spConfiguration(string spName)
        {
            cmd.CommandText = spName;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        }

        public static string execScalar()
        {
            sql.Open();
            cmd.CommandType = System.Data.CommandType.Text;
            string scalar = cmd.ExecuteScalar().ToString();
            sql.Close();
            return scalar;
        }

        //Вызов процедуры добавления данных в таблице "AppUser"
        public static void AppUser_Insert(string UserLogin, string UserPass, int Role_id)
        {
            spConfiguration("AppUser_Insert");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@user_Login", UserLogin);
                cmd.Parameters.AddWithValue("@User_Pass", Crypt.GetHash(UserPass));
                cmd.Parameters.AddWithValue("@Role_Id", Role_id);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры обновления данных в таблице "AppUser"
        public static void AppUser_Update(Int32 Id_User, string Log, string Pass, int Id_Role)
        {
            spConfiguration("AppUser_Update");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_User", Id_User);
                cmd.Parameters.AddWithValue("@Log", Log);
                cmd.Parameters.AddWithValue("@Pass", Crypt.GetHash(Pass));
                cmd.Parameters.AddWithValue("@RI", Id_Role);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры удаления данных в таблице "AppUser"
        public static void AppUser_Delete(Int32 ID)
        {
            spConfiguration("AppUser_Delete");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_User", ID);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры добавления данных в таблице "Booking"
        public static void Booking_Insert(string BookingDate, string BookingTime, int BookingCount, int Client_id)
        {
            spConfiguration("Booking_Insert");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Booking_Date", BookingDate);
                cmd.Parameters.AddWithValue("@Booking_Time", BookingTime);
                cmd.Parameters.AddWithValue("@Booking_Count", BookingCount);
                cmd.Parameters.AddWithValue("@Client_Id", Client_id);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры обновления данных в таблице "Booking"
        public static void Booking_Update(Int32 Id_Booking, string BD, string BT, int BC, int CI)
        {
            spConfiguration("Booking_Update");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_Booking", Id_Booking);
                cmd.Parameters.AddWithValue("@BD", BD);
                cmd.Parameters.AddWithValue("@BT", BT);
                cmd.Parameters.AddWithValue("@BC", BC);
                cmd.Parameters.AddWithValue("@CI", CI);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры удаления данных в таблице "Booking"
        public static void Booking_Delete(Int32 ID)
        {
            spConfiguration("Booking_Delete");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_Booking", ID);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры добавления данных в таблице "Client"
        public static void Client_Insert(string Surn_Client, string Name_Client, string Mob_Num_Client, string Email_Client, int appuser_Id)
        {
            spConfiguration("Client_Insert");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Surn_Client", Surn_Client);
                cmd.Parameters.AddWithValue("@Name_Client", Name_Client);
                cmd.Parameters.AddWithValue("@Mob_Num_Client", Mob_Num_Client);
                cmd.Parameters.AddWithValue("@Email_Client", Email_Client);
                cmd.Parameters.AddWithValue("@appuser_id", appuser_Id);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры обновления данных в таблице "Client"
        public static void Client_Update(Int32 Id_Client, string SC, string NC, string MNC, string EC, int AU)
        {
            spConfiguration("Client_Update");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_Client", Id_Client);
                cmd.Parameters.AddWithValue("@SC", SC);
                cmd.Parameters.AddWithValue("@NC", NC);
                cmd.Parameters.AddWithValue("@MNC", MNC);
                cmd.Parameters.AddWithValue("@EC", EC);
                cmd.Parameters.AddWithValue("@AU", AU);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры удаления данных в таблице "Client"
        public static void Client_Delete(Int32 ID)
        {
            spConfiguration("Client_Delete");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_Client", ID);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры добавления данных в таблице "Delivery"
        public static void Delivery_Insert(string invoice_Num, string Delivery_Date, string Delivery_Time, string Delivery_Type)
        {
            spConfiguration("Delivery_Insert");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Invoice_Num", invoice_Num);
                cmd.Parameters.AddWithValue("@Delivery_Date", Delivery_Date);
                cmd.Parameters.AddWithValue("@Delivery_Time", Delivery_Time);
                cmd.Parameters.AddWithValue("@Delivery_Type", Delivery_Type);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры обновления данных в таблице "Delivery"
        public static void Delivery_Update(Int32 Id_Delivery, string IN, string DD, string DT, string DTP)
        {
            spConfiguration("Delivery_Update");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_Delivery", Id_Delivery);
                cmd.Parameters.AddWithValue("@IN", IN);
                cmd.Parameters.AddWithValue("@DD", DD);
                cmd.Parameters.AddWithValue("@DT", DT);
                cmd.Parameters.AddWithValue("@DTP", DTP);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры удаления данных в таблице "Delivery"
        public static void Delivery_Delete(Int32 ID)
        {
            spConfiguration("Delivery_Delete");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_Delivery", ID);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры добавления данных в таблице "Employee"
        public static void Employee_Insert(string Surn_Employee, string Name_Employee, string Midn_Employee, string Mob_Num_Employee, string Employee_Contr_Num, string Employee_Passport_Num, int Pos_id, int appuser_Id)
        {
            spConfiguration("Employee_Insert");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Surn_Employee", Surn_Employee);
                cmd.Parameters.AddWithValue("@Name_Employee", Name_Employee);
                cmd.Parameters.AddWithValue("@Midn_Employee", Midn_Employee);
                cmd.Parameters.AddWithValue("@Mob_Num_employee", Mob_Num_Employee);
                cmd.Parameters.AddWithValue("@Employee_Contr_Num", Employee_Contr_Num);
                cmd.Parameters.AddWithValue("@Employee_Passport_Num", Employee_Passport_Num);
                cmd.Parameters.AddWithValue("@Position_Id", Pos_id);
                cmd.Parameters.AddWithValue("@Appuser_id", appuser_Id);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры обновления данных в таблице "Employee"
        public static void Employee_Update(Int32 Id_Employee, string SE, string NE, string ME, string MNE, string ECN, string EPN, int PI, int AUI)
        {
            spConfiguration("Employee_Update");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_Employee", Id_Employee);
                cmd.Parameters.AddWithValue("@SE", SE);
                cmd.Parameters.AddWithValue("@ME", ME);
                cmd.Parameters.AddWithValue("@NE", NE);
                cmd.Parameters.AddWithValue("@MNE", MNE);
                cmd.Parameters.AddWithValue("@ECN", ECN);
                cmd.Parameters.AddWithValue("@EPN", EPN);
                cmd.Parameters.AddWithValue("@PI", PI);
                cmd.Parameters.AddWithValue("@AUI", AUI);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры удаления данных в таблице "Employee"
        public static void Employee_Delete(Int32 ID)
        {
            spConfiguration("Employee_Delete");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_Employee", ID);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры добавления данных в таблице "PaymentCheck"
        public static void PaymentCheck_Insert(string Check_Num, string Check_Date, string Check_Time, float Check_Sum, int Employee_Id)
        {
            spConfiguration("PaymentCheck_Insert");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Check_Num", Check_Num);
                cmd.Parameters.AddWithValue("@Check_Date", Check_Date);
                cmd.Parameters.AddWithValue("@Check_Time", Check_Time);
                cmd.Parameters.AddWithValue("@Check_Sum", Check_Sum);
                cmd.Parameters.AddWithValue("@Employee_Id", Employee_Id);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры обновления данных в таблице "PaymentCheck"
        public static void PaymentCheck_Update(Int32 Id_PaymentCheck, string CN, string CD, string CT, float CS, int EI)
        {
            spConfiguration("PaymentCheck_Update");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_Check", Id_PaymentCheck);
                cmd.Parameters.AddWithValue("@CN", CN);
                cmd.Parameters.AddWithValue("@CD", CD);
                cmd.Parameters.AddWithValue("@CT", CT);
                cmd.Parameters.AddWithValue("@CS", CS);
                cmd.Parameters.AddWithValue("@EI", EI);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры удаления данных в таблице "PaymentCheck"
        public static void PaymentCheck_Delete(Int32 ID)
        {
            spConfiguration("PaymentCheck_Delete");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_PaymentCheck", ID);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры добавления данных в таблице "Position"
        public static void Position_Insert(string Position_Name, float Position_Salary)
        {
            spConfiguration("Position_Insert");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Position_Name", Position_Name);
                cmd.Parameters.AddWithValue("@Position_Salary", Position_Salary);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры обновления данных в таблице "Position"
        public static void Position_Update(Int32 Id_Position, string PN, float Salary)
        {
            spConfiguration("Position_Update");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_Position", Id_Position);
                cmd.Parameters.AddWithValue("@PN", PN);
                cmd.Parameters.AddWithValue("@Salary", Salary);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры удаления данных в таблице "Position"
        public static void Position_Delete(Int32 ID)
        {
            spConfiguration("Position_Delete");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_Position", ID);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры добавления данных в таблице "Product"
        public static void Product_Insert(string Product_Name, int Product_Count, string Product_Type, int delivery_Id)
        {
            spConfiguration("Product_Insert");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Product_Name", Product_Name);
                cmd.Parameters.AddWithValue("@Product_Count", Product_Count);
                cmd.Parameters.AddWithValue("@Product_Type", Product_Type);
                cmd.Parameters.AddWithValue("@Delivery_Id", delivery_Id);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры обновления данных в таблице "Product"
        public static void Product_Update(Int32 Id_Product, int PC, string PN, string PT, int DI)
        {
            spConfiguration("Product_Update");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_Product", Id_Product);
                cmd.Parameters.AddWithValue("@PC", PC);
                cmd.Parameters.AddWithValue("@PN", PN);
                cmd.Parameters.AddWithValue("@PT", PT);
                cmd.Parameters.AddWithValue("@DI", DI);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры удаления данных в таблице "Product"
        public static void Product_Delete(Int32 ID)
        {
            spConfiguration("Product_Delete");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_Product", ID);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры добавления данных в таблице "Race_Client"
        public static void Race_Client_Insert(int race_id, int client_id)
        {
            spConfiguration("Race_Client_Insert");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Race_Id", race_id);
                cmd.Parameters.AddWithValue("@Client_Id", client_id);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры обновления данных в таблице "Race_Client"
        public static void Race_Client_Update(Int32 Id_Race_Client, int RI, int CI)
        {
            spConfiguration("Race_Client_Update");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_Race_Client", Id_Race_Client);
                cmd.Parameters.AddWithValue("@RI", RI);
                cmd.Parameters.AddWithValue("@CI", CI);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры удаления данных в таблице "Race_Client"
        public static void Race_Client_Delete(Int32 ID)
        {
            spConfiguration("Race_Client_Delete");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_RaceClient", ID);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры добавления данных в таблице "Race"
        public static void Race_Insert(string Race_Time, string Race_Date, string Race_Duration, string RaceBestTime, int employee_id)
        {
            spConfiguration("Race_Insert");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Race_Time", Race_Time);
                cmd.Parameters.AddWithValue("@Race_Date", Race_Date);
                cmd.Parameters.AddWithValue("@Race_Duration", Race_Duration);
                cmd.Parameters.AddWithValue("@Race_Best_Time", RaceBestTime);
                cmd.Parameters.AddWithValue("@Employee_Id", employee_id);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры обновления данных в таблице "Race"
        public static void Race_Update(Int32 Id_Race, string RDT, string RT, string RD, string RBT, int EI)
        {
            spConfiguration("Race_Update");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_Race", Id_Race);
                cmd.Parameters.AddWithValue("@RDT", RDT);
                cmd.Parameters.AddWithValue("@RT", RT);
                cmd.Parameters.AddWithValue("@RD", RD);
                cmd.Parameters.AddWithValue("@RBT", RBT);
                cmd.Parameters.AddWithValue("@EI", EI);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры удаления данных в таблице "Race"
        public static void Race_Delete(Int32 ID)
        {
            spConfiguration("Race_Delete");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_Race", ID);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры добавления данных в таблице "Repair"
        public static void Repair_Insert(string Repair_Date, string Repair_Detail, string Repair_Ready, int Transport_Id)
        {
            spConfiguration("Repair_Insert");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Repair_Date", Repair_Date);
                cmd.Parameters.AddWithValue("@Repair_Detail", Repair_Detail);
                cmd.Parameters.AddWithValue("@Repair_Ready", Repair_Ready);
                cmd.Parameters.AddWithValue("@Transport_Id", Transport_Id);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры обновления данных в таблице "Repair"
        public static void Repair_Update(Int32 Id_Repair, string RDT, string RD, string RR, int TI)
        {
            spConfiguration("Repair_Update");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_Repair", Id_Repair);
                cmd.Parameters.AddWithValue("@RDT", RDT);
                cmd.Parameters.AddWithValue("@RD", RD);
                cmd.Parameters.AddWithValue("@RR", RR);
                cmd.Parameters.AddWithValue("@TI", TI);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры удаления данных в таблице "Repair"
        public static void Repair_Delete(Int32 ID)
        {
            spConfiguration("Repair_Delete");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_Repair", ID);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры добавления данных в таблице "Role"
        public static void Role_Insert(string Role_Name, bool client, bool booking, bool paymentcheck, bool employee, bool delivery, bool position,
            bool product, bool transport, bool repair, bool race, bool race_client, bool appuser, bool transport_Race)
        {
            spConfiguration("Role_Insert");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Role_Name", Role_Name);
                cmd.Parameters.AddWithValue("@Client", client);
                cmd.Parameters.AddWithValue("@Booking", booking);
                cmd.Parameters.AddWithValue("@PaymentCheck", paymentcheck);
                cmd.Parameters.AddWithValue("@Employee", employee);
                cmd.Parameters.AddWithValue("@Delivery", delivery);
                cmd.Parameters.AddWithValue("@Position", position);
                cmd.Parameters.AddWithValue("@Product", product);
                cmd.Parameters.AddWithValue("@Transport", transport);
                cmd.Parameters.AddWithValue("@Repair", repair);
                cmd.Parameters.AddWithValue("@Race", race);
                cmd.Parameters.AddWithValue("@Race_Client", race_client);
                cmd.Parameters.AddWithValue("@Appuser", appuser);
                cmd.Parameters.AddWithValue("@transport_race", transport_Race);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры обновления данных в таблице "Role"
        public static void Role_Update(Int32 Id_Role, string RN, bool BK, bool CL, bool DL, bool EMP, bool PC,
            bool POS, bool PRD, bool RACE, bool RC, bool REP, bool TRNS, bool AU, bool TR)
        {
            spConfiguration("Role_Update");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_ROLE", Id_Role);
                cmd.Parameters.AddWithValue("@RN", RN);
                cmd.Parameters.AddWithValue("@BK", BK);
                cmd.Parameters.AddWithValue("@CL", CL);
                cmd.Parameters.AddWithValue("@DL", DL);
                cmd.Parameters.AddWithValue("@EMP", EMP);
                cmd.Parameters.AddWithValue("@PC", PC);
                cmd.Parameters.AddWithValue("@POS", POS);
                cmd.Parameters.AddWithValue("@PRD", PRD);
                cmd.Parameters.AddWithValue("@RACE", RACE);
                cmd.Parameters.AddWithValue("@RC", RC);
                cmd.Parameters.AddWithValue("@REP", REP);
                cmd.Parameters.AddWithValue("@TRNS", TRNS);
                cmd.Parameters.AddWithValue("@AU", AU);
                cmd.Parameters.AddWithValue("@TR", TR);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры удаления данных в таблице "Role"
        public static void Role_Delete(Int32 ID)
        {
            spConfiguration("Role_Delete");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_Role", ID);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры добавления данных в таблице "Transport"
        public static void Transport_Insert(string Transport_Name, string TP_Num, string Transport_State, string Transport_Power, int Delivery_Id)
        {
            spConfiguration("Transport_Insert");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Transport_Name", Transport_Name);
                cmd.Parameters.AddWithValue("@TP_Num", TP_Num);
                cmd.Parameters.AddWithValue("@Transport_State", Transport_State);
                cmd.Parameters.AddWithValue("@Transport_Power", Transport_Power);
                cmd.Parameters.AddWithValue("@Delivery_id", Delivery_Id);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры обновления данных в таблице "Transport"
        public static void Transport_Update(Int32 Id_transport, string TN, string TPN, string TP, string TS, int DI)
        {
            spConfiguration("Transport_Update");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_Transport", Id_transport);
                cmd.Parameters.AddWithValue("@TN", TN);
                cmd.Parameters.AddWithValue("@TPN", TPN);
                cmd.Parameters.AddWithValue("@TP", TP);
                cmd.Parameters.AddWithValue("@TS", TS);
                cmd.Parameters.AddWithValue("@DI", DI);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры удаления данных в таблице "Tranposrt"
        public static void Transport_Delete(Int32 ID)
        {
            spConfiguration("Transport_Delete");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_Transport", ID);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры добавления данных в таблице "Transport_Race"
        public static void Transport_Race_Insert(int Transport_Id, int race_id)
        {
            spConfiguration("Transport_Race_Insert");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Transport_Id", Transport_Id);
                cmd.Parameters.AddWithValue("@Race_id", race_id);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры обновления данных в таблице "Transport_Race"
        public static void Transport_Race_Update(Int32 Id_transport_Race, int TI, int RI)
        {
            spConfiguration("Transport_Race_Update");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_Transport_Race", Id_transport_Race);
                cmd.Parameters.AddWithValue("@TI", TI);
                cmd.Parameters.AddWithValue("@RI", RI);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }

        //Вызов процедуры удаления данных в таблице "Tranposrt_Race"
        public static void Transport_Race_Delete(Int32 ID)
        {
            spConfiguration("Transport_Race_Delete");
            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID_TransportRace", ID);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sql.Close();
            }
        }
    }
}
