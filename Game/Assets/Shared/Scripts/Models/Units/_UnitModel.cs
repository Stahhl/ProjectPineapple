using System.Collections;
using System.Collections.Generic;

public abstract class _UnitModel : _ModelInterface
{
    public bool HasChanged { get; private set; } = true;
    public bool IsInitialized { get; private set; }

    public string MyId { get; protected set; }
    public string MyName { get; protected set; }

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
