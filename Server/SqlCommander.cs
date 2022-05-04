﻿using Microsoft.Data.SqlClient;
using Server;
using System;
using System.Data;

namespace Server
{
    internal static class SqlCommander
    {
        static private SqlConnection? sqlConnection;

        static public void ConnectToDatabase()
        {
            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = @"Data Source=DESKTOP-CA37D46\SQLEXPRESS;Initial Catalog=Fitnes-center;Integrated Security=True; TrustServerCertificate=True";
            sqlConnection.Open();
        }

        static public DataTable SelectClient(Client client)
        {
            SqlCommand sqlCommand = new SqlCommand();
            if (client.Search == "@")
            {
                sqlCommand.CommandText = "SELECT * FROM [Person]";
            }
            else
            {
                Console.WriteLine("Поиск в таблице Клиент: " + client.Search);
                sqlCommand.CommandText = "SELECT * FROM [Person] WHERE (surname='" + client.Search + "')";
            }
            sqlCommand.Connection = sqlConnection;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable("Person");
            sqlDataAdapter.Fill(dataTable);

            Console.WriteLine("База Данных: Вывод Клиентов ");
            return dataTable;
        }
        static public DataTable GetClientInfo(Client client)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "SELECT * FROM [Person] WHERE (ID='" + int.Parse(client.Search) + "')";

            sqlCommand.Connection = sqlConnection;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable("Person");
            sqlDataAdapter.Fill(dataTable);

            Console.WriteLine("База Данных: Получение данных клиента по ID ");
            return dataTable;
        }
        //clientName, clientSurname, clientThirdname, clientGender, clientEmail, clientAdress, clientPhoneCode, clientPhone, clientAge
        static public DataTable SelectAbonement(Abonement abonement)
        {
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                if (abonement.Search == "@")
                {
                    sqlCommand.CommandText = "SELECT * FROM [Abonement]";
                }
                else
                {
                    Console.WriteLine("Поиск в таблице Абонементы: " + abonement.Search);
                    sqlCommand.CommandText = "SELECT * FROM [Abonement] WHERE(ID='" + abonement.Search + "') OR (Type='" + abonement.Search + "') OR (CountOfAttendings='" + abonement.Search + "')";
                }
                sqlCommand.Connection = sqlConnection;
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable("Abonement");
            sqlDataAdapter.Fill(dataTable);

            Console.WriteLine("База Данных: Вывод Данных об абонементе");
            return dataTable;
        }

