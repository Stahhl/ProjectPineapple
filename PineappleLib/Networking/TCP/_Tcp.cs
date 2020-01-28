using System;
using System.Net.Sockets;

namespace PineappleLib.Networking.TCP
{
    public abstract class _Tcp
    {
        public TcpClient socket;

        protected int Id;
        protected NetworkStream stream;
        //private Packet receivedData;
        protected byte[] receiveBuffer;

        public virtual void Connect()
        {
            throw new NotImplementedException();
        }
        public virtual void Connect(TcpClient socket)
        {
            throw new NotImplementedException();
        }
        public virtual void ReceiveCallback(IAsyncResult _result)
        {
            throw new NotImplementedException();
        }
        public virtual void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void SendData(Packet _packet)
        {
            try
            {
                if (socket != null)
                {
                    //stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
                }
            }
            catch (Exception _ex)
            {
                throw new Exception(_ex.Message);
            }
        }
        public bool HandleData(byte[] _data)
        {
            //int _packetLength = 0;

            //receivedData.SetBytes(_data);

            //if (receivedData.UnreadLength() >= 4)
            //{
            //    // If client's received data contains a packet
            //    _packetLength = receivedData.ReadInt();
            //    if (_packetLength <= 0)
            //    {
            //        // If packet contains no data
            //        return true; // Reset receivedData instance to allow it to be reused
            //    }
            //}

            //while (_packetLength > 0 && _packetLength <= receivedData.UnreadLength())
            //{
            //    // While packet contains data AND packet data length doesn't exceed the length of the packet we're reading
            //    byte[] _packetBytes = receivedData.ReadBytes(_packetLength);
            //    ThreadManager.ExecuteOnMainThread(() =>
            //    {
            //        using (Packet _packet = new Packet(_packetBytes))
            //        {
            //            int _packetId = _packet.ReadInt();
            //            Server.packetHandlers[_packetId](id, _packet); // Call appropriate method to handle the packet
            //        }
            //    });

            //    _packetLength = 0; // Reset packet length
            //    if (receivedData.UnreadLength() >= 4)
            //    {
            //        // If client's received data contains another packet
            //        _packetLength = receivedData.ReadInt();
            //        if (_packetLength <= 0)
            //        {
            //            // If packet contains no data
            //            return true; // Reset receivedData instance to allow it to be reused
            //        }
            //    }
            //}

            //if (_packetLength <= 1)
            //{
            //    return true; // Reset receivedData instance to allow it to be reused
            //}

            return false;
        }
    }
}
