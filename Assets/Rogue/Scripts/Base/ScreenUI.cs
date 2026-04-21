using System.Collections.Generic;
using UnityEngine;

public abstract class ScreenUI : MonoBehaviour
{
    [Header("\nScreen type")]
    public Enums.ScreenType ScreenType; 

    public abstract void Initialize(List<CharacterViewModel> characterViewModels, UIPanelController uiPanelController);

    public abstract void Deinitialize();
}
