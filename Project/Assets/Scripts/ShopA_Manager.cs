// Example: ShopA_Manager.cs
using UnityEngine;

public class ShopA_Manager : MonoBehaviour
{
    [SerializeField] private ShopUIManager shopUIManager;
    [SerializeField] public ShopUIManager.Shop[] shopAItems; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Player entered Shop A trigger!");
            if (shopUIManager != null)
            {
                shopUIManager.DisplayShopItems(shopAItems);
                shopUIManager.transform.parent.parent.gameObject.SetActive(true); 
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited Shop A trigger!");
            if (shopUIManager != null)
            {
                shopUIManager.ClearShopItems();
                // You might also want to disable the Canvas/ScrollView here
                shopUIManager.transform.parent.parent.gameObject.SetActive(false);
            }
        }
    }
}