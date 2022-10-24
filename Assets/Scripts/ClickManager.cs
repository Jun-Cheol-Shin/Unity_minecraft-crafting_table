using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class ClickManager : Singleton<ClickManager>
{
    readonly int CURSOR_ITEM_DISTANCE = 30;

    public UnityEvent onClickEvent;
    public UnityEvent onCraftEvent;

    // 마우스 위치로 간 슬롯을 받는다.
    [Header("슬롯 확인용")]
    public GameObject slot;
    [Header("클릭한 아이템 확인용")]
    public GameObject clickItem;
    [Header("커서에 아이템이 있는지 확인용")]
    public bool cursorOnItem = false;

    [SerializeField]
    private Canvas canvas = null;

    private RectTransform rectTransform;


    public void CanvasInit()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        if (!canvas)
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

            if (!canvas) Debug.LogError("캔버스를 찾지 못했습니다!!");
        }
    }

    public void ClickFunc()
    {
        // 좌 클릭..(재료 옮기기 등)
        if (Input.GetMouseButtonDown(0))
        {
            // 커서에 아이템을 들고 있지 않은 경우...
            if (slot && clickItem && !cursorOnItem)
            {
                cursorOnItem = true;
                // 아이템의 트랜스폼을 저장 및 부모 위치 조정
                rectTransform = clickItem.GetComponent<RectTransform>();
                if(rectTransform) rectTransform.SetParent(canvas.transform.GetChild(0));

                // 아웃풋 슬롯의 아이템을 가져갔다면.. 재료를 소모시킨다.
                DropSlot SlotType = slot.GetComponent<DropSlot>();
                if (SlotType && SlotType.slotType == DropSlot.SlotType.OUTPUT) onCraftEvent.Invoke();
            }
            // 커서에 아이템이 있는 경우..
            else
            {
                cursorOnItem = false;
                if (clickItem && rectTransform)
                {
                    // 커서를 슬롯에 갖다 댄 경우
                    if (slot)
                    {
                        DropSlot SlotType = slot.GetComponent<DropSlot>();     
                        Item clickItemType = clickItem.GetComponent<Item>();

                        // 아웃풋 슬롯에 아이템을 넣을려 한다면..
                        if (SlotType.slotType == DropSlot.SlotType.OUTPUT && slot.transform.childCount == 0)
                        {
                            cursorOnItem = true;
                            return;
                        }
                        // 슬롯이 비어있다면..
                        else if (slot.transform.childCount == 0) rectTransform.SetParent(slot.transform);
                        // 비어 있지 않았다면..
                        else
                        {
                            Item slotItemType = slot.transform.GetChild(0).gameObject.GetComponent<Item>();
                            if (clickItemType && slotItemType)
                            {
                                // 같은 아이템인 경우..
                                if (clickItemType.type == slotItemType.type)
                                {
                                    GameObject temp;
                                    switch (SlotType.slotType)
                                    {
                                        // 아이템을 놓을 수 없는 슬롯이라면... (아웃풋 슬롯)
                                        // 아웃풋 슬롯의 아이템을 가져갔다면.. 재료를 소모시킨다.
                                        case DropSlot.SlotType.OUTPUT:
                                            clickItemType.count += slotItemType.count;
                                            clickItemType.resetcounting();
                                            cursorOnItem = true;
                                            onCraftEvent.Invoke();
                                            onClickEvent.Invoke();
                                            return;
                                        // 아이템을 놓을 수 있는 슬롯이라면... 아이템을 합친다.
                                        default:
                                            slotItemType.count += clickItemType.count;
                                            slotItemType.resetcounting();
                                            temp = clickItem;
                                            Destroy(temp.gameObject);
                                            break;
                                    }
                                }

                                // 다른 아이템인 경우 들고있는 아이템과 스왑
                                else
                                {
                                    cursorOnItem = true;
                                    switch (SlotType.slotType)
                                    {
                                        // 아이템을 놓을 수 없는 슬롯이라면... (아웃풋 슬롯)
                                        // 아웃풋 슬롯의 아이템을 가져갔다면.. 재료를 소모시킨다.
                                        case DropSlot.SlotType.OUTPUT:
                                            rectTransform.SetParent(InventoryUI.GetInstance.GetEmptySlot());
                                            rectTransform.anchoredPosition = new Vector3(0, 0, 0);
                                            onCraftEvent.Invoke();
                                            break;
                                        // 아이템을 놓을 수 있는 슬롯이라면...
                                        default:
                                            rectTransform.SetParent(slot.transform);
                                            rectTransform.anchoredPosition = new Vector3(0, 0, 0);
                                            break;
                                    }

                                    clickItem = slot.transform.GetChild(0).gameObject;
                                    rectTransform = slot.transform.GetChild(0).gameObject.GetComponent<RectTransform>();
                                    if (rectTransform) rectTransform.SetParent(canvas.transform.GetChild(0));
                                    onClickEvent.Invoke();
                                    return;
                                }
                            }
                        }
                    }
                    // 슬롯이 아닌 곳을 클릭하면 아무 빈 슬롯에 아이템을 위치시킨다.
                    else rectTransform.SetParent(InventoryUI.GetInstance.GetEmptySlot());

                    rectTransform.anchoredPosition = new Vector3(0, 0, 0);
                }
                clickItem = null;
                rectTransform = null;
            }
            onClickEvent.Invoke();
        }
        // 우 클릭.. (재료 반으로 나누기 등)
        else if (Input.GetMouseButtonDown(1))
        {
            if (!cursorOnItem)
            {
                DropSlot SlotType = slot.GetComponent<DropSlot>();
                if (SlotType.slotType == DropSlot.SlotType.OUTPUT)
                {
                    rectTransform = null;
                    clickItem = null;
                    cursorOnItem = false;
                    return;
                }

                cursorOnItem = true;
                if (clickItem) rectTransform = clickItem.GetComponent<RectTransform>();
                if (rectTransform) rectTransform.SetParent(canvas.transform.GetChild(0));

                Item OutputItem = clickItem.GetComponent<Item>();
                // 아이템을 나눠야 하는 경우...
                if (SlotType && SlotType.slotType != DropSlot.SlotType.OUTPUT && OutputItem && OutputItem.count > 1)
                {
                    // 커서 쪽에 아이템을 새로 만든다.
                    GameObject temp = Instantiate(clickItem);
                    temp.name = temp.GetComponent<Item>().type.ToString();
                    temp.transform.SetParent(slot.transform);
                    temp.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

                    Item Slotitem = temp.GetComponent<Item>();

                    // 슬롯과 커서쪽 아이템의 개수를 정해준다.
                    if (Slotitem.count % 2 == 0)
                    {
                        Slotitem.count = OutputItem.count / 2;
                        OutputItem.count = Slotitem.count;
                    }
                    else
                    {
                        Slotitem.count = OutputItem.count / 2 + 1;
                        OutputItem.count = Slotitem.count - 1;
                    }

                    // UI쪽 text 설정
                    Slotitem.resetcounting();
                    OutputItem.resetcounting();
                }
                else
                {
                    rectTransform = null;
                    clickItem = null;
                    cursorOnItem = false;
                }
            }
            onClickEvent.Invoke();
        }

        if(cursorOnItem && clickItem && rectTransform)
        {
            rectTransform.anchoredPosition =
                new Vector2((Input.mousePosition.x - canvas.transform.position.x) + CURSOR_ITEM_DISTANCE,
                (Input.mousePosition.y - canvas.transform.position.y) - CURSOR_ITEM_DISTANCE);
        }

    }

}
