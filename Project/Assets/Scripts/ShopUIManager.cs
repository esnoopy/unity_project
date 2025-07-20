using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class ButtonExtension
{
    public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate ()
        {
            OnClick(param);
        });
    }
}

public class ShopUIManager : MonoBehaviour
{
    [Serializable]
    public struct Shop
    {
        public string Name;
        public Sprite Icon;
        public Sprite MoneyIcon;
        public int Money;
    }

    [SerializeField] public GameObject shopButtonPrefab;
    [SerializeField] public Transform contentParent;
    private Shop[] allShops;

    [SerializeField] private InventoryUI inventoryUI; 

    void Awake()
    {
        if (contentParent == null)
        {
            
            contentParent = transform;
        }

        
        if (shopButtonPrefab == null && contentParent.childCount > 0)
        {
            
            shopButtonPrefab = contentParent.GetChild(0).gameObject;
            
        }
        else if (shopButtonPrefab == null)
        {
            Debug.LogError("ShopButton prefab is not assigned and could not be found as the first child of the Content object. Please assign it in the Inspector or ensure it's the first child.");
        }

        if (inventoryUI == null)
        {
            inventoryUI = FindObjectOfType<InventoryUI>();
            if (inventoryUI == null)
            {
                Debug.LogWarning("InventoryUI not found in the scene. Please assign it in the Inspector or ensure an InventoryUI script exists in the scene.");
            }
        }
    }

    public void DisplayShopItems(Shop[] itemsToDisplay)
    {
        ClearShopItems(); 
        allShops = itemsToDisplay; 

        if (shopButtonPrefab == null)
        {
            Debug.LogError("ShopButton prefab is not set. Cannot display shop items.");
            return;
        }

        int N = allShops.Length;
        for (int i = 0; i < N; i++)
        {
            
            GameObject g = Instantiate(shopButtonPrefab, contentParent);
            g.SetActive(true); 

            
            g.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = allShops[i].Name;
            g.transform.Find("ItemIcon").GetComponent<Image>().sprite = allShops[i].Icon;
            g.transform.Find("Money").GetComponent<TextMeshProUGUI>().text = allShops[i].Money.ToString();
            g.transform.Find("MoneyIcon").GetComponent<Image>().sprite = allShops[i].MoneyIcon;

            
            g.GetComponent<Button>().AddEventListener(allShops[i], ItemClicked);
        }
    }

    public void ClearShopItems()
    {
        foreach (Transform child in contentParent)
        {
            if (child.gameObject != shopButtonPrefab)
            {
                Destroy(child.gameObject);
            }
        }
        
        allShops = null;
    }

    void ItemClicked(Shop clickedShopItem)
    {
        Debug.Log("Item '" + clickedShopItem.Name + "' clicked in shop.");

        Item newItem = ScriptableObject.CreateInstance<Item>();
        newItem.name = clickedShopItem.Name;
        newItem.icon = clickedShopItem.Icon;
        newItem.moneyValue = clickedShopItem.Money;
        newItem.showInInventory = true;
        // Add it to the inventory
        //Inventory.instance.Add(newItem);

        if (inventoryUI != null)
        {

            inventoryUI.AddItemToInventoryUI(clickedShopItem.Icon, clickedShopItem.Money); 
            Inventory.instance.Add(newItem); // Add it to the inventory

        }
        else
        {
            Debug.LogWarning("InventoryUI reference is null. Cannot add item to inventory UI after shop click.");
        }
    }
}