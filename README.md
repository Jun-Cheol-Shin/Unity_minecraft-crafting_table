#                 #### 마인크래프트 제작대 ####
![캡처화면2](https://user-images.githubusercontent.com/77636255/115140267-720aef80-a071-11eb-984f-d29fa7cadb50.PNG)
# 목차
* [소개](#소개)
* [구현 내용](#구현-내용)

## 소개
* Unity 3D 2018 4.14 Ver.
* 2022/10/24 마우스 버그 수정과 전체적인 코드 리팩토링

## 구현 내용
* [아이템 슬롯](#아이템-슬롯)
* [마우스 좌클릭](#마우스-좌클릭)
* [마우스 우클릭](#마우스-우클릭)
* [아이템 제작](#아이템-제작)
* [전체적인 코드의 흐름](#전체적인-코드의-흐름)
___

### 아이템 슬롯
![tempsnip](https://user-images.githubusercontent.com/77636255/197442301-f188d6ae-b611-425a-85fb-50a0fc33c88a.png)
* 3종류의 아이템 슬롯을 ENUM으로 제작해 변수로 갖도록 했습니다.
* DROP = 인벤토리, CRAFT = 제작대, OUTPUT = 제작해서 나오는 아이템 슬롯
```
    public enum SlotType
    {
        DROP =  1,
        CRAFT = 2,
        OUTPUT = 3
    };
    public SlotType slotType = SlotType.DROP;
```
* 유니티 마우스 터치, 클릭 이벤트를 이용해 slot과 아이템의 정보를 가져도록 했습니다.
```
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
```
```
 public void OnPointerDown(PointerEventData _eventData)
 {
    if(ClickManager.GetInstance.clickItem == null)
    {
        // 아이템의 정보를 가져온다.
        ClickManager.GetInstance.clickItem = gameObject; 
    }
 }
```
___

### 마우스 좌클릭
![ezgif com-gif-maker](https://user-images.githubusercontent.com/77636255/197443535-166bb1ab-2d0d-480c-ad85-19c18d57e358.gif)
* 커서에 아이템이 없는 경우 rectTransform 변수를 저장해 아이템을 끌고 다닐 수 있도록 합니다.
```
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
                                        .
                                        .
                                        .
                                        
    // 아이템이 커서를 따라가도록 Update문에서 실행되는 코드
    if(cursorOnItem && clickItem && rectTransform)
    {
        rectTransform.anchoredPosition =
            new Vector2((Input.mousePosition.x - canvas.transform.position.x) + CURSOR_ITEM_DISTANCE,
            (Input.mousePosition.y - canvas.transform.position.y) - CURSOR_ITEM_DISTANCE);
    }
```
![ezgif com-gif-maker (1)](https://user-images.githubusercontent.com/77636255/197445094-cb383842-2b4b-4c4e-bbc7-4a6c73733dc0.gif)
* 커서에 아이템이 있는 경우에는 분기점이 많이 나뉩니다.
* 슬롯이 비었거나 슬롯이 아닌 곳에 둔다면 간단하게 비어있는 슬롯의 자식으로 넣어서 해결했습니다.
* 밑 코드는 슬롯 안에 아이템이 있을 경우, 같은 타입의 아이템이거나 혹은 완전히 다른 아이템일 경우의 코드입니다.
```
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
```
![ezgif com-gif-maker (2)](https://user-images.githubusercontent.com/77636255/197445509-51a5c780-d0ed-4b6d-8d5e-6c01c8678784.gif)
```
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
```
