using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

namespace Server
{
    class Serv
    {
        static private int userCounter = 0;
        static private int status = 0;
        static private string command = "";

        static public void Run()
        {
            SqlCommander.ConnectToDatabase();

            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(WorkSQL.GetIP()), WorkSQL.GetPort());
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listenSocket.Bind(ipPoint);
                listenSocket.Listen(10);

                Console.WriteLine("Сервер запущен и ожидает подключения...");
                Console.WriteLine("Параметры запуска: Порт: " + WorkSQL.GetPort());
                Console.WriteLine("IP адрес:" + WorkSQL.GetIP());

                Trainer trainer = new Trainer();
                Abonement Abonement = new Abonement();
                Client client = new Client();
                LogIn logIn = new LogIn();
                Expert expert = new Expert();
                Clear(trainer, Abonement, client, logIn, expert);


                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    byte[] data = new byte[256];
                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);
                    if (builder.ToString() == "Клиент подключён")
                    {
                        userCounter++;
                        WriteStatistic();
                    }
                    if (status == 0 && ((builder.ToString() == "GET CLIENT INFO") ||
                                        (builder.ToString() == "SELECT OPERATIONS") ||
                                        (builder.ToString() == "ADD OPERATION") ||
                                        (builder.ToString() == "RED trainer ACCESS") ||
                                        (builder.ToString() == "DELETE trainer") ||
                                        (builder.ToString() == "GET MARKS") ||
                                        (builder.ToString() == "EXPERT MARKS") ||
                                        (builder.ToString() == "LOG IN") ||
                                        (builder.ToString() == "REGISTRATION") ||
                                        (builder.ToString() == "SELECT CLIENT") ||
                                        (builder.ToString() == "ADD CLIENT") ||
                                        (builder.ToString() == "DELETE CLIENT") ||
                                        (builder.ToString() == "UPDATE CLIENT") ||
                                        (builder.ToString() == "SELECT ABONEMENT") ||
                                        (builder.ToString() == "ADD ABONEMENT") ||
                                        (builder.ToString() == "DELETE ABONEMENT") ||
                                        (builder.ToString() == "UPDATE ABONEMENT") ||
                                        (builder.ToString() == "READ CLIENTS")))
                    {
                        command = builder.ToString();
                        ++status;
                    }
                    else
                    {
                        if (status == 1)
                        {
                            switch (command)
                            {
                                case "READ CLIENTS":
                                    {
                                        DataTable dataTable = SqlCommander.GetClients("ALL");
                                        byte[] responseData = GetBinaryFormatData(dataTable);
                                        handler.Send(responseData);
                                        Clear(trainer, Abonement, client, logIn, expert);
                                    }
                                    break;
                                case "GET CLIENT INFO":
                                    {
                                        client.Search = builder.ToString();
                                        DataTable dataTable = SqlCommander.GetClientInfo(client);
                                        byte[] responseData = GetBinaryFormatData(dataTable);
                                        handler.Send(responseData);
                                        Clear(trainer, Abonement, client, logIn, expert);
                                    }
                                    break;
                                case "SELECT OPERATIONS":
                                    {
                                        WorkSQL.info = builder.ToString();
                                        DataTable dataTable = SqlCommander.SelectOperations(WorkSQL.info);
                                        byte[] responseData = GetBinaryFormatData(dataTable);
                                        handler.Send(responseData);
                                        Clear(trainer, Abonement, client, logIn, expert);
                                    }
                                    break;
                                case "DELETE trainer":
                                    {
                                        trainer.ID = builder.ToString();
                                        string response = SqlCommander.DelTrainer(trainer);
                                        data = Encoding.Unicode.GetBytes(response);
                                        handler.Send(data);
                                        Clear(trainer, Abonement, client, logIn, expert);
                                    }
                                    break;
                                case "RED trainer ACCESS":
                                    //{
                                    //    trainer.ID = builder.ToString();
                                    //    string response = SqlCommander.ChangeEmplAccess(trainer.ID);
                                    //    data = Encoding.Unicode.GetBytes(response);
                                    //    handler.Send(data);
                                    //    Clear(trainer, Abonement, client, logIn, expert);
                                    //}
                                    break;
                                case "GET MARKS":
                                    //{
                                    //    expert.ExpertNum = builder.ToString();
                                    //    DataTable dataTable = SqlCommander.GetMarks(expert);
                                    //    byte[] responseData = GetBinaryFormatData(dataTable);
                                    //    handler.Send(responseData);
                                    //    Clear(trainer, Abonement, client, logIn, expert);
                                    //}
                                    break;
                                case "EXPERT MARKS":
                                    //{
                                    //    if (expert.ExpertNum == "")
                                    //    {
                                    //        expert.ExpertNum = builder.ToString();
                                    //    }
                                    //    else
                                    //    {
                                    //        if (expert.Mark1_2 == "")
                                    //        {
                                    //            expert.Mark1_2 = builder.ToString();
                                    //        }
                                    //        else
                                    //        {
                                    //            if (expert.Mark1_3 == "")
                                    //            {
                                    //                expert.Mark1_3 = builder.ToString();
                                    //            }
                                    //            else
                                    //            {
                                    //                if (expert.Mark1_4 == "")
                                    //                {
                                    //                    expert.Mark1_4 = builder.ToString();
                                    //                }
                                    //                else
                                    //                {
                                    //                    if (expert.Mark2_3 == "")
                                    //                    {
                                    //                        expert.Mark2_3 = builder.ToString();
                                    //                    }
                                    //                    else
                                    //                    {
                                    //                        if (expert.Mark2_4 == "")
                                    //                        {
                                    //                            expert.Mark2_4 = builder.ToString();
                                    //                        }
                                    //                        else
                                    //                        {
                                    //                            expert.Mark3_4 = builder.ToString();
                                    //                            string response = SqlCommander.ExpertMark(expert);
                                    //                            data = Encoding.Unicode.GetBytes(response);
                                    //                            handler.Send(data);
                                    //                            Clear(trainer, Abonement, client, logIn, expert);
                                    //                        }
                                    //                    }
                                    //                }
                                    //            }
                                    //        }
                                    //    }
                                    //}
                                    break;
                                case "LOG IN":
                                    {
                                        if (client.Login == "")
                                        {
                                            client.Login = builder.ToString();
                                        }
                                        else
                                        {
                                            client.Password = builder.ToString();
                                            string response = SqlCommander.LogIn(client);
                                            data = Encoding.Unicode.GetBytes(response);
                                            handler.Send(data);
                                            Clear(trainer, Abonement, client, logIn, expert);
                                        }
                                    }
                                    break;
                                case "REGISTRATION":
                                    {
                                        if (client.Login == "")
                                        {
                                            client.Login = builder.ToString();
                                        }
                                        else
                                        {
                                            if (client.Password == "")
                                            {
                                                client.Password = builder.ToString();
                                            }
                                            else
                                            {
                                                if (client.Surname == "")
                                                {
                                                    client.Surname = builder.ToString();
                                                }
                                                else
                                                {
                                                    if (client.Name == "")
                                                    {
                                                        client.Name = builder.ToString();
                                                    }
                                                    else
                                                    {
                                                        if (client.Thirdname == "")
                                                        {
                                                            client.Thirdname = builder.ToString();
                                                        }
                                                        else
                                                        {
                                                            if (client.Email == "")
                                                            {
                                                                client.Email = builder.ToString();
                                                            }
                                                            else
                                                            {
                                                                
                                                                            if (client.Gender == "")
                                                                            {
                                                                                client.Gender = builder.ToString();
                                                                            }
                                                                            else
                                                                            {
                                                                                if (client.Age == "")
                                                                                {
                                                                                    client.Age = builder.ToString();
                                                                                }
                                                                                else
                                                                                {
                                                                                    client.Access = builder.ToString();
                                                                                    string response = SqlCommander.Registration(client);
                                                                                    data = Encoding.Unicode.GetBytes(response);
                                                                                    handler.Send(data);
                                                                                    Clear(trainer, Abonement, client, logIn, expert);
                                                                                }
                                                                            }
                                                                        
                                                                    
                                                                
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case "SELECT CLIENT":
                                    {
                                        client.Search = builder.ToString();
                                        DataTable dataTable = SqlCommander.SelectClient(client);
                                        byte[] responseData = GetBinaryFormatData(dataTable);
                                        handler.Send(responseData);
                                        Clear(trainer, Abonement, client, logIn, expert);
                                    }
                                    break;
                                case "ADD CLIENT":
                                    {
                                        if (client.Name == "")
                                        {
                                            client.Name = builder.ToString();
                                        }
                                        else
                                        {
                                            if (client.Surname == "")
                                            {
                                                client.Surname = builder.ToString();
                                            }
                                            else
                                            {
                                                if (client.Thirdname == "")
                                                {
                                                    client.Thirdname = builder.ToString();
                                                }
                                                else
                                                {
                                                    if (client.Gender == "")
                                                    {
                                                        client.Gender = builder.ToString();
                                                    }
                                                    else
                                                    {
                                                        if (client.Email == "")
                                                        {
                                                            client.Email = builder.ToString();
                                                        }
                                                        else
                                                        {
                                                            
                                                                        client.Age = builder.ToString();
                                               
                                                                       
                                                                        string response = SqlCommander.AddClient(client);
                                                                        data = Encoding.Unicode.GetBytes(response);
                                                                        handler.Send(data);
                                                                        Clear(trainer, Abonement, client, logIn, expert);
                                                                   
                                                                
                                                            
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case "DELETE CLIENT":
                                    {
                                        client.ID = builder.ToString();
                                        string response = SqlCommander.DelClient(client);
                                        data = Encoding.Unicode.GetBytes(response);
                                        handler.Send(data);
                                        Clear(trainer, Abonement, client, logIn, expert);
                                    }
                                    break;
                                case "UPDATE CLIENT":
                                    {
                                        if (client.ID == "")
                                        {
                                            client.ID = builder.ToString();
                                        }
                                        else
                                        {
                                            if (client.Name == "")
                                            {
                                                client.Name = builder.ToString();
                                            }
                                            else
                                            {
                                                if (client.Surname == "")
                                                {
                                                    client.Surname = builder.ToString();
                                                }
                                                else
                                                {
                                                    if (client.Thirdname == "")
                                                    {
                                                        client.Thirdname = builder.ToString();
                                                    }
                                                    else
                                                    {
                                                        if (client.Gender == "")
                                                        {
                                                            client.Gender = builder.ToString();
                                                        }
                                                        else
                                                        {
                                                            if (client.Email == "")
                                                            {
                                                                client.Email = builder.ToString();
                                                            }
                                                            else
                                                            {
                                                                if (client.Age == "")
                                                                {
                                                                    client.Age = builder.ToString();
                                                                    string response = SqlCommander.ChangeClient(client);
                                                                    data = Encoding.Unicode.GetBytes(response);
                                                                    handler.Send(data);
                                                                    Clear(trainer, Abonement, client, logIn, expert);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case "SELECT Abonement":
                                    {
                                        Abonement.ID = builder.ToString();
                                        DataTable dataTable = SqlCommander.SelectAbonement(Abonement);
                                        byte[] responseData = GetBinaryFormatData(dataTable);
                                        handler.Send(responseData);
                                        Clear(trainer, Abonement, client, logIn, expert);
                                    }
                                    break;
                                case "ADD Abonement":
                                    {
                                        if (Abonement.Term == "")
                                        {
                                            Abonement.Term = builder.ToString();
                                        }
                                        else
                                        {
                                            if (Abonement.TypeOfTraining == "")
                                            {
                                                Abonement.TypeOfTraining = builder.ToString();
                                            }
                                            else
                                            {
                                                if (Abonement.Price == "")
                                                {
                                                    Abonement.Price = builder.ToString();
                                                }
                                                else
                                                {
                                                    if (Abonement.CountOfAttendents == "")
                                                    {
                                                        Abonement.CountOfAttendents = builder.ToString();
                                                        string response = SqlCommander.AddAbonement(Abonement);
                                                        data = Encoding.Unicode.GetBytes(response);
                                                        handler.Send(data);
                                                        Clear(trainer, Abonement, client, logIn, expert);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case "DELETE ABONEMENT":
                                    {
                                        Abonement.ID = builder.ToString();
                                        string response = SqlCommander.DelAbonement(Abonement.ID);
                                        data = Encoding.Unicode.GetBytes(response);
                                        handler.Send(data);
                                        Clear(trainer, Abonement, client, logIn, expert);
                                    }
                                    break;
                                case "UPDATE ABONEMENT":
                                    {
                                        if (Abonement.ID == "")
                                        {
                                            Abonement.ID = builder.ToString();
                                        }
                                        else
                                        {
                                            if (Abonement.Term == "")
                                            {
                                                Abonement.Term = builder.ToString();
                                            }
                                            else
                                            {
                                                if (Abonement.TypeOfTraining == "")
                                                {
                                                    Abonement.TypeOfTraining = builder.ToString();
                                                }
                                                else
                                                {
                                                    if (Abonement.CountOfAttendents == "")
                                                    {
                                                        Abonement.CountOfAttendents= builder.ToString();
                                                    }
                                                    else
                                                    {
                                                        if (Abonement.Price == "")
                                                        {
                                                            Abonement.Price = builder.ToString();
                                                            string response = SqlCommander.ChangeAbonement(Abonement);
                                                            data = Encoding.Unicode.GetBytes(response);
                                                            handler.Send(data);
                                                            Clear(trainer, Abonement, client, logIn, expert);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine(builder.ToString());
                            string message = "Ваше сообщение доставлено";
                            data = Encoding.Unicode.GetBytes(message);
                            handler.Send(data);
                        }
                    }
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Serialize DataTable for Select
        static byte[] GetBinaryFormatData(DataTable dt)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            byte[] outList = null;
            
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    formatter.Serialize(ms, dt);
                }catch (SerializationException e)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                    throw;
                }
                outList = ms.ToArray();
            }
            return outList;
        }
        static public void WriteStatistic()
        {
            Console.WriteLine("\r\n За время работы сервера количество подключенных клиентов: " + userCounter + ".\r\n");
        }
        static private void Clear(Trainer trainer, Abonement abonement, Client client, LogIn logIn, Expert expert)
        {
            abonement.Clean();
            client.Clean();
            trainer.Clean();
            logIn.Clean();
            expert.Clean();

            status = 0;
            command = "";
        }
    }
}
