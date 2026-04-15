using UnityEngine;
using UnityEngine.EventSystems;

public class Passive : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Tooltip tooltipManager;
    [TextArea]
    public string passiveDescription;
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipManager.ShowToolTip(passiveDescription); //shows tooltip with the passive description when the mouse enters the passive icon
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipManager.HideToolTip(); //hides tooltip when the mouse exits the passive icon
    }

}
