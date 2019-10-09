using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftingWindow : MonoBehaviour
{
    [Header("Refernces")]
    [SerializeField] CraftingRecipeUI recipeUIPrefab;
    [SerializeField] RectTransform RecipeUIParent;
    [SerializeField] List<CraftingRecipeUI> craftingRecipeUIs;

    [Header("Public Variables")]
    public ItemContainer itemContainer;
    public List<CraftingRecipe> CraftingRecipes;

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;

    private void OnValidate()
    {

        Init();
    }

    void Start()
    {
        Init();

        foreach (CraftingRecipeUI craftingRecipeUI in craftingRecipeUIs)
        {
            craftingRecipeUI.OnPointerEnterE += OnPointerEnterEvent;
            craftingRecipeUI.OnPointerExitE += OnPointerExitEvent;

            //craftingRecipeUI.OnPointerEnterE += slot => OnPointerEnterEvent(slot);
            //craftingRecipeUI.OnPointerExitE += slot => OnPointerExitEvent(slot);
        }
    }

    private void Init()
    {
        RecipeUIParent.GetComponentsInChildren(includeInactive: true, result: craftingRecipeUIs);
        UpdateCraftingRecipes();
    }

    public void UpdateCraftingRecipes()
    {
        for (int i = 0; i < CraftingRecipes.Count; i++)
        {
            if(craftingRecipeUIs.Count == i)
            {
                craftingRecipeUIs.Add(Instantiate(recipeUIPrefab, RecipeUIParent, false));
            }
            else if(craftingRecipeUIs[i] == null)
            {
                craftingRecipeUIs[i] = Instantiate(recipeUIPrefab, RecipeUIParent, false);
            }

            craftingRecipeUIs[i].itemContainer = itemContainer;
            craftingRecipeUIs[i].Craftingrecipe = CraftingRecipes[i];
        }
        for (int i = CraftingRecipes.Count; i < CraftingRecipes.Count; i++)
        {
            craftingRecipeUIs[i].Craftingrecipe = null;
        }
    }
}
