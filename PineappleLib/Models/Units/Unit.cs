using System;
using System.Collections;
using System.Collections.Generic;
using PineappleLib.Interfaces;
using PineappleLib.General.Data;
using System.Runtime.Serialization;
using PineappleLib.Models.Abilities;

namespace PineappleLib.Models.Units
{
    [Serializable]
    public class Unit : ModelInterface
    {
        public Unit()
        {
            Abilities = new List<_Ability>();

            Name = "Peon";
            HealthPoints = Values.BaseHealthPoints;
            ActionPoints = Values.BaseActionPoints;

            Abilities.Add(new Slap());
        }

        public List<_Ability> Abilities { get; protected set; }

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
            if(isNegative == true)
            {
                HealthPoints -= value;
            }
            else
            {
                HealthPoints += value;
            }
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


