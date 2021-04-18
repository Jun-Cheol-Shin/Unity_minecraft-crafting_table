using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    // 3x3 제작 배열
    private Transform[,] slotArray;

    private Item.ItemType outputType;
    // 아웃풋 슬롯
    public Transform outputSlot;

    // 제작 체크
    bool craftflag = false;

    [SerializeField]
    private CraftManager craft_manager;
    [SerializeField]
    private ItemManager item_manager;

    private void Awake()
    {
        slotArray = new Transform[CraftManager.SIZE, CraftManager.SIZE];
        int count = 0;

        if(transform.childCount >= CraftManager.SIZE * CraftManager.SIZE)
        {
            for(int i = 0; i < CraftManager.SIZE; i++)
            {
                for(int j = 0; j < CraftManager.SIZE; j++)
                {
                    slotArray[i, j] = transform.GetChild(count++);
                }
            }

            outputSlot = transform.GetChild(count);
        }
    }

    public void recipeCountCheck(ClickManager clickmanager)
    {
        if(!craftflag && clickmanager.clickItem != null && 
            clickmanager.clickItem.GetComponent<Item>().type == outputType)
        {
            for(int i = 0; i < CraftManager.SIZE; i++)
            {
                for(int j = 0; j < CraftManager.SIZE; j++)
                {
                    if(slotArray[i, j].childCount == 1)
                    {
                        craft_manager.itemcountDown(i, j);
                        if(craft_manager.getItem(i, j).count == 0)
                        {
                            Destroy(slotArray[i, j].GetChild(0).gameObject);
                        }
                    }
                }
            }
            craftflag = true;
        }
    }

    public void outputCheck()
    {
        int count = 1;
        outputType = craft_manager.GetRecipeOutput();
        if(outputSlot.childCount > 0)
        {
            if(outputType == Item.ItemType.none)
            {
                Destroy(outputSlot.GetChild(0).gameObject);
            }
            else if(outputType != outputSlot.GetChild(0).GetComponent<Item>().type)
            {
                Destroy(outputSlot.GetChild(0).gameObject);
                switch(outputType)
                {
                    case Item.ItemType.web_string:
                    count = 9;
                    break;

                    case Item.ItemType.stick:
                    count = 4;
                    break;

                    case Item.ItemType.wooden:
                    count = 4;
                    break;

                    default:
                    count = 1;
                    break;
                }
                GameObject item = Resources.Load("prefab") as GameObject;
                item.GetComponent<Item>().ItemSetting(item_manager, count, outputType);
                GameObject insitem = Instantiate(item);
                insitem.name = item.GetComponent<Item>().type.ToString();
                insitem.transform.SetParent(outputSlot.transform);
                insitem.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            }
        }
        else
        {
            if(outputType != Item.ItemType.none)
            {
                switch(outputType)
                {
                    case Item.ItemType.web_string:
                    count = 9;
                    break;

                    case Item.ItemType.stick:
                    count = 4;
                    break;

                    case Item.ItemType.wooden:
                    count = 4;
                    break;

                    default:
                    count = 1;
                    break;
                }
                GameObject item = Resources.Load("prefab") as GameObject;
                item.GetComponent<Item>().ItemSetting(item_manager, count, outputType);
                GameObject insitem = Instantiate(item);
                insitem.name = item.GetComponent<Item>().type.ToString();
                insitem.transform.SetParent(outputSlot.transform);
                insitem.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                craftflag = false;
            }
        }
    }

    public void CheckItem()
    {
        for(int i =0; i < CraftManager.SIZE; i++)
        {
            for(int j=0; j < CraftManager.SIZE; j++)
            {
                if(slotArray[i,j].childCount == 1)
                {
                    Item item =  slotArray[i, j].GetChild(0).GetComponent<Item>();
                    craft_manager.setItem(item, i, j);
                }
                else
                {
                    craft_manager.DeleteItem(i, j);
                }
            }
        }
    }

}