        static public string AddAbonement(Abonement abonement)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "INSERT into [Abonement](ID, Price, CountOfAttendings, Term, TypeOftraining)" +
                "values(@abID, @abPrice, @abCountOfAttendings, @abTerm, @abTypeOfTraining)";

            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@abID", abonement.ID);
            sqlCommand.Parameters.AddWithValue("@abPrice", int.Parse(abonement.Price));
            sqlCommand.Parameters.AddWithValue("@abCountOfAttendings", abonement.CountOfAttendents);
            sqlCommand.Parameters.AddWithValue("@abTerm", abonement.Term);
            sqlCommand.Parameters.AddWithValue("@abTypeOfTraining", abonement.TypeOfTraining);
            try
            {
                sqlCommand.ExecuteNonQuery();
                string response = "База Данных: Добавлены данные об абонементе ";
                Console.WriteLine(response + " (ID: " + abonement.ID + ") (Тип тренировок: " + abonement.TypeOfTraining + ") (Срок:  " + abonement.Term + ") (Количество посещений: " + abonement.CountOfAttendents + " (Цена: " + abonement.Price + ")");
                return response;
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                string response = "База Данных: Информация  об абонементе не добавлена ";
                Console.WriteLine(response);
                return response;
            }
        }

        static public string ChangeAbonement(Abonement abonement)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "UPDATE [Abonement] SET Price = @abPrice, CountOfAttendings = @abCountOfAttendings, " +
                "Term = @abTerm, TypeOfTraining = @abTypeOfTraining Where ID = @abID";

            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@abID", abonement.ID);
            sqlCommand.Parameters.AddWithValue("@abPrice", int.Parse(abonement.Price));
            sqlCommand.Parameters.AddWithValue("@abCountOfAttendings", abonement.CountOfAttendents);
            sqlCommand.Parameters.AddWithValue("@abTerm", abonement.Term);
            sqlCommand.Parameters.AddWithValue("@abTraning", abonement.TypeOfTraining);

            if (sqlCommand.ExecuteNonQuery() == 0)
            {
                string response = "База Данных: Данные об абонементе не изменены ";
                Console.WriteLine(response);
                return response;
            }
            else
            {
                string response = "База Данных: Данные об абонементе изменены ";
                Console.WriteLine(response + " (ID: " + abonement.ID + ") (Тип тренировок: " + abonement.TypeOfTraining + ") (Срок:  " + abonement.Term + ") (Количество посещений: " + abonement.CountOfAttendents + " (Цена: " + abonement.Price + ")");
                return response;
            }
        }
        static public string DelAbonement(string ID)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "DELETE FROM [Abonement] WHERE ID = @abID";

            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@AbonementID", ID);
            if (sqlCommand.ExecuteNonQuery() == 0)
            {
                string response = "База Данных: Информация об абонементе не удалена ";
                Console.WriteLine(response + " " + ID);
                return response;
            }
            else
            {
                string response = "База Данных: Информация об абонементе удалена ";
                Console.WriteLine(response + " " + ID);
                return response;
            }
        }
        static public string DelTrainer(Trainer trainer)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "DELETE FROM [Trainers] WHERE ID = @id";

            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@id", trainer.ID);
            if (sqlCommand.ExecuteNonQuery() == 0)
            {
                string response = "Тренер не удален,такого тренера не существует ";
                Console.WriteLine(response + " " + trainer.ID);
                return response;
            }
            else
            {
                string response = "Тренер удален ";
                Console.WriteLine(response + " " + trainer.ID);
                return response;
            }
        }
        static public string DelClient(Client client)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "DELETE FROM [Person] WHERE Id = @Id";

            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@Id", client.ID);
            if (sqlCommand.ExecuteNonQuery() == 0)
            {
                string response = "База Данных: Клиент не удален ";
                Console.WriteLine(response + " " + client.ID);
                return response;
            }
            else
            {
                string response = "База Данных: Клиент удален ";
                Console.WriteLine(response + " " + client.ID);
                return response;
            }
        }
        //static public DataTable GetMarks(Expert expert)
        //{
        //    SqlCommand sqlCommand = new SqlCommand();
        //    try
        //    {
        //        Console.WriteLine("Получение отметок " + expert.ExpertNum + " эксперта. ");
        //        sqlCommand.CommandText = "SELECT * FROM [Expert] WHERE(expertNum='" + expert.ExpertNum + "')";

        //        sqlCommand.Connection = sqlConnection;
        //    }
        //    catch (System.Data.SqlClient.SqlException ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
        //    DataTable dataTable = new DataTable("Expert");
        //    sqlDataAdapter.Fill(dataTable);

        //    Console.WriteLine("Получение отметок экспертов");
        //    return dataTable;
        //}
        static public DataTable GetClients(string sqlInfo)
        {
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                Console.WriteLine("Запрос данных о пользователях. ");
                if (sqlInfo == "All")
                {
                    sqlCommand.CommandText = "SELECT * FROM [clients]";
                }
                else
                    sqlCommand.CommandText = "SELECT * FROM [Employees] Where access = '1'";

                sqlCommand.Connection = sqlConnection;
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable("Employees");
            sqlDataAdapter.Fill(dataTable);

            return dataTable;
        }

        static public string ChangeClient(Client client)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "UPDATE [Person] SET gender = @clGender, name = @clName, surname = @clSurname, thirdname = @clThirdname," +
                "email = @clEmail, address = @clAddress, phone_code = @clPhoneCode, phone=@clPhone, age=@clAge Where ID = @clId";

            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@clId", Convert.ToDecimal(client.ID));
            sqlCommand.Parameters.AddWithValue("@clGender", client.Gender);
            sqlCommand.Parameters.AddWithValue("@clName", client.Name);
            sqlCommand.Parameters.AddWithValue("@clSurname", client.Surname);
            sqlCommand.Parameters.AddWithValue("@clThirdname", client.Thirdname);
            sqlCommand.Parameters.AddWithValue("@clEmail", client.Email);
            sqlCommand.Parameters.AddWithValue("@clAddress", client.Adress);
            sqlCommand.Parameters.AddWithValue("@clPhoneCode", client.PhoneCode);
            sqlCommand.Parameters.AddWithValue("@clPhone", Convert.ToDecimal(client.Phone));
            sqlCommand.Parameters.AddWithValue("@clAge", Convert.ToDecimal(client.Age));

            try
            {
                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    string response = "База Данных: Данные о клиенте не изменены ";
                    Console.WriteLine(response);

                    return response;
                }
                else
                {
                    string response = "База Данных: Данные клиента изменены ";
                    Console.WriteLine(response + " (Id: " + client.ID + ") (Имя клиента: " + client.Name + ") (Фамилия:  " + client.Surname + ") (Отчество: " + client.Thirdname + ") " +
                        "(Пол: " + client.Gender + ") (Email: " + client.Email + ") (Адрес: " + client.Adress + ") (Код телефона: " + client.PhoneCode + ") (Телефон: " + client.Phone + ") (Возраст: " + client.Age + ")");

                    return response;
                }
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                string response = "rtgfhghg";
                Console.WriteLine(ex.Message);
                return response;
            }

        }
        static public string AddClient(Client client)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "INSERT into [Clients]( gender, name, surname, thirdname, email, address, phone_code, phone, age) " +
                "VALUES(@clGender, @clName, @clSurname, @clThirdname, @clEmail, @clAddress, @clPhoneCode, @clPhone,@clAge)";

            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@clName", client.Name);
            sqlCommand.Parameters.AddWithValue("@clSurname", client.Surname);
            sqlCommand.Parameters.AddWithValue("@clThirdname", client.Thirdname);
            sqlCommand.Parameters.AddWithValue("@clGender", client.Gender);
            sqlCommand.Parameters.AddWithValue("@clEmail", client.Email);
            sqlCommand.Parameters.AddWithValue("@clAddress", client.Adress);
            sqlCommand.Parameters.AddWithValue("@clPhoneCode", client.PhoneCode);
            sqlCommand.Parameters.AddWithValue("@clPhone", int.Parse(client.Phone));
            sqlCommand.Parameters.AddWithValue("@clAge", int.Parse(client.Age));

            try
            {
                sqlCommand.ExecuteNonQuery();
                string response = "База Данных: Добавление Клиента ";
                Console.WriteLine(response + " (Возраст: " + client.Age + ") (Имя клиента: " + client.Name + ") (Фамилия:  " + client.Surname + ") (Отчество: " + client.Thirdname + ") " +
                    "(Пол: " + client.Gender + ") (Email: " + client.Email + ") (Адрес: " + client.Adress + ") (Код телефона: " + client.PhoneCode + ") (Телефон: " + client.Phone + ")");
                return response;

            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                string response = "База Данных: Клиент не был добавлен, введены неверные данные!  ";
                Console.WriteLine(response);
                Console.WriteLine(ex.Message);
                return response;
            }
        }
        static public string Registration(Client client)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "INSERT into [Clients](ID, login, password, surname, name," +
                "thirdname, email, address, phone_code, phone,gender, age, access) " +
                "VALUES(@clID, @clLogin, @clPassword, @clSurname, @clName, @clThirdname, @clEmail, " +
                "@clAddress, @clPhoneCode, @clPhone, @clGender, @clAge, @clAccess)";

            sqlCommand.Connection = sqlConnection;
            Random rnd = new Random();
            client.ID = rnd.Next(1000, 9999).ToString();
            sqlCommand.Parameters.AddWithValue("@clID", int.Parse(client.ID));
            sqlCommand.Parameters.AddWithValue("@clLogin", client.Login);
            sqlCommand.Parameters.AddWithValue("@clPassword", client.Password);
            sqlCommand.Parameters.AddWithValue("@clSurname", client.Surname);
            sqlCommand.Parameters.AddWithValue("@clName", client.Name);
            sqlCommand.Parameters.AddWithValue("@clThirdname", client.Thirdname);
            sqlCommand.Parameters.AddWithValue("@clEmail", client.Email);
            sqlCommand.Parameters.AddWithValue("@clAddress", client.Adress);
            sqlCommand.Parameters.AddWithValue("@clPhoneCode", int.Parse(client.PhoneCode));
            sqlCommand.Parameters.AddWithValue("@clPhone", int.Parse(client.Phone));
            sqlCommand.Parameters.AddWithValue("@clGender", client.Gender);
            sqlCommand.Parameters.AddWithValue("@clAge", int.Parse(client.Age));
            sqlCommand.Parameters.AddWithValue("@clAccess", int.Parse(client.Access));

            try
            {
                sqlCommand.ExecuteNonQuery();
                string response = "Регистрация прошла успешно!";
                Console.WriteLine(response);
                return response;

            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                string response = "Регистрация закончилась ошибкой!  ";
                Console.WriteLine(response);
                Console.WriteLine(ex.Message);
                return response;
            }
        }
        static public string LogIn(Client client)
        {
            Console.WriteLine("Запрос на подключение пользователя: Login: " + client.Login + " Password: " + client.Password);
            string response = "FALSE";
            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.CommandText = "SELECT * FROM [Clients] WHERE (login='" + client.Login + "') AND (password='" + client.Password + "') AND (access='1')";

            sqlCommand.Connection = sqlConnection;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable("Clients");
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                Console.WriteLine("Подключен пользователь! Login: " + client.Login + " Password: " + client.Password);
                return "USER";
            }
            sqlCommand.CommandText = "SELECT * FROM [Clients] WHERE (login='" + client.Login + "') AND (password='" + client.Password + "') AND (access='2')";

            sqlCommand.Connection = sqlConnection;

            sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            dataTable = new DataTable("Clients");
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                Console.WriteLine("Подключен администратор! Login: " + client.Login + " Password: " + client.Password);
                return "ADMIN";
            }

            return response;
        }

        //static public string ExpertMark(Expert expert)
        //{
        //    SqlCommand sqlCommand = new SqlCommand();
        //    sqlCommand.CommandText = "UPDATE [Expert] SET mark1_2 = @exMark1_2, mark1_3 = @exMark1_3, " +
        //        "mark1_4 = @exMark1_4, mark2_3 = @exMark2_3, mark2_4 = @exMark2_4, mark3_4 = @exMark3_4 Where expertNum = @exNum";

        //    sqlCommand.Connection = sqlConnection;

        //    sqlCommand.Parameters.AddWithValue("@exNum", int.Parse(expert.ExpertNum));
        //    sqlCommand.Parameters.AddWithValue("@exMark1_2", int.Parse(expert.Mark1_2));
        //    sqlCommand.Parameters.AddWithValue("@exMark1_3", int.Parse(expert.Mark1_3));
        //    sqlCommand.Parameters.AddWithValue("@exMark1_4", int.Parse(expert.Mark1_4));
        //    sqlCommand.Parameters.AddWithValue("@exMark2_3", int.Parse(expert.Mark2_3));
        //    sqlCommand.Parameters.AddWithValue("@exMark2_4", int.Parse(expert.Mark2_4));
        //    sqlCommand.Parameters.AddWithValue("@exMark3_4", int.Parse(expert.Mark3_4));

        //    if (sqlCommand.ExecuteNonQuery() == 0)
        //    {
        //        string response = "Данные об оценках экспертов не изменены ";
        //        Console.WriteLine(response);
        //        return response;
        //    }
        //    else
        //    {
        //        string response = "Данные об оценках отредактированы ";
        //        Console.WriteLine(response);
        //        return response;
        //    }
        //}
        static public string ChangeEmplAccess(string id)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "UPDATE [Employees] SET access = '2' Where ID = @clId and access='1'";

            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@clId", Convert.ToDecimal(id));

            try
            {
                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    string response = " Данные о сотруднике не изменены, такого сотрудника не существует ";
                    Console.WriteLine("База Данных:" + response + "(Id: " + id + ")");

                    return response;
                }
                else
                {
                    string response = "Данные сотруднике изменены ";
                    Console.WriteLine("База Данных: " + response + " (Id: " + id + ")");

                    return response;
                }
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                string response = "Сотрудника с таким ID не существует";
                Console.WriteLine(ex.Message);
                return response;
            }

        }
        //static public string AddOperation(string clientId, string sqlInfo, string carVIN)
        //{
        //    SqlCommand sqlCommand = new SqlCommand();
        //    sqlCommand.CommandText = "INSERT into [Leasing](clientFIO, emplFIO, carBrandName) values(@clFIO, @empFIO, @crBrandName)";

        //    sqlCommand.Connection = sqlConnection;

        //    sqlCommand.Parameters.AddWithValue("@clFIO", clientId);
        //    sqlCommand.Parameters.AddWithValue("@empFIO", sqlInfo);
        //    sqlCommand.Parameters.AddWithValue("@crBrandName", carVIN);

        //    try
        //    {
        //        sqlCommand.ExecuteNonQuery();
        //        string response = "Добавлена операция лизинга ";
        //        Console.WriteLine("База Данных: " + response + "(ФИО клиента: " + clientId + ") (ФИО сотрудника: " + sqlInfo + ") (VIN автомобиля:  " + carVIN + ")");
        //        return response;
        //    }
        //    catch (Microsoft.Data.SqlClient.SqlException)
        //    {
        //        string response = "Операция лизинга не добавлена";
        //        Console.WriteLine(response);
        //        return response;
        //    }
        //}
        static public string GetClientFio(string clientID)
        {
            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.CommandText = "SELECT * FROM [Clients] WHERE (ID='" + int.Parse(clientID) + "')";

            sqlCommand.Connection = sqlConnection;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable("Clients");
            sqlDataAdapter.Fill(dataTable);

            string FIO = dataTable.Rows[0][2].ToString() + " " + dataTable.Rows[0][3].ToString() + " " + dataTable.Rows[0][4].ToString();

            return FIO;
        }
        static public string GetEmplFio(string empLogin)
        {
            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.CommandText = "SELECT * FROM [Clients] WHERE (login='" + empLogin + "')";

            sqlCommand.Connection = sqlConnection;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable("Clients");
            sqlDataAdapter.Fill(dataTable);

            string FIO = dataTable.Rows[0][3].ToString() + " " + dataTable.Rows[0][4].ToString() + " " + dataTable.Rows[0][5].ToString();

            return FIO;
        }
        
        static public DataTable SelectOperations(string sqlInfo)
        {
            SqlCommand sqlCommand = new SqlCommand();
            if (sqlInfo == "@")
            {
                sqlCommand.CommandText = "SELECT * FROM [Fitnes-center]";
            }
            else
            {
                sqlCommand.CommandText = "SELECT * FROM [Fitnes-center] Where operation_data Between '2021-" + int.Parse(sqlInfo) + "- 01' And '2021-" + int.Parse(sqlInfo) + "-30'";

            }
            sqlCommand.Connection = sqlConnection;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable("Fitnes-center");
            sqlDataAdapter.Fill(dataTable);

            Console.WriteLine("База Данных: Вывод операций фитнес-центра ");
            return dataTable;
        }

        //Get the number of rows in table
        static public int GetCount(string tableName)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "SELECT COUNT(*) FROM" + tableName;
            sqlCommand.Connection = sqlConnection;
            return (int)sqlCommand.ExecuteScalar();
        }

        static public int GetId(string tableName)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "SELECT MAX(Id) FROM " + tableName;
            sqlCommand.Connection = sqlConnection;
            object obj = sqlCommand.ExecuteScalar();
            if (obj == DBNull.Value)
            {
                return 0;
            }
            else
            {
                int id = Convert.ToInt32(sqlCommand.ExecuteScalar());
                return id;
            }
        }
    }
}