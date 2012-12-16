using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace OnkyoControl
{
    class Receiver
    {
        private TcpClient _client;
        private readonly IPEndPoint _endPoint;

        public Receiver(IPAddress ipAddress)
        {
            _endPoint = new IPEndPoint(ipAddress, 60128);
        }
    
        public void ExecutePackage(Package package)
        {
            _client = new TcpClient();
            _client.Connect(_endPoint);

            if (_client.Connected)
            {
                NetworkStream stream = _client.GetStream();
                stream.Write(package.ByteValue(), 0, package.Length());
            }
            _client.Close();
        }

        public string ExecuteQuestion(Package package, bool returnDecimal)
        {
            string response = "";
            _client = new TcpClient();
            _client.Connect(_endPoint);

            if (_client.Connected)
            {
                NetworkStream stream = _client.GetStream();
                stream.Write(package.ByteValue(), 0, package.Length());

                byte[] responseBuffer = new byte[128];
                stream.Read(responseBuffer, 0, responseBuffer.Length);

                int startPosition = findOccurence(responseBuffer, 0x21); // EOF byte
                int endPosition = findOccurence(responseBuffer, 0x1A); // EOF byte

                if(startPosition < endPosition)
                {
                    if (returnDecimal)
                    {
                        startPosition = endPosition - 2;
                    }

                    byte[] data = new byte[endPosition - startPosition];
                    Buffer.BlockCopy(responseBuffer, startPosition, data, 0, data.Length);
                    response = Encoding.ASCII.GetString(data);

                    if(returnDecimal)
                    {
                        response = Convert.ToInt32(response, 16).ToString();
                    }
                }

            }
            _client.Close();

            return response;
        }

        private int findOccurence(byte[] buffer, byte toBeFound)
        {
            int count = 0;
            for(int i = 0; i < buffer.Length; i++)
            {
                if(buffer[i].Equals(toBeFound))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
