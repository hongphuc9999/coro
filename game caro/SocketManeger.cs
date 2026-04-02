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

namespace game_caro
{
    internal class SocketManeger
    {
        #region Client
        Socket Client;
        public bool ConnectServer()
        {
            IPEndPoint o = new IPEndPoint(IPAddress.Parse(IP), POST);
            Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                Client.Connect(o);
                return true;
            }
            catch {
                return false;
            }
           
        }
        #endregion

        #region Server
        Socket Server;
        public void CreateServer()
        {
            if (Server != null)
                return;

            IPEndPoint o = new IPEndPoint(IPAddress.Parse(IP), POST);

            Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            

            Server.Bind(o);
            Server.Listen(10);

            Thread acceptClient = new Thread(() =>
            {
               
                         Client = Server.Accept();
            });

            acceptClient.IsBackground = true;
            acceptClient.Start();
        }
        #endregion

        #region Both
        public string IP = "127.0.0.1";
        public int POST =8888;
        public bool isServer = true;
        public const int BUFFER = 1024;
        public bool Send(object data)
        {
            byte[] senData = SerializeData(data);
           
          
            
                return Sendata(Client, senData);
            
            
        }
        public object Receive()
        {
            byte[] receivedata = new byte[BUFFER];
            bool isOK = Receivedata(Client, receivedata );

            return DeserializeData(receivedata);
        }

        public bool Sendata(Socket tager, byte[] data)
        {
            return tager.Send(data) == 1 ? true : false;
        }

        public bool Receivedata(Socket tager, byte[] data)
        {
            return tager.Receive(data) == 1 ? true : false;
        }
        public byte[] SerializeData(Object p)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(ms, p);
            return ms.ToArray();
        }

        public object DeserializeData(byte[] theByteArray)
        {
            MemoryStream ms = new MemoryStream(theByteArray);
            BinaryFormatter b = new BinaryFormatter();
            ms.Position = 0;    
            return b.Deserialize(ms);
        }
        
        public string GetLocalIPv4(NetworkInterfaceType _type)
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces()) 
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }
        #endregion
    }
}
