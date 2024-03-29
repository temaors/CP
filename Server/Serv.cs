﻿using System;
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
                Report report = new Report();
                
                Clear(trainer, Abonement, client, logIn, expert, report);


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
                    if (status == 0 && (
                                        (builder.ToString() == "DELETE TRAINER") ||
                                        (builder.ToString() == "LOG IN") ||
                                        (builder.ToString() == "REGISTRATION") ||
                                        (builder.ToString() == "SELECT CLIENT") ||
                                        (builder.ToString() == "ADD CLIENT") ||
                                        (builder.ToString() == "DELETE CLIENT") ||
                                        (builder.ToString() == "UPDATE CLIENT") ||
                                        (builder.ToString() == "SELECT ABONEMENT") ||
                                        (builder.ToString() == "SELECT TRAINER") ||
                                        (builder.ToString() == "ADD ABONEMENT") ||
                                        (builder.ToString() == "ADD TRAINER") ||
                                        (builder.ToString() == "DELETE ABONEMENT") ||
                                        (builder.ToString() == "UPDATE ABONEMENT") ||
                                        (builder.ToString() == "READ ABONEMENTS") ||
                                        (builder.ToString() == "READ TRAINERS") ||
                                        (builder.ToString() == "READ CLIENTS") ||
                                        (builder.ToString() == "SET ABONEMENT") ||
                                        (builder.ToString() == "SET TRAINER") ||
                                        (builder.ToString() == "READ REPORT") ||
                                        (builder.ToString() == "UPDATE REPORT") ||
                                        (builder.ToString() == "EXPERT")))
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
                                case "EXPERT":
                                    {
                                        DataTable dataTable = SqlCommander.GetTrainers("All", "All");
                                        int[] nums = new int[dataTable.Rows.Count];
                                        int[] nums2 = new int[dataTable.Rows.Count];
                                        int max = 0;
                                        for (int i = 0; i < dataTable.Rows.Count; i++)
                                        {
                                            nums[i] = int.Parse((string)dataTable.Rows[1][i]);
                                            nums2[i] = i + 1;
                                            if (nums[i] > max)
                                            {
                                                max = nums[i];
                                            }
                                        }
                                        max = max * dataTable.Rows.Count / 100;
                                        for (int i = 0; i < dataTable.Rows.Count; i++)
                                        {
                                            for (int j = 0; j < dataTable.Rows.Count; j++)
                                            {
                                                expert.matrix[i][j] = (100 - (nums[i] * nums2[j])) / max;
                                            }
                                        }
                                    }
                                    break;
                                case "READ REPORT":
                                    {
                                        if (client.Search == "")
                                        {
                                            client.Search = builder.ToString();
                                        }
                                        else
                                        {
                                            string find = builder.ToString();
                                            DataTable dataTable = SqlCommander.GetReport(find);
                                            byte[] responseData = GetBinaryFormatData(dataTable);
                                            handler.Send(responseData);
                                            Clear(trainer, Abonement, client, logIn, expert, report);
                                        }
                                    }
                                    break;
                                case "UPDATE REPORT":
                                    {
                                        if(report.TransactionID == "")
                                        {
                                            report.TransactionID = builder.ToString();
                                        }
                                        else
                                        {
                                           if (report.Cost == "")
                                            {
                                                report.Cost = builder.ToString();
                                            }
                                            else
                                            {
                                                if(report.Date == "")
                                                {
                                                    report.Date = builder.ToString();

                                                }
                                                else
                                                {
                                                    report.ClientID = builder.ToString();
                                                    string response = SqlCommander.AddReport(report);
                                                    data = Encoding.Unicode.GetBytes(response);
                                                    handler.Send(data);
                                                    Clear(trainer, Abonement, client, logIn, expert, report);
                                                }
                                            }
                                        }

                                    }
                                    break;
                                case "SET TRAINER":
                                    {
                                        if (client.Login == "")
                                        {
                                            client.Login = builder.ToString();
                                        }
                                        else
                                        {
                                            string inf = builder.ToString();
                                            string response = SqlCommander.SetTrainer(client, inf);
                                            data = Encoding.Unicode.GetBytes(response);
                                            handler.Send(data);
                                            Clear(trainer, Abonement, client, logIn, expert, report);
                                        }

                                    }
                                    break;
                                case "SET ABONEMENT":
                                    {
                                        if (client.Login == "")
                                        {
                                            client.Login = builder.ToString();
                                        }
                                        else
                                        {
                                            string inf = builder.ToString();
                                            string response = SqlCommander.SetAbonement(client, inf);
                                            data = Encoding.Unicode.GetBytes(response);
                                            handler.Send(data);
                                            Clear(trainer, Abonement, client, logIn, expert, report);
                                        }

                                    }
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
                                            Clear(trainer, Abonement, client, logIn, expert, report);
                                        }
                                    }
                                    break;
                                case "READ CLIENTS":
                                    {
                                        if (client.Search == "")
                                        {
                                            client.Search = builder.ToString();
                                        }
                                        else
                                        {
                                            string find = builder.ToString();
                                            DataTable dataTable = SqlCommander.GetClients(find, client);
                                            byte[] responseData = GetBinaryFormatData(dataTable);
                                            handler.Send(responseData);
                                            Clear(trainer, Abonement, client, logIn, expert, report);
                                        }
                                    }
                                    break;
                                case "READ ABONEMENTS":
                                    {
                                        if (Abonement.Search == "")
                                        {
                                            Abonement.Search = builder.ToString();
                                        }
                                        else
                                        {
                                            string find = builder.ToString();
                                            DataTable dataTable = SqlCommander.GetAbonements(find, Abonement);
                                            byte[] responseData = GetBinaryFormatData(dataTable);
                                            handler.Send(responseData);
                                            Clear(trainer, Abonement, client, logIn, expert, report);
                                        }
                                    }
                                    break;
                                case "READ TRAINERS":
                                    {
                                        if(trainer.Search == "")
                                        {
                                            trainer.Search = builder.ToString();
                                        }
                                        else
                                        {
                                            string find = builder.ToString();
                                            DataTable dataTable = SqlCommander.GetTrainers(find, trainer.Search);
                                            byte[] responseData = GetBinaryFormatData(dataTable);
                                            handler.Send(responseData);
                                            Clear(trainer, Abonement, client, logIn, expert, report);
                                        }
                                    }
                                    break;
                                case "GET CLIENT INFO":
                                    {
                                        client.Search = builder.ToString();
                                        DataTable dataTable = SqlCommander.GetClientInfo(client);
                                        byte[] responseData = GetBinaryFormatData(dataTable);
                                        handler.Send(responseData);
                                        Clear(trainer, Abonement, client, logIn, expert, report);
                                    }
                                    break;
                                case "SELECT OPERATIONS":
                                    {
                                        WorkSQL.info = builder.ToString();
                                        DataTable dataTable = SqlCommander.SelectOperations(WorkSQL.info);
                                        byte[] responseData = GetBinaryFormatData(dataTable);
                                        handler.Send(responseData);
                                        Clear(trainer, Abonement, client, logIn, expert, report);
                                    }
                                    break;
                                case "DELETE trainer":
                                    {
                                        trainer.ID = builder.ToString();
                                        string response = SqlCommander.DelTrainer(trainer);
                                        data = Encoding.Unicode.GetBytes(response);
                                        handler.Send(data);
                                        Clear(trainer, Abonement, client, logIn, expert, report);
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
                                                                                    Clear(trainer, Abonement, client, logIn, expert, report);
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
                                        Clear(trainer, Abonement, client, logIn, expert, report);
                                    }
                                    break;
                                case "ADD TRAINER":
                                    {
                                        if (trainer.ID == "")
                                        {
                                            trainer.ID = builder.ToString();
                                        }
                                        else
                                        {
                                            if (trainer.Surname == "")
                                            {
                                                trainer.Surname = builder.ToString();
                                            }
                                            else
                                            {
                                                if (trainer.Name == "")
                                                {
                                                    trainer.Name = builder.ToString();
                                                }
                                                else { 
                                                    if (trainer.ThirdName == "")
                                                    {
                                                        trainer.ThirdName = builder.ToString();
                                                    }
                                                    else
                                                    {
                                                        if (trainer.Type == "")
                                                        {
                                                            trainer.Type = builder.ToString();
                                                        }
                                                        else
                                                        {
                                                            trainer.Cost = builder.ToString();
                                                            string response = SqlCommander.AddTrainer(trainer);
                                                            data = Encoding.Unicode.GetBytes(response);
                                                            handler.Send(data);
                                                            Clear(trainer, Abonement, client, logIn, expert, report);
                                                        }
                                                    }
                                                }
                                            }
                                        }
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
                                                            if(client.ID == "")
                                                            {
                                                                client.ID = builder.ToString();

                                                            }
                                                            else
                                                            {
                                                                if(client.Login == "")
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
                                                                        if (client.Age == "")
                                                                        {
                                                                            client.Age = builder.ToString();
                                                                        }
                                                                        else
                                                                        {
                                                                            if (client.Access == "")
                                                                            {
                                                                                client.Access = builder.ToString();
                                                                                string response = SqlCommander.AddClient(client);
                                                                                data = Encoding.Unicode.GetBytes(response);
                                                                                handler.Send(data);
                                                                                Clear(trainer, Abonement, client, logIn, expert, report);
                                                                            }
                                                                        }
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
                                case "DELETE CLIENT":
                                    {
                                        client.ID = builder.ToString();
                                        string response = SqlCommander.DelClient(client);
                                        data = Encoding.Unicode.GetBytes(response);
                                        handler.Send(data);
                                        Clear(trainer, Abonement, client, logIn, expert, report);
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
                                                                    Clear(trainer, Abonement, client, logIn, expert, report);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case "ADD ABONEMENT":
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
                                                if (Abonement.Cost == "")
                                                {
                                                    Abonement.Cost = builder.ToString();
                                                }
                                                else
                                                {
                                                    if (Abonement.CountOfAttendents == "")
                                                    {
                                                        Abonement.CountOfAttendents = builder.ToString();
                                                        string response = SqlCommander.AddAbonement(Abonement);
                                                        data = Encoding.Unicode.GetBytes(response);
                                                        handler.Send(data);
                                                        Clear(trainer, Abonement, client, logIn, expert, report);
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
                                        Clear(trainer, Abonement, client, logIn, expert, report);
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
                                                        if (Abonement.Cost == "")
                                                        {
                                                            Abonement.Cost = builder.ToString();
                                                            string response = SqlCommander.ChangeAbonement(Abonement);
                                                            data = Encoding.Unicode.GetBytes(response);
                                                            handler.Send(data);
                                                            Clear(trainer, Abonement, client, logIn, expert, report);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case "SELECT TRAINER":
                                    {
                                        trainer.ID = builder.ToString();
                                        DataTable dataTable = SqlCommander.SelectTrainer(trainer);
                                        byte[] responseData = GetBinaryFormatData(dataTable);
                                        handler.Send(responseData);
                                        Clear(trainer, Abonement, client, logIn, expert, report);
                                    }
                                    break;
                                case "SELECT ABONEMENT":
                                    {
                                        DataTable dataTable = SqlCommander.SelectAbonement(Abonement);
                                        byte[] responseData = GetBinaryFormatData(dataTable);
                                        handler.Send(responseData);
                                        Clear(trainer, Abonement, client, logIn, expert, report);
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
        static private void Clear(Trainer trainer, Abonement abonement, Client client, LogIn logIn, Expert expert, Report report)
        {
            abonement.Clean();
            client.Clean();
            trainer.Clean();
            logIn.Clean();
            expert.Clean();
            report.Clear();

            status = 0;
            command = "";
        }
    }
}
