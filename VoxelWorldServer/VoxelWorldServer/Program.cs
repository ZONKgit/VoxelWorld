using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        TcpListener server = new TcpListener(IPAddress.Any, 12345);
        server.Start();

        Console.WriteLine("Ожидание подключения клиента...");

        using (TcpClient client = server.AcceptTcpClient())
        using (NetworkStream stream = client.GetStream())
        {
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string jsonData = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            // Десериализуем JSON в массив объектов
            object[] receivedArray = JsonConvert.DeserializeObject<object[]>(jsonData);

            // Используем массив объектов
            foreach (var item in receivedArray)
            {
                Console.WriteLine(item);
            }
        }

        server.Stop();
    }
}