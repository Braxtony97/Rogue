using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _nameText;

    public void Initialize(AbilityData abilityData)
    {
        if (abilityData != null)
        {
            _icon.sprite = abilityData.Icon;
            _nameText.text = abilityData.Name;
        }
    }
}
