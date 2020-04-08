using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PanelsManager : MonoBehaviour
{
    public Panel defaultPanel;
    public List<Panel> panels;
    public Panel currentPanel;

    private void Awake() {
        panels = GetComponentsInChildren<Panel>(true).ToList();

        if (panels.Count > 0) {
            defaultPanel = panels[0];
        }

        EnablePanel(defaultPanel);
    }

    private void EnablePanel(Panel panel) {
        if (panel != null) {
            DisableAll();
            currentPanel = panel;
            currentPanel.gameObject.SetActive(true);
            currentPanel.OnPanelEnable();
        }
    }

    public void SetPanelByName(string panelName) {
        Panel panel = GetPanelByName(panelName);
        EnablePanel(panel);
    }

    private void DisableAll() {
        for (int i = 0; i < panels.Count; i++)
        {
            if (panels[i].gameObject.activeInHierarchy) {
                panels[i].OnPanelDisable();
            }

        }
    }

    private Panel GetPanelByName(string panelName) {
        for (int i = 0; i < panels.Count; i++)
        {
            if (panels[i].gameObject.name == panelName) {
                return panels[i];
            }
        }

        return null;
    }
}
