using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : Singleton<Manager>
{
    // Start is called before the first frame update

    private void Awake()
    {
        CraftingUI.GetInstance.CraftSlotInit();
        InventoryUI.GetInstance.InventorySlotInit();
    }

    void Start()
    {
        InventoryUI.GetInstance.AddItem(80, Item.ItemType.iron_ingot);
        InventoryUI.GetInstance.AddItem(5,  Item.ItemType.carrot);
        InventoryUI.GetInstance.AddItem(10, Item.ItemType.redstone);
        InventoryUI.GetInstance.AddItem(20, Item.ItemType.paper);
        InventoryUI.GetInstance.AddItem(5,  Item.ItemType.web);
        InventoryUI.GetInstance.AddItem(30, Item.ItemType.wood);
        InventoryUI.GetInstance.AddItem(50, Item.ItemType.gold_ingot);
        InventoryUI.GetInstance.AddItem(50, Item.ItemType.diamond_gem);
        InventoryUI.GetInstance.AddItem(50, Item.ItemType.diamond_gem);
        InventoryUI.GetInstance.AddItem(50, Item.ItemType.diamond_gem);
        InventoryUI.GetInstance.AddItem(50, Item.ItemType.diamond_gem);
        InventoryUI.GetInstance.AddItem(50, Item.ItemType.diamond_gem);


        CraftManager.GetInstance.recipeInit();
        ClickManager.GetInstance.CanvasInit();
    }

    private void Update()
    {
        ClickManager.GetInstance.ClickFunc();
    }

}
