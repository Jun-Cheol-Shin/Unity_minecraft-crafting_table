using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        none = -1,
        wood = 1,
        wooden,
        stone,
        iron_ingot,
        gold_ingot,
        diamond_gem,
        carrot,
        fishing,
        paper,
        web,
        web_string,
        compass,
        carrot_fishing,
        stick,
        redstone,
        map,
        empty_map,

        w_sword,
        w_axe,
        w_hoe,
        w_pickaxe,
        w_shovel,

        s_sword,
        s_axe,
        s_hoe,
        s_pickaxe,
        s_shovel,

        i_sword,
        i_axe,
        i_hoe,
        i_pickaxe,
        i_shovel,

        g_sword,
        g_axe,
        g_hoe,
        g_pickaxe,
        g_shovel,

        d_sword,
        d_axe,
        d_hoe,
        d_pickaxe,
        d_shovel
    }


    public Sprite itemicon;
    public int count;
    public ItemType type;


    public void ItemSetting(int _count, ItemType _type)
    {
        count = _count;
        type = _type;
        bool flag = false;
        for(int i = 0; i < ItemManager.GetInstance.itemAssets.Count; i++)
        {
            if(type.ToString() == ItemManager.GetInstance.itemAssets[i].name)
            {
                itemicon = ItemManager.GetInstance.itemAssets[i];
                transform.GetChild(0).GetComponent<Image>().sprite = itemicon;
                flag = true;
            }
            if(flag)
                break;
        }


        transform.GetChild(1).GetComponent<Text>().text = count.ToString();
    }


    public void resetcounting()
    {
        transform.GetChild(1).GetComponent<Text>().text = count.ToString();
    }
}
