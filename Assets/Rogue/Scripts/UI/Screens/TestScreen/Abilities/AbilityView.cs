using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Enums;
using static EventsProvider;

[Serializable]
public class ModifierPrefab
{
    public GameObject Prefab;
    public ModifierType ModifierType;
}

public class AbilityView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private GameObject selector;
    [SerializeField] private List<ModifierPrefab> ModifierPrefabs;

    private AbilityViewModel _ability;
    private ModifierModel _modifierModel;

    public void Initialize(AbilityViewModel ability, ModifierModel modifierModel)
    {
        _ability = ability;
        _modifierModel = modifierModel;
        _ability.OnDataChanged += UpdateUI;
        _ability.OnHighlightChanged += UpdateHighlightState;
        _ability.OnModifierAttached += ModifierAttache;
        _ability.OnModifierDetached += ModifierDetache;

        UpdateUI();

        EventAggregator.Instance.Subscribe<ModifierDragStateChangedEvent>(OnModifierDragStateChange);
    }

    private void ModifierAttache(ModifierModel model)
    {
        if (model  == null) 
            return;

        foreach (ModifierPrefab modifier in ModifierPrefabs)
        { 
            if (modifier.ModifierType == model.ModifierType)
            {
                modifier.Prefab.gameObject.SetActive(true);
                break;
            }
        }
    }

    private void ModifierDetache()
    {
        foreach (ModifierPrefab modifier in ModifierPrefabs)
        {
            modifier.Prefab.gameObject.SetActive(false);
        }
    }

    private void UpdateHighlightState(bool isHighLight)
    {
        selector.SetActive(isHighLight);
    }

    private void OnModifierDragStateChange(ModifierDragStateChangedEvent modifierDragStateChangedEvent)
    {
        if (modifierDragStateChangedEvent.IsDragging)
            _ability.UpdateHighlightFromDrag(modifierDragStateChangedEvent.Modifier);
        else
            _ability.ClearDragHighlight();
    }

    private void UpdateUI()
    {
        icon.sprite = _ability.Icon;
        nameText.text = _ability.Name; 

        ModifierAttache(_modifierModel);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventAggregator.Instance.Publish(new AbilityHoverEvent(_ability, true));

        selector.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventAggregator.Instance.Publish(new AbilityHoverEvent(_ability, false));

        selector.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        ModifierView draggedModifier = eventData.pointerDrag?.GetComponent<ModifierView>();

        if (draggedModifier != null && _ability.CanAcceptModifier(draggedModifier.ModifierViewModel))
        {
            _ability.AttachModifier(draggedModifier.ModifierViewModel);
        }
    }

    private void OnDestroy()
    {
        _ability.OnDataChanged -= UpdateUI;
        _ability.OnHighlightChanged -= UpdateHighlightState;
        _ability.OnModifierAttached -= ModifierAttache;
        _ability.OnModifierDetached -= ModifierDetache;
        EventAggregator.Instance.Unsubscribe<ModifierDragStateChangedEvent>(OnModifierDragStateChange);
    }
}
