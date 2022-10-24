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
            ClickManager.GetInstance.clickItem = gameObject;
        }
    }

}
