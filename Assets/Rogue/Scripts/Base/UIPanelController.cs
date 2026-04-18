using System.Collections.Generic;
using UnityEngine;

public class UIPanelController : MonoBehaviour
{
    public PanelUI CurrentPanel => _currentPanel;

    [SerializeField] private PanelUI[] panels;

    private PanelUI _currentPanel;
    private List<CharacterViewModel> _characterViewModels;
    private Dictionary<Enums.PanelType, PanelUI> _instantiatedPanels = new Dictionary<Enums.PanelType, PanelUI>();

    public void Initialize(List<CharacterViewModel> characterViewModels)
    {
        _characterViewModels = characterViewModels;
    }

    public void ShowPanel(Enums.PanelType panelType, Transform canvas)
    {
        if (_currentPanel != null)
        {
            _currentPanel.gameObject.SetActive(false);
        }

        if (_instantiatedPanels.TryGetValue(panelType, out PanelUI existingPanel))
        {
            existingPanel.gameObject.SetActive(true);
            _currentPanel = existingPanel;
            return;
        }

        foreach (PanelUI panel in panels)
        {
            if (panel.PanelType == panelType)
            {
                PanelUI newPanel = Instantiate(panel);
                newPanel.transform.SetParent(canvas, false);
                panel.gameObject.SetActive(true);
                newPanel.Initialize(_characterViewModels);

                _instantiatedPanels[panelType] = newPanel;
                _currentPanel = newPanel;
                break;
            }
        }
    }

    public void ClearPanels()
    {
        foreach (PanelUI panel in _instantiatedPanels.Values)
        {
            if (panel != null)
                Destroy(panel.gameObject);
        }

        _instantiatedPanels.Clear();
        _currentPanel = null;
    }

    private void OnDestroy() => 
        ClearPanels();
}
