﻿using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using PineappleLib.Interfaces;
using PineappleLib.Models.Units;

namespace PineappleLib.Serialization.Surrogates
{
    class UnitSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
            //Unit unit = (Unit)obj;
            //info.AddValue(nameof(unit.Id), unit.Id);
            //info.AddValue(nameof(unit.Name), unit.Name);
            //info.AddValue(nameof(unit.HealthPoints), unit.HealthPoints);
            //info.AddValue(nameof(unit.ActionPoints), unit.ActionPoints);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            throw new NotImplementedException();
            //return new Unit(info);
        }
    }
}
