using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using PineappleLib.Models.Units;

namespace PineappleLib.Serialization.ClassSerializers
{
    public class UnitSerializer
    {
        public UnitSerializer(BinaryFormatter formatter)
        {
            this.formatter = formatter;
        }

        BinaryFormatter formatter;

        public byte[] Serialize(object obj)
        {
            byte[] data = null;

            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);
                data = stream.GetBuffer();
            }

            return data;
        }

        public object Deserialize(byte[] data)
        {
            object objectGraph = null;
            using (MemoryStream stream = new MemoryStream(data))
            {
                objectGraph = formatter.Deserialize(stream);
            }

            return objectGraph;
        }
    }
}
