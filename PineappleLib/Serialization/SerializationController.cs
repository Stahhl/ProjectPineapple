using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using PineappleLib.Serialization.Surrogates;
using PineappleLib.Serialization.ClassSerializers;
using PineappleLib.Models.Units;

namespace PineappleLib.Serialization
{
    public class SerializationController
    {
        public SerializationController()
        {
            context = new StreamingContext();
            surrogateSelector = new SurrogateSelector();
            formatter = new BinaryFormatter(surrogateSelector, context);

            AddSurrogates();
            AddSerializers();
        }

        private StreamingContext context;
        private SurrogateSelector surrogateSelector;
        protected BinaryFormatter formatter;

        public UnitSerializer UnitSerializer { get; private set; }

        private void AddSurrogates()
        {
            surrogateSelector.AddSurrogate(typeof(Unit), context, new UnitSurrogate());
        }
        private void AddSerializers()
        {
            UnitSerializer = new UnitSerializer();
        }
    }
}
