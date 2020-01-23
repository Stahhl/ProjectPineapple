using System.Collections;
using System.Collections.Generic;

namespace Domain.Models.Interfaces
{
    public interface _ModelInterface
    {
        bool HasChanged { get; }
        bool IsInitialized { get; }
        void Initialize(object t);
        void ToggleHasChanged();
        void UpdateMe();
    }
}


