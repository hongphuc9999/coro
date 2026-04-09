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
        public string IP { get; set; } = "";
        public int Port { get; set; } = 9999;           // ← Đổi port cố định dễ nhớ
        public Socket Client { get; private set; }
        public Socket Server { get; private set; }
        public bool IsServer { get; private set; } = false;

        private Thread listenThread;
        private bool isListening = false;
        #endregion

        #region Server
        public void CreateServer()
        {
            try
            {
                IPEndPoint ipe = new IPEndPoint(IPAddress.Any, Port);

                Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Server.Bind(ipe);
                Server.Listen(1);   // Chỉ cho 1 người chơi kết nối

                IsServer = true;

                // Thread chấp nhận client
                Thread acceptThread = new Thread(AcceptClient);
                acceptThread.IsBackground = true;
                acceptThread.Start();

                MessageBox.Show($"Server đã khởi tạo thành công!\n\nPort: {Port}\n\nIP của bạn là:\n{GetLocalIPv4(NetworkInterfaceType.Wireless80211)}\nHoặc\n{GetLocalIPv4(NetworkInterfaceType.Ethernet)}\n\nHãy cho đối thủ nhập IP này!",
                    "Thông báo Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                Client = Server.Accept();   // Chờ client kết nối
                MessageBox.Show("Đối thủ đã kết nối thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Bắt đầu lắng nghe dữ liệu
                StartListening();
            }
            catch { }
        }
        #endregion

        #region Client
        public bool ConnectServer()
        {
            try
            {
                IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(IP), Port);
                Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Client.Connect(ipe);

                MessageBox.Show("Kết nối đến Server thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                StartListening();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kết nối thất bại: " + ex.Message + "\n\nKiểm tra lại IP và đảm bảo Server đã chạy.", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    if (Client == null || !Client.Connected)
                        break;

                    int bytesReceived = Client.Receive(buffer);
                    if (bytesReceived > 0)
                    {
                        byte[] receivedData = new byte[bytesReceived];
                        Array.Copy(buffer, receivedData, bytesReceived);

                        SocketData data = (SocketData)DeserializeData(receivedData);

                        // Gọi xử lý dữ liệu trên UI thread
                        if (Form.ActiveForm != null)
                        {
                            Form.ActiveForm.Invoke(new Action(() =>
                            {
                                // Bạn sẽ gọi ProcesData từ form
                                // Tạm thời chúng ta sẽ để form xử lý sau
                                // Hoặc bạn có thể tạo event
                            }));
                        }

                        // Vì code cũ của bạn dùng Receive() blocking, chúng ta sẽ giữ cơ chế cũ
                        // Nhưng cải tiến để ổn định hơn
                    }
                }
                catch
                {
                    // Kết nối bị ngắt
                    break;
                }
            }

            isListening = false;
        }
        #endregion

        #region Send & Receive
        public bool Send(object data)
        {
            if (Client == null || !Client.Connected)
                return false;

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

        public object Receive()
        {
            if (Client == null || !Client.Connected)
                throw new Exception("Chưa kết nối");

            byte[] buffer = new byte[4096];
            int bytes = Client.Receive(buffer);

            if (bytes > 0)
            {
                byte[] data = new byte[bytes];
                Array.Copy(buffer, data, bytes);
                return DeserializeData(data);
            }
            return null;
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
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                            return ip.Address.ToString();
                    }
                }
            }
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
    }
}
