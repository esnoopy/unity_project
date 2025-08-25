// InventoryUI.cs

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventorySlotPrefab;

    InventorySlot[] slots;
    public TextMeshProUGUI playerMoneyText;
    private int playerMoney = 0;

    public Button refundButton;
    private Item selectedItem = null;
    
    // New: Reference to the Canvas Group component
    public CanvasGroup inventoryCanvasGroup;

    public void AddMoney(int amount)
    {
        playerMoney += amount;
        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        if (playerMoneyText != null)
            playerMoneyText.text = "Money: " + playerMoney;
    }

    void Start()
    {
        if (Inventory.instance == null)
        {
            Debug.LogError("Inventory instance not found. Make sure the Inventory script is in the scene.");
            return;
        }

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        if (slots == null || slots.Length == 0)
        {
            InitializeInventorySlots(Inventory.instance.space);
        }

        Inventory.instance.onItemChangedCallback += UpdateInventoryUI;
        if (refundButton != null)
            refundButton.onClick.AddListener(RefundSelectedItem);

        playerMoney = 100;
        UpdateMoneyText();
    }
    
    // New: Public method to toggle the UI's visibility
    public void ToggleInventoryUI(bool show)
    {
        if (inventoryCanvasGroup != null)
        {
            inventoryCanvasGroup.alpha = show ? 1f : 0f;
            inventoryCanvasGroup.blocksRaycasts = show;
            inventoryCanvasGroup.interactable = show; // This is the missing part
        }
    }

    public void AddItemToInventoryUI(Sprite itemSprite, int itemMoney)
    {
        if (playerMoney < itemMoney)
        {
            Debug.Log("Not enough money to purchase this item!");
            return; 
        }

        AddMoney(-itemMoney);
        
        if (slots == null)
        {
            Debug.LogError("Inventory slots are null. The UI was not properly initialized.");
            return;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemImage.sprite == null)
            {
                slots[i].itemImage.sprite = itemSprite;
                slots[i].itemImage.enabled = true;
                Debug.Log($"Added item sprite to inventory slot {i}");
                return;
            }
        }
        Debug.LogWarning("No empty inventory slots available to display the item sprite.");
    }

    void UpdateInventoryUI()
    {
        if (slots == null || slots.Length == 0)
        {
             Debug.LogWarning("InventoryUI slots not initialized.");
             return;
        }

        foreach (InventorySlot slot in slots)
        {
            slot.ClearSlot();
        }

        for (int i = 0; i < Inventory.instance.items.Count; i++)
        {
            if (i < slots.Length)
            {
                slots[i].AddItem(Inventory.instance.items[i]);
            }
        }
    }

    private void InitializeInventorySlots(int numberOfSlots)
    {
        slots = new InventorySlot[numberOfSlots];
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject slotGO = Instantiate(inventorySlotPrefab, itemsParent);
            slots[i] = slotGO.GetComponent<InventorySlot>();
        }
    }
    
    public void SelectItem(Item item)
    {
        selectedItem = item;
    }

    public void RefundSelectedItem()
    {
        if (selectedItem != null)
        {
            Inventory.instance.Remove(selectedItem);
            AddMoney(selectedItem.moneyValue);
            selectedItem = null;
            UpdateInventoryUI();
        }
    }
}