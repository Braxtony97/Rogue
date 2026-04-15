using UnityEngine;

public abstract class ScreenUI : MonoBehaviour
{
    [Header("\nScreen type")]
    public Enums.ScreenType ScreenType; 

    public abstract void Initialize();

    public abstract void Deinitialize();
}
