using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickandDrop : MonoBehaviour, IPointerDownHandler
{
    private ClickManager clickmanager;

    private void Start()
    {
        clickmanager = GameObject.Find("Management").transform.Find("ClickManager").GetComponent<ClickManager>();
    }

    public void OnPointerDown(PointerEventData _eventData)
    {
        if(clickmanager.clickItem == null)
        {
            clickmanager.clickItem = gameObject;
        }
    }


}
