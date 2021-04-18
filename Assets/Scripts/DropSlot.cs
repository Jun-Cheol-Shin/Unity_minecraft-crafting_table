using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour,IPointerEnterHandler
{

    private ClickManager clickmanager;
    private void Start()
    {
        clickmanager = GameObject.Find("Management").transform.Find("ClickManager").GetComponent<ClickManager>();
    }

    public void OnPointerEnter(PointerEventData eventdata)
    {
        clickmanager.slot = gameObject;
    }
}
