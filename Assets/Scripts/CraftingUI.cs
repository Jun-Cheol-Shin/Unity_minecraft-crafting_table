using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CraftingUI : Singleton<CraftingUI>
{
    // 3x3 제작 배열
    private Transform[,] slotArray;
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
                    if (CraftManager.GetInstance.getItem(i, j).count == 0)
                    {
                        CraftManager.GetInstance.DeleteItem(i, j);
                        Destroy(slotArray[i, j].GetChild(0).gameObject);
                    }
                }
            }
        }
    }

    public void OutputCheck()
    {
        // 아웃풋 슬롯에 나올 아이템을 정해준다.
        outputType = CraftManager.GetInstance.GetRecipeOutput();
        // 아무 타입 없다면 함수 탈출
        if (outputType == Item.ItemType.none)
        {
            if (outputSlot.childCount > 0) Destroy(outputSlot.GetChild(0).gameObject);
            return;
        }

        int count = GetItemCount(outputType);

        // 아웃풋 슬롯에 이미 아이템이 있다면..
        if (outputSlot.childCount > 0)
        {
            Destroy(outputSlot.GetChild(0).gameObject);
            CreateItem(count);
        }
        // 슬롯에 아이템이 없다면...
        else if(outputSlot.childCount == 0)
        {
            CreateItem(count);
        }
    }


    // 크래프팅 3x3 슬롯에 아이템이 들어가거나 빠진 경우 슬롯 자리에 맞는 배열 자리에 아이템을 추가
    public void CheckItem()
    {
        for(int i = 0; i < slotArray.GetLength(0); ++i)
        {
            for(int j = 0; j < slotArray.GetLength(1); ++j)
            {
                if (slotArray[i, j].childCount > 0)
                {
                    Item item = slotArray[i, j].GetChild(0).GetComponent<Item>();
                    if(item) CraftManager.GetInstance.setItem(item, i, j);
                }
                else CraftManager.GetInstance.DeleteItem(i, j);
            }
        }

        OutputCheck();
    }

}
