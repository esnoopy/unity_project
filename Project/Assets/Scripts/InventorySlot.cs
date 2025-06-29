using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlot : MonoBehaviour {

	public Image itemImage;
	private Item currentItem;

    //Item item;
    
    void Awake()
    {
        if (itemImage == null)
        {
            Transform itemImageTransform = transform.Find("ItemButton/ItemImage"); // Adjust path if needed
            if (itemImageTransform != null)
            {
                itemImage = itemImageTransform.GetComponent<Image>();
            }
            else
            {
                Debug.LogError("ItemImage not found in InventorySlot prefab. Please assign it or check the path.");
            }
        }
        ClearSlot(); 
    }
    public void AddItem(Item item)
    {
        
        currentItem = item;
        if (item.icon != null)
        {
            itemImage.sprite = item.icon;
            itemImage.enabled = true;
        }
        /*item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;*/
    }
    public void ClearSlot()
    {
        /*item = null;

		icon.sprite = null;
		icon.enabled = false;
		removeButton.interactable = false;*/
        currentItem = null;
        itemImage.sprite = null;
        itemImage.enabled = false;
	}

	// Use the item
	public void UseItem ()
	{
		if (currentItem != null)
		{
			currentItem.Use();
		}
	}

}