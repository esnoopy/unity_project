using UnityEngine;
using UnityEngine.UI; // Required for Canvas.SetActive

public class ShopA_Manager : MonoBehaviour
{
    [SerializeField] private ShopUIManager shopUIManager;
    [SerializeField] private GameObject shopCanvas; 
    [SerializeField] private GameObject promptCanvas; 

    [SerializeField] public ShopUIManager.Shop[] shopAItems; 

    private bool playerInTrigger = false; 

    void Start()
    {
        if (shopCanvas != null)
        {
            shopCanvas.SetActive(false);
        }
        if (promptCanvas != null)
        {
            promptCanvas.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInTrigger)
        {
            // If 'E' is pressed and the prompt is active, show the shop
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                if (promptCanvas != null && promptCanvas.activeSelf) 
                {
                    Debug.Log("E pressed, opening shop!");
                    promptCanvas.SetActive(false); 

                    if (shopUIManager != null)
                    {
                        shopUIManager.DisplayShopItems(shopAItems);
                    }
                    if (shopCanvas != null)
                    {
                        shopCanvas.SetActive(true);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Player entered Shop A trigger!");
            playerInTrigger = true;
            if (promptCanvas != null)
            {
                promptCanvas.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited Shop A trigger!");
            playerInTrigger = false;
            if (shopUIManager != null)
            {
                shopUIManager.ClearShopItems();
            }
            if (shopCanvas != null)
            {
                shopCanvas.SetActive(false);
            }
            if (promptCanvas != null)
            {
                promptCanvas.SetActive(false);
            }
        }
    }
}