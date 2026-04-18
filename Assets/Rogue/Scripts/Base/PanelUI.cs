using System.Collections.Generic;
using UnityEngine;

public abstract class PanelUI : MonoBehaviour
{
    [Header("\nPanel type")]
    public Enums.PanelType PanelType;
    public abstract void Initialize(List<CharacterViewModel> characterViewModels);
    public abstract void Deinitialize();
}
