using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "NewModifier", menuName = "Data/Modifier")]
public class ModifierData : ScriptableObject
{
    public string Name;
    public ModifierName ModifierName;
    public ModifierType ModifierType;
}
