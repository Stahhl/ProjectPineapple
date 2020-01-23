using System;
using System.Collections;
using System.Collections.Generic;
using Domain.Interfaces;
using Domain.General.Data;

namespace Domain.Models.Units
{
    public class Unit : ModelInterface
    {
        public Unit()
        {
            Name = "Peon";
            HealthPoints = Values.BaseHealthPoints;
            ActionPoints = Values.BaseActionPoints;
        }

        public bool HasChanged { get; private set; } = true;
        public bool IsInitialized { get; private set; }

        public string Id { get; private set; }
        public string Name { get; private set; }

        public int HealthPoints { get; private set; }
        public int ActionPoints { get; private set; }

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


