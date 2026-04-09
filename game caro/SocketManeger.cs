using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game_caro
{
    internal class SocketManeger
    {
        #region Properties
        public string IP { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 9999;
        public Socket Client { get; private set; }
        public Socket Server { get; private set; }
        public bool IsServer { get; private set; } = false;

        private Thread listenThread;
        private bool isListening = false;

        // Bổ sung Event để truyền dữ liệu sang Form
        public event Action<SocketData> DataReceived;
        public event Action Connected; // Thông báo khi 2 máy kết nối thành công
        #endregion

        #region Server
        public void CreateServer()
        {
            try
            {
                Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Server.Bind(new IPEndPoint(IPAddress.Any, Port));
                Server.Listen(1);
                IsServer = true;

                string ipLan = GetLocalIPv4(NetworkInterfaceType.Wireless80211);
                if (string.IsNullOrEmpty(ipLan) || ipLan == "Không tìm thấy IP")
                {
                    ipLan = GetLocalIPv4(NetworkInterfaceType.Ethernet);
                }

                MessageBox.Show(
                    $"Bạn là HOST.\nIP của bạn là: {ipLan}\n" +
                    $"Hãy gửi IP này cho đối thủ để họ kết nối.",
                    "Thông báo Server",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Thread acceptThread = new Thread(AcceptClient);
                acceptThread.IsBackground = true;
                acceptThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tạo Server thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AcceptClient()
        {
            try
            {
                Client = Server.Accept();
                Connected?.Invoke(); // Báo cho Form biết đã có người kết nối
                StartListening();
            }
            catch { }
        }
        #endregion

        #region Client
        public bool ConnectServer(string inputIP)
        {
            try
            {
                IsServer = false;
                IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(inputIP), Port);
                Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Client.Connect(ipe);

                Connected?.Invoke(); // Báo cho Form biết đã kết nối thành công tới Server
                StartListening();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kết nối thất bại: " + ex.Message + "\n\nĐảm bảo Host đã ấn LAN trước.", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        #endregion

        #region Listen & Receive
        private void StartListening()
        {
            if (isListening) return;
            isListening = true;

            listenThread = new Thread(ListenThread);
            listenThread.IsBackground = true;
            listenThread.Start();
        }

        private void ListenThread()
        {
            byte[] buffer = new byte[4096];
            while (true)
            {
                try
                {
                    if (Client == null || !Client.Connected) break;

                    int bytesReceived = Client.Receive(buffer);
                    if (bytesReceived > 0)
                    {
                        byte[] receivedData = new byte[bytesReceived];
                        Array.Copy(buffer, receivedData, bytesReceived);

                        SocketData data = (SocketData)DeserializeData(receivedData);

                        // Bắn sự kiện mang dữ liệu qua Form xử lý
                        DataReceived?.Invoke(data);
                    }
                }
                catch
                {
                    break;
                }
            }
            isListening = false;
        }
        #endregion

        #region Send
        public bool Send(object data)
        {
            if (Client == null || !Client.Connected) return false;

            try
            {
                byte[] sendData = SerializeData(data);
                return Client.Send(sendData) > 0;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Serialize / Deserialize
        private byte[] SerializeData(object p)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, p);
                return ms.ToArray();
            }
        }

        private object DeserializeData(byte[] theByteArray)
        {
            using (MemoryStream ms = new MemoryStream(theByteArray))
            {
                BinaryFormatter bf = new BinaryFormatter();
                ms.Position = 0;
                return bf.Deserialize(ms);
            }
        }
        #endregion

        #region Get IP
        public string GetLocalIPv4(NetworkInterfaceType _type)
        {
            try
            {
                foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                    {
                        foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                if (ip.Address.ToString().StartsWith("127.")) continue;
                                return ip.Address.ToString();
                            }
                        }
                    }
                }
            }
            catch { }
            return "Không tìm thấy IP";
        }
        #endregion

        public void CloseConnection()
        {
            try
            {
                Client?.Close();
                Server?.Close();
            }
            catch { }
        }

        internal void ConnectToServer(string inputIP)
        {
            throw new NotImplementedException();
        }

        internal SocketData Receive()
        {
            throw new NotImplementedException();
        }
    }
}

