using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : Singleton<InventoryUI>
{

    [SerializeField]
    private Transform[,] slotArray;

    [SerializeField]
    private int array_x = 3;
    [SerializeField]
    private int array_y = 10;

    public void InventorySlotInit()
    {
        slotArray = new Transform[array_x, array_y];
        int count = 0;

        if (transform.childCount == array_x * array_y)
        {
            for (int i = 0; i < array_x; i++)
            {
                for (int j = 0; j < array_y; j++)
                {
                    slotArray[i, j] = transform.GetChild(count++).GetComponent<Transform>();
                }
            }
        }
    }

    public Transform GetEmptySlot()
    {
        for(int i = 0; i < array_x; ++i)
        {
            for(int j = 0; j < array_y; ++j)
            {
                if(slotArray[i, j].childCount == 0)
                {
                    return slotArray[i, j];
                }
            }
        }

        return null;
    }

    public void AddItem(int _count, Item.ItemType _type)
    {
        Transform emptySlot = GetEmptySlot();
        if (emptySlot == null) return;

        GameObject item = Resources.Load("prefab") as GameObject;
        item.GetComponent<Item>().ItemSetting(_count, _type);

        GameObject insitem = Instantiate(item);
        insitem.name = item.GetComponent<Item>().type.ToString();

        insitem.transform.SetParent(emptySlot);
        insitem.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }

 
}
