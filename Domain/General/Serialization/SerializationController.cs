using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Domain.General.Serialization.Surrogates;

namespace Domain.General.Serialization
{
    public class SerializationController
    {
        public SerializationController()
        {
            context = new StreamingContext();
            surrogateSelector = new SurrogateSelector();
            formatter = new BinaryFormatter(surrogateSelector, context);

            //surrogateSelector.AddSurrogate(typeof(Foo), context, new FooSurrogate());   
        }

        private StreamingContext context;
        private SurrogateSelector surrogateSelector;
        private BinaryFormatter formatter;
    }
}
