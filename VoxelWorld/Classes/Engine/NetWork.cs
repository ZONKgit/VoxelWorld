using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using OpenTK;

namespace VoxelWorld.Classes.Engine
{
    static public class NetWork
    {
        static UdpClient udpClient = new UdpClient();
        static IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
        public static float[] networkData = new[] { 0f, 0f, 0f };
        private static Vector3 playerPos;

        static public void SendCoordinates(float x, float y, float z)
        {
            try
            {
                // Создаем массив байт из координат
                byte[] data = new byte[12];
                BitConverter.GetBytes(x).CopyTo(data, 0);
                BitConverter.GetBytes(y).CopyTo(data, 4);
                BitConverter.GetBytes(z).CopyTo(data, 8);

                // Отправляем данные на сервер
                udpClient.Send(data, data.Length, serverEndPoint);

                playerPos = new Vector3(x, y, z);
                
                // Получаем и выводим координаты других клиентов
                ReceiveAndPrintCoordinates();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending coordinates: {ex.Message}");
            }
        }

        static void ReceiveAndPrintCoordinates()
        {
            // Получаем координаты от сервера
            byte[] receivedData = udpClient.Receive(ref serverEndPoint);

            // Проверка длины полученных данных
            if (receivedData.Length == 12)
            {
                float x = BitConverter.ToSingle(receivedData, 0);
                float y = BitConverter.ToSingle(receivedData, 4);
                float z = BitConverter.ToSingle(receivedData, 8);

                // Выводим координаты в консоль
                if (new Vector3(x, y, z) != playerPos)
                {
                    networkData[0] = x;
                    networkData[1] = y;
                    networkData[2] = z;
                    Console.WriteLine($"Received Coordinates from Server: X={x}, Y={y}, Z={z}");
                }
                
            }
            else
            {
                Console.WriteLine("Invalid data length received from the server.");
            }
        }

        static public void CloseConnection()
        {
            udpClient.Close();
        }
    }
}
