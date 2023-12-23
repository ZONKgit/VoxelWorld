using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace VoxelWorld.Classes.Engine
{
    static public class NetWork
    {
        static bool useNetwork = false;
        static UdpClient udpClient = new UdpClient();

        static public void SendMessage(string message)
        {
            if (useNetwork)
            {
                try
                {
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    udpClient.Send(data, data.Length, "127.0.0.1", 12345);

                    // Получаем ответ от сервера
                    IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] responseData = udpClient.Receive(ref serverEndPoint);
                    string response = Encoding.UTF8.GetString(responseData);

                    Console.WriteLine($"Ответ от сервера ({serverEndPoint}): {response}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                // finally
                // {
                //     udpClient.Close();
                // }
                }
            }

        static public void CloseConnection()
        {
            udpClient.Close();
        }
    }
}