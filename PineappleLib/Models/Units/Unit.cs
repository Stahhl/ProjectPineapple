using System;
using System.Collections;
using System.Collections.Generic;
using PineappleLib.Interfaces;
using PineappleLib.General.Data;
using System.Runtime.Serialization;

namespace PineappleLib.Models.Units
{
    [Serializable]
    public class Unit : ModelInterface
    {
        public Unit()
        {
            Name = "Peon";
            HealthPoints = Values.BaseHealthPoints;
            ActionPoints = Values.BaseActionPoints;
        }
        public Unit(SerializationInfo info)
        {
            Id = info.GetString(nameof(Id));
            Name = info.GetString(nameof(Name));
            HealthPoints = info.GetInt32(nameof(HealthPoints));
            ActionPoints = info.GetInt32(nameof(ActionPoints));
        }

        public bool HasChanged { get; protected set; } = true;
        public bool IsInitialized { get; protected set; }

        public string Id { get; protected set; }
        public string Name { get; protected set; }

        public int HealthPoints { get; protected set; }
        public int ActionPoints { get; protected set; }

        public void AdjustActionPoints(int value, bool isNegative)
        {
            throw new System.NotImplementedException();
        }

        public void AdjustHealth(int value, bool isNegative)
        {
            throw new System.NotImplementedException();
        }

        public void Initialize(object t)
        {
            throw new System.NotImplementedException();
        }

        public void ToggleHasChanged()
        {
            HasChanged = !HasChanged;
        }

        public void UpdateMe()
        {
            throw new System.NotImplementedException();
        }
    }
}


