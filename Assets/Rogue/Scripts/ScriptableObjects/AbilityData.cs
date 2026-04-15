using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Data/Ability")]
public class AbilityData : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public AbilityType AbilityType;
    public List<ModifierType> CompatibleModifiers;
}
