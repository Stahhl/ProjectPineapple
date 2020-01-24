using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Domain.Models.Units;
using Domain.Interfaces;

public class UnitBehaviour : MonoBehaviour, ModelInterface
{
    //Unique
    public Unit MyUnit { get; private set; }

    //Interface
    public bool HasChanged { get; private set; }
    public bool IsInitialized { get; private set; }

    //Manually fields that match _UnitModel properties for displaying in inscpetor
    public string Name;
    public int Health;
    public int ActionPoints;

    private void Start()
    {
        Debug.Log("Start");
        Initialize(new Unit());
    }
    private void Update()
    {
        if (IsInitialized == false)
            return;

        if (MyUnit.HasChanged == true)
            UpdateMe();
    }

    public void Initialize(object t)
    {
        try
        {
            //if (IsInitialized == true || MyUnit != null)
            //    throw new SingletonException();

            IsInitialized = true;
            MyUnit = (Unit)t;
        }
        catch(System.Exception e)
        {
            throw new UnityException(e.Message);
        }
    }

    public void ToggleHasChanged()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateMe()
    {
        this.Name = MyUnit.Name;
        this.Health = MyUnit.HealthPoints;
        this.ActionPoints = MyUnit.ActionPoints;

        MyUnit.ToggleHasChanged();
    }
}
