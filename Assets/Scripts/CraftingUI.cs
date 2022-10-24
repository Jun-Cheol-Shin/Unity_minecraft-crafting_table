using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CraftingUI : Singleton<CraftingUI>
{

    // 아웃풋 슬롯
    public Transform outputSlot;

    public void CraftSlotInit()
    {
        slotArray = new Transform[CraftManager.SIZE, CraftManager.SIZE];
        int count = 0;

        if (transform.childCount >= CraftManager.SIZE * CraftManager.SIZE)
        {
            for (int i = 0; i < CraftManager.SIZE; i++)
            {
                for (int j = 0; j < CraftManager.SIZE; j++)
                {
                    slotArray[i, j] = transform.GetChild(count++);
                }
            }

            outputSlot = transform.GetChild(count);
        }
    }
    int GetItemCount(Item.ItemType type)
    {
        switch (outputType)
        {
            case Item.ItemType.web_string:
                return 9;

            case Item.ItemType.stick:
                return 4;

            case Item.ItemType.wooden:
                return 4;

            default:
                return 1;
        }
    }
    void CreateItem(int count)
    {
        GameObject item = Resources.Load("prefab") as GameObject;
        item.GetComponent<Item>().ItemSetting(count, outputType);
        GameObject insitem = Instantiate(item);
        insitem.name = item.GetComponent<Item>().type.ToString();
        insitem.transform.SetParent(outputSlot.transform);
        insitem.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }



    private Item.ItemType outputType;

    // 제작 후 아이템을 가져가면 재료를 소모시켜주는 함수
    public void CraftMaterialCounting()
    {
        for (int i = 0; i < CraftManager.SIZE; i++)
        {
            for (int j = 0; j < CraftManager.SIZE; j++)
            {
                if (slotArray[i, j].childCount > 0)
                {
                    CraftManager.GetInstance.itemcountDown(i, j);
                    Item item = CraftManager.GetInstance.getItem(i, j);
                    if (item && item.count <= 0)
                    {
                        CraftManager.GetInstance.DeleteItem(i, j);
                        slotArray[i, j].GetChild(0).gameObject.SetActive(false);
                        Destroy(slotArray[i, j].GetChild(0).gameObject);
                    }
                }
            }
        }
    }

    public void OutputCheck()
    {
        // OUTPUT 슬롯에 나올 아이템을 정해준다.
        outputType = CraftManager.GetInstance.GetRecipeOutput();
        // OUTPUT 슬롯에 나올 아이템이 없다면 함수 탈출
        if (outputType == Item.ItemType.none)
        {
            if (outputSlot.childCount > 0) Destroy(outputSlot.GetChild(0).gameObject);
            return;
        }

        int count = GetItemCount(outputType);

        // 아웃풋 슬롯에 이미 아이템이 있다면.. 타입이 다르면 삭제 후 생성
        if (outputSlot.childCount > 0)
        {
            Item slotItem = outputSlot.GetChild(0).gameObject.GetComponent<Item>();
            if(slotItem && slotItem.type != outputType)
            {
                Destroy(outputSlot.GetChild(0).gameObject);
                CreateItem(count);
            }
        }
        // 슬롯에 아이템이 없다면...
        else if(outputSlot.childCount == 0)
        {
            CreateItem(count);
        }
    }


    // 크래프팅 3x3 슬롯에 아이템이 들어가거나 빠진 경우 슬롯 자리에 맞는 배열 자리에 아이템을 추가
    // 3x3 제작 배열
    private Transform[,] slotArray;
    public void CheckItem()
    {
        for(int i = 0; i < CraftManager.SIZE; ++i)
        {
            for(int j = 0; j < CraftManager.SIZE; ++j)
            {
                // 슬롯 안에 아이템이 있다면...
                if (slotArray[i, j].childCount > 0 && slotArray[i, j].GetChild(0).gameObject.activeSelf)
                {
                    // 아이템 클래스를 제작 매니저에 등록
                    Item item = slotArray[i, j].GetChild(0).GetComponent<Item>();
                    if(item && item.count > 0) CraftManager.GetInstance.setItem(item, i, j);
                }
                // 아이템 삭제
                else CraftManager.GetInstance.DeleteItem(i, j);
            }
        }
        // 제작대에서 나올 아이템을 체크한다.
        OutputCheck();
    }

}
