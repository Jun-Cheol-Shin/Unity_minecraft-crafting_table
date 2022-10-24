using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickandDrop : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData _eventData)
    {
        if(ClickManager.GetInstance.clickItem == null)
        {
            // 아이템의 정보를 가져온다.
            ClickManager.GetInstance.clickItem = gameObject; 
        }
    }

}
