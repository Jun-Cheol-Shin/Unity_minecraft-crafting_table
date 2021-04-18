using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    // 마우스 위치로 간 슬롯을 받는다.
    [Header("슬롯 확인용")]
    public GameObject slot;
    [Header("클릭한 아이템 확인용")]
    public GameObject clickItem;


    bool click = false;


    private Canvas canvas;
    private RectTransform rectTransform;

    private void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }


    public void clickFunction()
    {
        if(Input.GetMouseButtonDown(0))
        {
            click = !click;
            if(clickItem != null)
            {
                rectTransform = clickItem.GetComponent<RectTransform>();
            }

            if(!click)
            {
                if(slot.transform.childCount == 0)
                {
                    if(clickItem != null)
                    {
                        rectTransform.SetParent(slot.transform);
                        rectTransform.anchoredPosition = new Vector3(0, 0, 0);
                    }
                    clickItem = null;
                    rectTransform = null;
                }

                else
                {
                    if(clickItem.GetComponent<Item>().type != slot.transform.GetChild(0).gameObject.GetComponent<Item>().type)
                    {
                        click = true;
                        clickItem.transform.SetParent(slot.transform);
                        rectTransform.anchoredPosition = new Vector3(0, 0, 0);
                        clickItem = slot.transform.GetChild(0).gameObject;
                        clickItem.transform.SetParent(canvas.transform.GetChild(0));
                    }

                    else
                    {
                        slot.transform.GetChild(0).gameObject.GetComponent<Item>().count += clickItem.GetComponent<Item>().count;
                        slot.transform.GetChild(0).gameObject.GetComponent<Item>().resetcounting();
                        GameObject temp = clickItem;
                        clickItem = null;
                        Destroy(temp.gameObject);
                    }

                    if(clickItem != null)
                    {
                        rectTransform = clickItem.GetComponent<RectTransform>();
                    }
                }
            }
            else
            {
                if(clickItem != null)
                    rectTransform.SetParent(canvas.transform.GetChild(0));
            }
        }

        else if(Input.GetMouseButtonDown(1) && !click)
        {
            click = !click;

            if(clickItem != null)
            {
                rectTransform = clickItem.GetComponent<RectTransform>();
            }

            if(click)
            {
                if(clickItem.GetComponent<Item>().count > 1)
                {
                    Item c_item = clickItem.GetComponent<Item>();
                    Item t_item;
                    GameObject temp = Instantiate(clickItem);
                    temp.name = temp.GetComponent<Item>().type.ToString();
                    temp.transform.SetParent(clickItem.transform.parent);
                    temp.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

                    t_item = temp.GetComponent<Item>();
                    if(clickItem.GetComponent<Item>().count % 2 == 0)
                    {
                        t_item.count = c_item.count / 2;
                        c_item.count = c_item.count / 2;
                        c_item.resetcounting();
                        t_item.resetcounting();
                    }
                    else
                    {
                        t_item.count = c_item.count / 2 + 1;
                        c_item.count = c_item.count / 2;
                        c_item.resetcounting();
                        t_item.resetcounting();
                    }
                }
                if(clickItem != null)
                    rectTransform.SetParent(canvas.transform.GetChild(0));
            }

        }

        // 마우스 따라서 이동구현
        if(click && clickItem != null)
        {
            rectTransform.anchoredPosition = 
                new Vector3((Input.mousePosition.x - canvas.transform.position.x) + 22.5f,
                (Input.mousePosition.y - canvas.transform.position.y) - 22.5f, 0);
        }
    }

}
