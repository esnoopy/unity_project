/*using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

	public GameObject inventoryUI;	
	public Transform itemsParent;	

	Inventory inventory;	

	void Start ()
	{
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;
	}

	
	void Update ()
	{
		if (Input.GetButtonDown("Inventory"))
		{
			inventoryUI.SetActive(!inventoryUI.activeSelf);
			UpdateUI();
		}
	}

	public void UpdateUI ()
	{
		InventorySlot[] slots = GetComponentsInChildren<InventorySlot>();

		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inventory.items.Count)
			{
				slots[i].AddItem(inventory.items[i]);
			} else
			{
				slots[i].ClearSlot();
			}
		}
	}

}*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent; 
    public GameObject inventorySlotPrefab; 
    public Image inventoryItemImage; 

    InventorySlot[] slots; 

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
    }

    public void AddItemToInventoryUI(Sprite itemSprite)
    {
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
}