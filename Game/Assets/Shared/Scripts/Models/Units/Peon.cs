using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peon : _UnitModel
{
    public Peon()
    {
        MyName = "Peon";
        HealthPoints = Values.BaseHealthPoints;
        ActionPoints = Values.BaseActionPoints;
    }
}
