using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : Singleton<CraftManager>
{
    [HideInInspector]
    public const int SIZE = 3;

    public Item[,] Array = new Item[SIZE, SIZE];

    public struct recipe
    {
        public Item.ItemType output;
        public Item.ItemType[,] Recipe;
    }


    private List<recipe> recipeList = new List<recipe>();

    private bool isEmpty(int x,int y)
    {
        return Array[x, y] == null ? true : false;
    }

    public Item getItem(int x,int y)
    {
        if(Array[x, y]) return Array[x, y];

        return null;
    }

    public void setItem(Item item, int x,int y)
    {
        Array[x, y] = item;
    }

    public void itemcountUp(int x, int y)
    {
        Item item = getItem(x, y);
        if (item)
        {
            item.count++;
            item.resetcounting();
        }
    }

    public void itemcountDown(int x, int y)
    {
        Item item = getItem(x, y);
        if (item)
        {
            item.count--;
            if (item.count < 0)
            {
                item.count = 0;
                DeleteItem(x, y);
            }
            item.resetcounting();
        }
    }

    public void DeleteItem(int x, int y)
    {
        Array[x, y] = null;
    }

    public bool AddItem(Item item, int x, int y)
    {
        if(isEmpty(x, y))
        {
            setItem(item, x, y);
            return true;
        }
        else
        {
            if(item.type == getItem(x, y).type)
            {
                itemcountUp(x, y);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void recipeInit()
    {
        recipe i_axerecipe = new recipe();
        i_axerecipe.output = Item.ItemType.i_axe;
        i_axerecipe.Recipe = new Item.ItemType[SIZE, SIZE];
        i_axerecipe.Recipe[0, 0] = Item.ItemType.iron_ingot;     i_axerecipe.Recipe[0, 1] = Item.ItemType.iron_ingot;     i_axerecipe.Recipe[0, 2] = Item.ItemType.none;
        i_axerecipe.Recipe[1, 0] = Item.ItemType.iron_ingot;     i_axerecipe.Recipe[1, 1] = Item.ItemType.stick;          i_axerecipe.Recipe[1, 2] = Item.ItemType.none;
        i_axerecipe.Recipe[2, 0] = Item.ItemType.none;           i_axerecipe.Recipe[2, 1] = Item.ItemType.stick;          i_axerecipe.Recipe[2, 2] = Item.ItemType.none;

        recipe fishingrecipe = new recipe();
        fishingrecipe.output = Item.ItemType.fishing;
        fishingrecipe.Recipe = new Item.ItemType[SIZE, SIZE];
        fishingrecipe.Recipe[0, 0] = Item.ItemType.none;     fishingrecipe.Recipe[0, 1] = Item.ItemType.none;       fishingrecipe.Recipe[0, 2] = Item.ItemType.stick;
        fishingrecipe.Recipe[1, 0] = Item.ItemType.none;     fishingrecipe.Recipe[1, 1] = Item.ItemType.stick;      fishingrecipe.Recipe[1, 2] = Item.ItemType.web_string;
        fishingrecipe.Recipe[2, 0] = Item.ItemType.stick;    fishingrecipe.Recipe[2, 1] = Item.ItemType.none;       fishingrecipe.Recipe[2, 2] = Item.ItemType.web_string;


        recipe cfrecipe = new recipe();
        cfrecipe.output = Item.ItemType.carrot_fishing;
        cfrecipe.Recipe = new Item.ItemType[SIZE, SIZE];
        cfrecipe.Recipe[0, 0] = Item.ItemType.none;         cfrecipe.Recipe[0, 1] = Item.ItemType.none;         cfrecipe.Recipe[0, 2] = Item.ItemType.none;
        cfrecipe.Recipe[1, 0] = Item.ItemType.fishing;      cfrecipe.Recipe[1, 1] = Item.ItemType.none;         cfrecipe.Recipe[1, 2] = Item.ItemType.none;
        cfrecipe.Recipe[2, 0] = Item.ItemType.none;         cfrecipe.Recipe[2, 1] = Item.ItemType.carrot;       cfrecipe.Recipe[2, 2] = Item.ItemType.none;


        recipe compassrecipe = new recipe();
        compassrecipe.output = Item.ItemType.compass;
        compassrecipe.Recipe = new Item.ItemType[SIZE, SIZE];
        compassrecipe.Recipe[0, 0] = Item.ItemType.none;            compassrecipe.Recipe[0, 1] = Item.ItemType.iron_ingot;          compassrecipe.Recipe[0, 2] = Item.ItemType.none;
        compassrecipe.Recipe[1, 0] = Item.ItemType.iron_ingot;      compassrecipe.Recipe[1, 1] = Item.ItemType.redstone;            compassrecipe.Recipe[1, 2] = Item.ItemType.iron_ingot;
        compassrecipe.Recipe[2, 0] = Item.ItemType.none;            compassrecipe.Recipe[2, 1] = Item.ItemType.iron_ingot;          compassrecipe.Recipe[2, 2] = Item.ItemType.none;


        recipe maprecipe = new recipe();
        maprecipe.output = Item.ItemType.empty_map;
        maprecipe.Recipe = new Item.ItemType[SIZE, SIZE];
        maprecipe.Recipe[0, 0] = Item.ItemType.paper;         maprecipe.Recipe[0, 1] = Item.ItemType.paper;        maprecipe.Recipe[0, 2] = Item.ItemType.paper;
        maprecipe.Recipe[1, 0] = Item.ItemType.paper;         maprecipe.Recipe[1, 1] = Item.ItemType.compass;      maprecipe.Recipe[1, 2] = Item.ItemType.paper;
        maprecipe.Recipe[2, 0] = Item.ItemType.paper;         maprecipe.Recipe[2, 1] = Item.ItemType.paper;        maprecipe.Recipe[2, 2] = Item.ItemType.paper;


        recipe webstringrecipe = new recipe();
        webstringrecipe.output = Item.ItemType.web_string;
        webstringrecipe.Recipe = new Item.ItemType[SIZE, SIZE];
        webstringrecipe.Recipe[0, 0] = Item.ItemType.none;         webstringrecipe.Recipe[0, 1] = Item.ItemType.none;        webstringrecipe.Recipe[0, 2] = Item.ItemType.none;
        webstringrecipe.Recipe[1, 0] = Item.ItemType.none;         webstringrecipe.Recipe[1, 1] = Item.ItemType.web;         webstringrecipe.Recipe[1, 2] = Item.ItemType.none;
        webstringrecipe.Recipe[2, 0] = Item.ItemType.none;         webstringrecipe.Recipe[2, 1] = Item.ItemType.none;        webstringrecipe.Recipe[2, 2] = Item.ItemType.none;

        recipe woodenrecipe = new recipe();
        woodenrecipe.output = Item.ItemType.wooden;
        woodenrecipe.Recipe = new Item.ItemType[SIZE, SIZE];
        woodenrecipe.Recipe[0, 0] = Item.ItemType.none;         woodenrecipe.Recipe[0, 1] = Item.ItemType.none;        woodenrecipe.Recipe[0, 2] = Item.ItemType.none;
        woodenrecipe.Recipe[1, 0] = Item.ItemType.none;         woodenrecipe.Recipe[1, 1] = Item.ItemType.wood;         woodenrecipe.Recipe[1, 2] = Item.ItemType.none;
        woodenrecipe.Recipe[2, 0] = Item.ItemType.none;         woodenrecipe.Recipe[2, 1] = Item.ItemType.none;        woodenrecipe.Recipe[2, 2] = Item.ItemType.none;


        recipe stickrecipe = new recipe();
        stickrecipe.output = Item.ItemType.stick;
        stickrecipe.Recipe = new Item.ItemType[SIZE, SIZE];
        stickrecipe.Recipe[0, 0] = Item.ItemType.none;         stickrecipe.Recipe[0, 1] = Item.ItemType.none;          stickrecipe.Recipe[0, 2] = Item.ItemType.none;
        stickrecipe.Recipe[1, 0] = Item.ItemType.none;         stickrecipe.Recipe[1, 1] = Item.ItemType.wooden;        stickrecipe.Recipe[1, 2] = Item.ItemType.none;
        stickrecipe.Recipe[2, 0] = Item.ItemType.none;         stickrecipe.Recipe[2, 1] = Item.ItemType.wooden;        stickrecipe.Recipe[2, 2] = Item.ItemType.none;


        recipe i_pickaxerecipe = new recipe();
        i_pickaxerecipe.output = Item.ItemType.i_pickaxe;
        i_pickaxerecipe.Recipe = new Item.ItemType[SIZE, SIZE];
        i_pickaxerecipe.Recipe[0, 0] = Item.ItemType.iron_ingot;   i_pickaxerecipe.Recipe[0, 1] = Item.ItemType.iron_ingot;     i_pickaxerecipe.Recipe[0, 2] = Item.ItemType.iron_ingot;
        i_pickaxerecipe.Recipe[1, 0] = Item.ItemType.none;         i_pickaxerecipe.Recipe[1, 1] = Item.ItemType.stick;          i_pickaxerecipe.Recipe[1, 2] = Item.ItemType.none;
        i_pickaxerecipe.Recipe[2, 0] = Item.ItemType.none;         i_pickaxerecipe.Recipe[2, 1] = Item.ItemType.stick;          i_pickaxerecipe.Recipe[2, 2] = Item.ItemType.none;

        recipe i_swordrecipe = new recipe();
        i_swordrecipe.output = Item.ItemType.i_sword;
        i_swordrecipe.Recipe = new Item.ItemType[SIZE, SIZE];
        i_swordrecipe.Recipe[0, 0] = Item.ItemType.none;         i_swordrecipe.Recipe[0, 1] = Item.ItemType.iron_ingot;  i_swordrecipe.Recipe[0, 2] = Item.ItemType.none;
        i_swordrecipe.Recipe[1, 0] = Item.ItemType.none;         i_swordrecipe.Recipe[1, 1] = Item.ItemType.iron_ingot;  i_swordrecipe.Recipe[1, 2] = Item.ItemType.none;
        i_swordrecipe.Recipe[2, 0] = Item.ItemType.none;         i_swordrecipe.Recipe[2, 1] = Item.ItemType.stick;       i_swordrecipe.Recipe[2, 2] = Item.ItemType.none;

        recipe i_hoerecipe = new recipe();
        i_hoerecipe.output = Item.ItemType.i_hoe;
        i_hoerecipe.Recipe = new Item.ItemType[SIZE, SIZE];
        i_hoerecipe.Recipe[0, 0] = Item.ItemType.iron_ingot;   i_hoerecipe.Recipe[0, 1] = Item.ItemType.iron_ingot;  i_hoerecipe.Recipe[0, 2] = Item.ItemType.none;
        i_hoerecipe.Recipe[1, 0] = Item.ItemType.none;         i_hoerecipe.Recipe[1, 1] = Item.ItemType.stick;       i_hoerecipe.Recipe[1, 2] = Item.ItemType.none;
        i_hoerecipe.Recipe[2, 0] = Item.ItemType.none;         i_hoerecipe.Recipe[2, 1] = Item.ItemType.stick;       i_hoerecipe.Recipe[2, 2] = Item.ItemType.none;


        recipe i_shovelrecipe = new recipe();
        i_shovelrecipe.output = Item.ItemType.i_shovel;
        i_shovelrecipe.Recipe = new Item.ItemType[SIZE, SIZE];
        i_shovelrecipe.Recipe[0, 0] = Item.ItemType.none;         i_shovelrecipe.Recipe[0, 1] = Item.ItemType.iron_ingot;  i_shovelrecipe.Recipe[0, 2] = Item.ItemType.none;
        i_shovelrecipe.Recipe[1, 0] = Item.ItemType.none;         i_shovelrecipe.Recipe[1, 1] = Item.ItemType.stick;       i_shovelrecipe.Recipe[1, 2] = Item.ItemType.none;
        i_shovelrecipe.Recipe[2, 0] = Item.ItemType.none;         i_shovelrecipe.Recipe[2, 1] = Item.ItemType.stick;       i_shovelrecipe.Recipe[2, 2] = Item.ItemType.none;

        recipeList.Add(fishingrecipe);
        recipeList.Add(i_axerecipe);
        recipeList.Add(cfrecipe);
        recipeList.Add(compassrecipe);
        recipeList.Add(maprecipe);
        recipeList.Add(webstringrecipe);
        recipeList.Add(woodenrecipe);
        recipeList.Add(stickrecipe);
        recipeList.Add(i_pickaxerecipe);
        recipeList.Add(i_swordrecipe);
        recipeList.Add(i_hoerecipe);
        recipeList.Add(i_shovelrecipe);
    }

    bool CheckRecipe(int index)
    {
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {
                if (recipeList[index].Recipe[i, j] != Item.ItemType.none &&
                    (isEmpty(i, j) || getItem(i, j).type != recipeList[index].Recipe[i, j]))
                {
                    return false;
                }

                else if (recipeList[index].Recipe[i, j] == Item.ItemType.none && !isEmpty(i, j))
                {
                    return false;
                }

            }
        }

        return true;
    }

    public Item.ItemType GetRecipeOutput()
    {
        for (int index = 0; index < recipeList.Count; index++)
        {
            if(CheckRecipe(index)) return recipeList[index].output;
        }

        return Item.ItemType.none;
    }
}
