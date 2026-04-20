using System.Collections.Generic;
using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Data/Ability")]
public class AbilityData : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public AbilityName AbilityType;
    public List<ModifierType> CompatibleModifiers;
}
