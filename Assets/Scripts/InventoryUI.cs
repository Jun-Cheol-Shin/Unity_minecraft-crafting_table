using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private Transform[,] slotArray;

    [SerializeField]
    private ItemManager item_manager;

    public int array_x;
    public int array_y;

    public void AddItem(int _count, Item.ItemType _type)
    {
        GameObject item = Resources.Load("prefab") as GameObject;
        item.GetComponent<Item>().ItemSetting(item_manager, _count, _type);

        GameObject insitem = Instantiate(item);
        insitem.name = item.GetComponent<Item>().type.ToString();
        CheckNullSlot(insitem);
    }

    void CheckNullSlot(GameObject item)
    {
        for(int i=0; i<array_x; i++)
        {
            for(int j=0; j<array_y; j++)
            {
                if(slotArray[i,j].childCount == 0)
                {
                    item.transform.SetParent(slotArray[i, j].transform);
                    item.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                    return;
                }
            }
        }
    }

    private void Awake()
    {
        item_manager = item_manager.GetComponent<ItemManager>();
        slotArray = new Transform[array_x, array_y];
        int count = 0;

        if(transform.childCount == array_x * array_y)
        for(int i = 0; i < array_x; i++)
        {
            for(int j = 0; j < array_y; j++)
            {
                slotArray[i, j] = transform.GetChild(count++).GetComponent<Transform>();
            }
        }
    }
}
