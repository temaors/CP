using Microsoft.Data.SqlClient;
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
            Console.WriteLine("Поиск в таблице Клиент: " + client.ID);
            sqlCommand.CommandText = "SELECT * FROM [Clients] WHERE (ID='" + client.ID + "')";
            sqlCommand.Connection = sqlConnection;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable("Clients");
            sqlDataAdapter.Fill(dataTable);

            //Console.WriteLine("База Данных: Вывод Клиентов ");
            return dataTable;
        }
        static public DataTable SelectTrainer(Trainer trainer)
        {
            SqlCommand sqlCommand = new SqlCommand();
            Console.WriteLine("Поиск в таблице Тренеры: " + trainer.ID);
            sqlCommand.CommandText = "SELECT * FROM [Trainers] WHERE (ID='" + trainer.ID + "')";
            sqlCommand.Connection = sqlConnection;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable("Trainers");
            sqlDataAdapter.Fill(dataTable);

            //Console.WriteLine("База Данных: Вывод Клиентов ");
            return dataTable;
        }
        static public DataTable SelectAbonement(Abonement abonement)
        {
            SqlCommand sqlCommand = new SqlCommand();
            Console.WriteLine("Поиск в таблице Абонементы: " + abonement.ID);
            sqlCommand.CommandText = "SELECT * FROM [Abonements] WHERE (ID='" + abonement.ID + "')";
            sqlCommand.Connection = sqlConnection;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable("Abonements");
            sqlDataAdapter.Fill(dataTable);

            //Console.WriteLine("База Данных: Вывод Клиентов ");
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
        static public DataTable SelectAbonement()
        {
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.CommandText = "SELECT * FROM [Abonements]";
                sqlCommand.Connection = sqlConnection;
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable("Abonements");
            sqlDataAdapter.Fill(dataTable);

            Console.WriteLine("База Данных: Вывод Данных об абонементе");
            return dataTable;
        }
        static public DataTable SelectTrainer()
        {
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.CommandText = "SELECT * FROM [Trainers]";
                sqlCommand.Connection = sqlConnection;
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable("Trainers");
            sqlDataAdapter.Fill(dataTable);

            Console.WriteLine("База Данных: Вывод Данных о тренере");
            return dataTable;
        }

        static public string AddAbonement(Abonement abonement)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "INSERT into [Abonement](ID, Price, CountOfAttendings, Term, TypeOftraining)" +
                "values(@abID, @abPrice, @abCountOfAttendings, @abTerm, @abTypeOfTraining)";

            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@abID", abonement.ID);
            sqlCommand.Parameters.AddWithValue("@abPrice", int.Parse(abonement.Cost));
            sqlCommand.Parameters.AddWithValue("@abCountOfAttendings", abonement.CountOfAttendents);
            sqlCommand.Parameters.AddWithValue("@abTerm", abonement.Term);
            sqlCommand.Parameters.AddWithValue("@abTypeOfTraining", abonement.TypeOfTraining);
            try
            {
                sqlCommand.ExecuteNonQuery();
                string response = "База Данных: Добавлены данные об абонементе ";
                Console.WriteLine(response + " (ID: " + abonement.ID + ") (Тип тренировок: " + abonement.TypeOfTraining + ") (Срок:  " + abonement.Term + ") (Количество посещений: " + abonement.CountOfAttendents + " (Цена: " + abonement.Cost + ")");
                return response;
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                string response = "База Данных: Информация  об абонементе не добавлена ";
                Console.WriteLine(response);
                return response;
            }
        }

        static public string AddTrainer(Trainer trainer)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "INSERT into [Trainers](ID, Surname, Name, ThirdName, Type, Cost)" +
                "values(@trID, @trSurname, @trName, @trThirdName, @trType, @trCost)";

            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@trID", int.Parse(trainer.ID) );
            sqlCommand.Parameters.AddWithValue("@trSurname", trainer.Surname);
            sqlCommand.Parameters.AddWithValue("@trName", trainer.Name);
            sqlCommand.Parameters.AddWithValue("@trThirdName", trainer.ThirdName);
            sqlCommand.Parameters.AddWithValue("@trType", trainer.Type);
            sqlCommand.Parameters.AddWithValue("@trCost", int.Parse(trainer.Cost));
            try
            {
                sqlCommand.ExecuteNonQuery();
                string response = "База Данных: Добавлены данные о тренере ";
                //Console.WriteLine(response + " (ID: " + abonement.ID + ") (Тип тренировок: " + abonement.TypeOfTraining + ") (Срок:  " + abonement.Term + ") (Количество посещений: " + abonement.CountOfAttendents + " (Цена: " + abonement.Cost + ")");
                return response;
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                string response = "База Данных: Информация не добавлена ";
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
            sqlCommand.Parameters.AddWithValue("@abPrice", int.Parse(abonement.Cost));
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
                Console.WriteLine(response + " (ID: " + abonement.ID + ") (Тип тренировок: " + abonement.TypeOfTraining + ") (Срок:  " + abonement.Term + ") (Количество посещений: " + abonement.CountOfAttendents + " (Цена: " + abonement.Cost + ")");
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
            sqlCommand.CommandText = "DELETE FROM [Clients] WHERE Id = @Id";

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
        static public DataTable GetClients(string sqlInfo, Client client)
        {
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                Console.WriteLine("Запрос данных о пользователях.");
                switch (sqlInfo)
                {
                    case "All":
                        sqlCommand.CommandText = "SELECT * FROM [Clients]";
                        break;

                    case "ID":
                        sqlCommand.CommandText = "SELECT * FROM [Clients] Where id = '" + client.Search + "'";
                        break;

                    case "Фамилии":
                        sqlCommand.CommandText = "SELECT * FROM [Clients] Where surname = '" + client.Search + "'";
                        break;

                    case "Имени":
                        sqlCommand.CommandText = "SELECT * FROM [Clients] Where name = '" + client.Search + "'";
                        break;

                    case "Паролю":
                        sqlCommand.CommandText = "SELECT * FROM [Clients] Where password = '" + client.Search + "'";
                        break;

                    case "Логину":
                        sqlCommand.CommandText = "SELECT * FROM [Clients] Where login = '" + client.Search + "'";
                        break;

                    case "Отчеству":
                        sqlCommand.CommandText = "SELECT * FROM [Clients] Where thirdname = '" + client.Search + "'";
                        break;

                    case "Возрасту":
                        sqlCommand.CommandText = "SELECT * FROM [Clients] Where age = '" + client.Search + "'";
                        break;

                    case "Полу":
                        sqlCommand.CommandText = "SELECT * FROM [Clients] Where gender = '" + client.Search + "'";
                        break;

                    case "Email":
                        sqlCommand.CommandText = "SELECT * FROM [Clients] Where email = '" + client.Search + "'";
                        break;

                    case "Доступу":
                        sqlCommand.CommandText = "SELECT * FROM [Clients] Where access = '" + client.Search + "'";
                        break;
                    default:
                        break;
                }   
                Console.WriteLine("SELECT * FROM [Clients] Where " + sqlInfo + " = '" + client.Search + "'");

                sqlCommand.Connection = sqlConnection;
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable("Clients");
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        static public DataTable GetAbonements(string inf, Abonement abonement)
        {
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                switch (inf)
                {
                    case "All":
                        break;
                    case "ID":
                        break;
                    case "":
                        break;
                    case "Имени":
                        break;
                    case "Отчеству":
                        break;
                    case "Цене":
                        break;
                    case "Типу тренировок":
                        break;
                    default:
                        break;
                }
                if (inf == "All")
                {
                    Console.WriteLine("Запрос данных об абонементах.");
                    sqlCommand.CommandText = "SELECT * FROM [Abonements]";
                    sqlCommand.Connection = sqlConnection;
                }
                else
                {
                    Console.WriteLine("Запрос данных об абонементах.");
                    sqlCommand.CommandText = "SELECT * FROM [Abonements] Where id = '" + int.Parse(inf) + "'";
                    sqlCommand.Connection = sqlConnection;
                }
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable("Abonements");
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        static public DataTable GetTrainers(string inf, string find)
        {
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                Console.WriteLine("Запрос данных о тренерах.");
                switch (find)
                {
                    case "All":
                        
                        sqlCommand.CommandText = "SELECT * FROM [Trainers]";
                        break;
                    case "ID":
                        sqlCommand.CommandText = "SELECT * FROM [Trainers] Where id = '" + int.Parse(inf) + "'";
                        break;
                    case "Фамилии":
                        sqlCommand.CommandText = "SELECT * FROM [Trainers] Where surname = '" + inf + "'";
                        break;
                    case "Имени":
                        sqlCommand.CommandText = "SELECT * FROM [Trainers] Where name = '" + inf + "'";
                        break;
                    case "Отчеству":
                        sqlCommand.CommandText = "SELECT * FROM [Trainers] Where thirdname = '" + inf + "'";
                        break;
                    case "Стоимости":
                        sqlCommand.CommandText = "SELECT * FROM [Trainers] Where cost = '" + inf + "'";
                        break;
                    case "Спецификации":
                        sqlCommand.CommandText = "SELECT * FROM [Trainers] Where type = '" + inf + "'";
                        break;
                    default:
                        break;
                }
                sqlCommand.Connection = sqlConnection;
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable("Trainers");
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }

        static public string SetAbonement(Client client, string inf)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "UPDATE [Clients] SET abonementid = @abid Where login = '" + client.Login + "'";

            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@сlLogin",client.Login);
            sqlCommand.Parameters.AddWithValue("@abid", int.Parse(inf));

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
                    string response = "Абонемент добавлен ";
                    Console.WriteLine(response + " (Id: " + client.ID + ") (Абонемент: " + client.AbonementId + ")");

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
        static public string SetTrainer(Client client, string inf)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "UPDATE [Clients] SET trainerid = @trid Where login = '" + client.Login + "'";

            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@сlLogin", client.Login);
            sqlCommand.Parameters.AddWithValue("@trid", int.Parse(inf));

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
                    string response = "Тренер добавлен ";
                    Console.WriteLine(response + " (Id: " + client.ID + ") (Тренер: " + client.AbonementId + ")");

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
        static public string ChangeClient(Client client)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "UPDATE [Person] SET gender = @clGender, name = @clName, surname = @clSurname, thirdname = @clThirdname," +
                "email = @clEmail,age=@clAge Where ID = @clId";

            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@clId", Convert.ToDecimal(client.ID));
            sqlCommand.Parameters.AddWithValue("@clGender", client.Gender);
            sqlCommand.Parameters.AddWithValue("@clName", client.Name);
            sqlCommand.Parameters.AddWithValue("@clSurname", client.Surname);
            sqlCommand.Parameters.AddWithValue("@clThirdname", client.Thirdname);
            sqlCommand.Parameters.AddWithValue("@clEmail", client.Email);
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
                        "(Пол: " + client.Gender + ") (Email: " + client.Email + ")  (Возраст: " + client.Age + ")");

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
            sqlCommand.CommandText = "INSERT into [Clients](id, login, password, surname, name, thirdname, email, gender, age, access) " +
                "VALUES(@clID, @clLogin, @clPassword, @clSurname, @clName, @clThirdname, @clEmail, @clGender, @clAge, @clAccess)";

            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@clID", int.Parse(client.ID));
            sqlCommand.Parameters.AddWithValue("@clLogin", client.Login);
            sqlCommand.Parameters.AddWithValue("@clPassword", client.Password);
            sqlCommand.Parameters.AddWithValue("@clSurname", client.Surname);
            sqlCommand.Parameters.AddWithValue("@clName", client.Name);
            sqlCommand.Parameters.AddWithValue("@clThirdname", client.Thirdname);
            sqlCommand.Parameters.AddWithValue("@clEmail", client.Email);
            sqlCommand.Parameters.AddWithValue("@clGender", client.Gender);
            sqlCommand.Parameters.AddWithValue("@clAge", int.Parse(client.Age));
            sqlCommand.Parameters.AddWithValue("@clAccess", int.Parse(client.Access));
            

            try
            {
                sqlCommand.ExecuteNonQuery();
                string response = "База Данных: Добавление Клиента ";
                Console.WriteLine(response + " (Возраст: " + client.Age + ") (Имя клиента: " + client.Name + ") (Фамилия:  " + client.Surname + ") (Отчество: " + client.Thirdname + ") " +
                    "(Пол: " + client.Gender + ") (Email: " + client.Email + ")");
                string resp = "База данных: Успешно!";
                return resp;

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
                "thirdname, email, gender, age, access) " +
                "VALUES(@clID, @clLogin, @clPassword, @clSurname, @clName, @clThirdname, @clEmail, " +
                "@clGender, @clAge, @clAccess)";

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