using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Domain.General.Serialization.Surrogates;
using Domain.Models.Units;

namespace Domain.General.Serialization
{
    public class SerializationController
    {
        public SerializationController()
        {
            context = new StreamingContext();
            surrogateSelector = new SurrogateSelector();
            formatter = new BinaryFormatter(surrogateSelector, context);

            surrogateSelector.AddSurrogate(typeof(Unit), context, new UnitSurrogate());   
        }

        private StreamingContext context;
        private SurrogateSelector surrogateSelector;
        private BinaryFormatter formatter;

        public byte[] UnitSerialize(Unit unit)
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
