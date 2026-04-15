using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{
    public GameObject tooltipPanel;
    public TextMeshProUGUI tooltipText;
    void Start()
    {
        tooltipPanel.SetActive(false); //doesn't show tooltip panel
    }
    public void ShowToolTip(string message)
    {
        tooltipPanel.SetActive(true); //shows tooltip panel
        tooltipText.text = message; //sets the text of the tooltip to the message passed in
    }
    public void HideToolTip()
    {
        tooltipPanel.SetActive(false); //hides tooltip panel
    }
}
