using System.Net;
using System.Net.Sockets;

namespace OnkyoControl
{
    class Receiver
    {
        private readonly TcpClient _client;
        private readonly IPEndPoint _endPoint;

        public Receiver(IPAddress ipAddress)
        {
            _client = new TcpClient();
            _endPoint = new IPEndPoint(ipAddress, 60128);
        }

        public void ExecutePackage(Package package)
        {
            _client.Connect(_endPoint);

            if (_client.Connected)
            {
                NetworkStream stream = _client.GetStream();
                stream.Write(package.ByteValue(), 0, package.Length());
            }
            _client.Close();
        }
    }
}
