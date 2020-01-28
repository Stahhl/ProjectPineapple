using System.IO;
using PineappleLib.Models.Units;

namespace PineappleLib.Serialization.ClassSerializers
{
    public class UnitSerializer : SerializationController
    {
        public byte[] Serialize(Unit unit)
        {
            byte[] data = null;

            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, unit);
                data = stream.GetBuffer();
            }

            return data;
        }

        public Unit Deserialize(byte[] data)
        {
            object objectGraph = null;
            using (MemoryStream stream = new MemoryStream(data))
            {
                objectGraph = formatter.Deserialize(stream);
            }

            return (Unit)objectGraph;
        }
    }
}
