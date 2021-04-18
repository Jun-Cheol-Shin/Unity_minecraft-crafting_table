using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private InventoryUI inven;
    [SerializeField]
    private CraftingUI craft;

    [SerializeField]
    private ClickManager clickmanager;

    // Start is called before the first frame update
    void Start()
    {
        inven.AddItem(80, Item.ItemType.iron_ingot);
        inven.AddItem(5,  Item.ItemType.carrot);
        inven.AddItem(10, Item.ItemType.redstone);
        inven.AddItem(20, Item.ItemType.paper);
        inven.AddItem(5,  Item.ItemType.web);
        inven.AddItem(30, Item.ItemType.wood);
        //inven.AddItem(50, Item.ItemType.gold_ingot);
        //inven.AddItem(50, Item.ItemType.diamond_gem);
    }

    private void Update()
    {
        clickmanager.clickFunction();
        craft.CheckItem();
        craft.outputCheck();
        craft.recipeCountCheck(clickmanager);
    }

}
