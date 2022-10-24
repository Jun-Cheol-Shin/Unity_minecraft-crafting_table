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
        // 슬롯 정보 가져오기
        ClickManager.GetInstance.slot = gameObject;
    }

    public void OnPointerExit(PointerEventData eventdata)
    {
        // 슬롯 정보 초기화
        ClickManager.GetInstance.slot = null;
    }
}
