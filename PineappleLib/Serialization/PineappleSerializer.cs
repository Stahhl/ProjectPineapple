using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using PineappleLib.Serialization.Surrogates;
using PineappleLib.Models.Units;
using PineappleLib.Logging;

namespace PineappleLib.Serialization
{
    public class PineappleSerializer
    {
        public PineappleSerializer()
        {
            context = new StreamingContext();
            surrogateSelector = new SurrogateSelector();
            formatter = new BinaryFormatter(surrogateSelector, context);

            AddSurrogates();
        }

        protected StreamingContext context;
        protected SurrogateSelector surrogateSelector;
        protected IFormatter formatter;

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

        private void AddSurrogates()
        {
            //surrogateSelector.AddSurrogate(typeof(Unit), context, new UnitSurrogate());
        }
    }
}
