using System;
using System.Text;

namespace OnkyoControl
{
    public class Package
    {
        private byte[] _buffer;
        private int _pointer;
        
        public Package(string command)
        {
            //
            uint commandLength = ((uint) command.Length) + 2;
            uint packageLength = commandLength + 1 + 16;

            // Create a buffer in memory with the calculated package size
            _pointer = 0;
            _buffer = new byte[packageLength];

            AppendHeader();
            AppendMessageSize();
            AppendVersion();
            AppendReserved();
            AppendMessagePrefix();
            AppendString(command);
            AppendEnd();
        }

        public byte[] ByteValue()
        {
            return _buffer;
        }

        public int Length()
        {
            return _buffer.Length;
        }

        private void AppendHeader()
        {
            AppendString("ISCP");

            // Not sure what this block is for...
            byte[] part2 = new byte[] {0x00, 0x00, 0x00, 0x10};
            AppendBytes(part2);
        }

        private void AppendMessageSize()
        {
            byte[] size = new byte[] {0x00, 0x00, 0x00, (byte)_buffer.Length};
            AppendBytes(size);
        }

        private void AppendVersion()
        {
            byte[] version = new byte[] {0x01};
            AppendBytes(version);
        }

        private void AppendReserved()
        {
            byte[] reserved = new byte[] {0x00, 0x00, 0x00};
            AppendBytes(reserved);
        }

        private void AppendMessagePrefix()
        {
            AppendString("!1");
        }

        private void AppendEnd()
        {
            byte[] end = new byte[] {0x0D};
            AppendBytes(end);
        }

        private void AppendString(string value)
        {
            byte[] byteValue = Encoding.ASCII.GetBytes(value);
            AppendBytes(byteValue);
        }

        private void AppendBytes(byte[] source)
        {
            Buffer.BlockCopy(source, 0, _buffer, _pointer, source.Length);
            _pointer += source.Length;
        }
    }
}