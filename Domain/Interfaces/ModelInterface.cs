using System.Collections;
using System.Collections.Generic;

namespace Domain.Interfaces
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


