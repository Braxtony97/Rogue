using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModifierView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text typeText;
    [SerializeField] private RectTransform rectTransform;

    private ModifierViewModel _viewModel;
    private RectMask2D _mask;
    private Vector2 _offset;

    private Vector2 _originalAnchoredPosition;

    public void Initialize(ModifierViewModel viewModel, RectMask2D mask)
    {
        _viewModel = viewModel;
        _mask = mask;

        UpdateUI();
    }

    private void UpdateUI()
    {
        nameText.text = _viewModel.Name;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
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
        if (_mask != null)
            _mask.enabled = true;

        rectTransform.anchoredPosition = _originalAnchoredPosition;
    }
}
