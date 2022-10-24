using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public enum SlotType
    {
        DROP =  1,
        CRAFT = 2,
        OUTPUT = 3
    };


    public SlotType slotType = SlotType.DROP;

    public void OnPointerEnter(PointerEventData eventdata)
    {
        ClickManager.GetInstance.slot = gameObject;
    }

    public void OnPointerExit(PointerEventData eventdata)
    {
        ClickManager.GetInstance.slot = null;
    }
}
