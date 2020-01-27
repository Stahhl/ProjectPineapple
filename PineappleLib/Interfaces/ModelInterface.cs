using System.Collections;
using System.Collections.Generic;

namespace PineappleLib.Interfaces
{
    public interface ModelInterface
    {
        bool HasChanged { get; }
        bool IsInitialized { get; }
        void Initialize(object t);
        void ToggleHasChanged();
        void UpdateMe();
    }
}


