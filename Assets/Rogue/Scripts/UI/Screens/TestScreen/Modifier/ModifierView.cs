using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static EventsProvider;

public class ModifierView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ModifierViewModel ModifierViewModel => _viewModel;

    [SerializeField] private Image iconLogo;
    [SerializeField] private Image iconBack;

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text typeText;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private GameObject highlight;

    private ModifierViewModel _viewModel;
    private RectMask2D _mask;
    private Vector2 _offset;

    private Vector2 _originalAnchoredPosition;

    public void Initialize(ModifierViewModel viewModel, RectMask2D mask)
    {
        _viewModel = viewModel;
        _mask = mask;

        EventAggregator.Instance.Subscribe<AbilityHoverEvent>(OnAbilityHover);
        _viewModel.OnHighlihtChanged += ChangeHighlight; 

        UpdateUI();
    }

    private void ChangeHighlight(bool isHighlight) => 
        highlight.SetActive(isHighlight);

    private void OnAbilityHover(AbilityHoverEvent abilityHoverEvent)
    {
        if (abilityHoverEvent.IsHover)
        {
            bool isCompatible = _viewModel.CanAttachToAbility(abilityHoverEvent.ViewModel);
            _viewModel.SetCompatibleHighlight(isCompatible);
        }
        else
        {
            _viewModel.SetCompatibleHighlight(false); 
        }
    }

    private void UpdateUI()
    {
        nameText.text = _viewModel.Name; 
        typeText.text = _viewModel.ModifierType.ToString();
        iconLogo.sprite = _viewModel.IconLogo;
        iconBack.color = _viewModel.IconBackColor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;

        eventData.pointerDrag = gameObject;

        _viewModel.SetDragging(true);

        if (_mask != null)
            _mask.enabled = false;

        _originalAnchoredPosition = rectTransform.anchoredPosition;

        Vector2 localPointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            eventData.position, 
            eventData.pressEventCamera,
            out localPointerPosition);

        _offset = rectTransform.anchoredPosition - localPointerPosition;

        EventAggregator.Instance.Publish(new ModifierDragStateChangedEvent(_viewModel, true));
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_viewModel.IsAttached)
        {
            Vector2 localPointerPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform.parent as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out localPointerPosition);

            rectTransform.anchoredPosition = localPointerPosition + _offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        _viewModel.SetDragging(false);

        if (_mask != null)
            _mask.enabled = true;

        rectTransform.anchoredPosition = _originalAnchoredPosition;

        EventAggregator.Instance.Publish(new ModifierDragStateChangedEvent(_viewModel, false));
    }

    private void OnDestroy()
    {
        if (EventAggregator.Instance != null)
            EventAggregator.Instance.Unsubscribe<AbilityHoverEvent>(OnAbilityHover);

        if (_viewModel != null)
            _viewModel.OnHighlihtChanged -= ChangeHighlight;
    }
}
