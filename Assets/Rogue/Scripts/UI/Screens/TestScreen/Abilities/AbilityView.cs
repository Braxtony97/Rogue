using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _nameText;

    public void Initialize(AbilityViewModel ability)
    {
        if (ability != null)
        {
            _icon.sprite = ability.Icon;
            _nameText.text = ability.Name;
        }
    }
}
