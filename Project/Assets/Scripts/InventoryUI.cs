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
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        if (slots == null || slots.Length == 0)
        {
            InitializeInventorySlots(Inventory.instance.space);
        }

        if (Inventory.instance != null)
        {
            Inventory.instance.onItemChangedCallback += UpdateInventoryUI;
        }
        if (refundButton != null)
            refundButton.onClick.AddListener(RefundSelectedItem);

        // Initialize player money
        playerMoney = 100; // Set initial money to 100
        UpdateMoneyText(); // Display initial money

        Inventory.instance.onItemChangedCallback += UpdateInventoryUI;
    }

    public void AddItemToInventoryUI(Sprite itemSprite, int itemMoney)
    {

        // Check if player has enough money
        if (playerMoney < itemMoney)
        {
            Debug.Log("Not enough money to purchase this item!");
            return; 
        }

        // Subtract money
        AddMoney(-itemMoney); // Subtract the cost
        
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
        Debug.Log("Updating Inventory UI.");

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
            if (slots[i] == null)
            {
                Debug.LogError("InventorySlot prefab does not have an InventorySlot component!");
            }
        }
    }
    
    public void SelectItem(Item item)
    {
        selectedItem = item;
        Debug.Log("Selected item: " + item.name);
    }

    public void RefundSelectedItem()
    {
        if (selectedItem != null)
        {
            Inventory.instance.Remove(selectedItem);
            AddMoney(selectedItem.moneyValue);
            selectedItem = null;
            UpdateInventoryUI(); // Make sure the UI reflects the updated inventory
        }
    }

}