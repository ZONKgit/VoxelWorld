using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    private static List<IPEndPoint> connectedClients = new List<IPEndPoint>();
    static UdpClient udpServer = new UdpClient(12345);

    static void Main()
    {

        Console.WriteLine("Waiting for client connection...");

        while (true)
        {
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] receivedData = udpServer.Receive(ref clientEndPoint);

            // Проверка длины полученных данных
            if (receivedData.Length == 12)
            {
                float x = BitConverter.ToSingle(receivedData, 0);
                float y = BitConverter.ToSingle(receivedData, 4);
                float z = BitConverter.ToSingle(receivedData, 8);

                // Обработка координат
                HandleReceivedCoordinates(clientEndPoint, x, y, z);

                // Отправка координат всем клиентам
                BroadcastCoordinates(x, y, z);
            }
            else
            {
                Console.WriteLine("Invalid data length received.");
            }
        }
    }

    static void HandleReceivedCoordinates(IPEndPoint clientEndPoint, float x, float y, float z)
    {
        // Пример обработки полученных координат (вывод в консоль)
        Console.WriteLine($"Received Coordinates from {clientEndPoint.Address}: X={x}, Y={y}, Z={z}");

        // Добавляем клиента в список подключенных
        if (!connectedClients.Contains(clientEndPoint))
        {
            connectedClients.Add(clientEndPoint);
            Console.WriteLine($"Client {clientEndPoint.Address} connected.");
        }
    }

    static void BroadcastCoordinates(float x, float y, float z)
    {
        // Отправка координат всем клиентам
        foreach (var clientEndPoint in connectedClients)
        {
            SendCoordinatesToClient(clientEndPoint, x, y, z);
        }
    }

    static void SendCoordinatesToClient(IPEndPoint clientEndPoint, float x, float y, float z)
    {
        try
        {
            // Создаем массив байт из координат
            byte[] data = new byte[12];
            BitConverter.GetBytes(x).CopyTo(data, 0);
            BitConverter.GetBytes(y).CopyTo(data, 4);
            BitConverter.GetBytes(z).CopyTo(data, 8);

            // Отправляем данные клиенту
            udpServer.Send(data, data.Length, clientEndPoint);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending coordinates to {clientEndPoint.Address}: {ex.Message}");
        }
    }
}
