using UnityEngine;

public class PanelLogic : MonoBehaviour
{
    public GameObject upgradePanel;
    public GameObject profilePanel;
    public GameObject settingsPanel;

    public void ShowUpgradePanel()
    {
        upgradePanel.SetActive(true);
        profilePanel.SetActive(false);
        settingsPanel.SetActive(false);

    }

    public void ShowProfilePanel()
    {

        profilePanel.SetActive(true);
        upgradePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }
    public void ShowSettingsPanel()
    {
        settingsPanel.SetActive(true);
        upgradePanel.SetActive(false);
        profilePanel.SetActive(false);
    }
    public void HideAllPanels()
    {
        upgradePanel.SetActive(false);
        profilePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }


    
}
