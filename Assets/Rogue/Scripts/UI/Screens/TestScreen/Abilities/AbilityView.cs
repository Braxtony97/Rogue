using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static EventsProvider;

public class AbilityView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _nameText;

    private AbilityViewModel _ability;

    public void Initialize(AbilityViewModel ability)
    {
        _ability = ability;
        _ability.OnDataChanged += UpdateUI;

        UpdateUI();
    }

    private void UpdateUI()
    {
        _icon.sprite = _ability.Icon;
        _nameText.text = _ability.Name;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventAggregator.Instance.Publish(new AbilityHoverEvent(_ability, true));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventAggregator.Instance.Publish(new AbilityHoverEvent(_ability, false));
    }
}
