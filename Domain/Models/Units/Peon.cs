using System.Collections;
using System.Collections.Generic;
using Domain.General.Data;

namespace Domain.Models.Units
{
    public class Peon : _UnitModel
    {
        public Peon()
        {
            MyName = "Peon";
            HealthPoints = Values.BaseHealthPoints;
            ActionPoints = Values.BaseActionPoints;
        }
    }

}

