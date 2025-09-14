using UnityEngine;
using UnityEngine.UI; 
using TMPro; // Add this line

public class ArmorEquipper : MonoBehaviour
{
    // These references should be assigned in the Inspector
    public GameObject player;
    public GameObject armorPrefab;
    public GameObject swordPrefab;
    public GameObject helmetPrefab;
    
    // Changed the variable type to TextMeshProUGUI
    public GameObject equippingButtonsUI; 
    public TextMeshProUGUI promptText; 

    private bool armorEquipped = false;
    private bool swordEquipped = false;
    private bool helmetEquipped = false;
    private bool isPlayerInZone = false; 

    private GameObject armorInstance;
    private GameObject swordInstance;
    private GameObject helmetInstance;
    
    void Start()
    {
        // This is where the UI elements are hidden at the start of the game
        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
        if (equippingButtonsUI != null)
        {
            equippingButtonsUI.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerInZone = true;
            Debug.Log("Player entered the equipping zone.");
            
            // This is where the prompt text is made visible.
            // The text content is no longer being changed here.
            if (promptText != null)
            {
                promptText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerInZone = false;
            Debug.Log("Player exited the equipping zone.");
            
            // This is where all UI is hidden when the player leaves the zone
            if (promptText != null)
            {
                promptText.gameObject.SetActive(false);
            }
            if (equippingButtonsUI != null)
            {
                equippingButtonsUI.SetActive(false);
            }
        }
    }

    void Update()
    {
        // Adding Debug logs to help troubleshoot
        if (isPlayerInZone)
        {
            Debug.Log("Update: isPlayerInZone is TRUE.");
        }
        else
        {
            Debug.Log("Update: isPlayerInZone is FALSE.");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Update: 'E' key was pressed.");
        }

        if (isPlayerInZone && Input.GetKeyDown(KeyCode.E))
        {
            if (equippingButtonsUI != null && !equippingButtonsUI.activeSelf)
            {
                // This is where the buttons UI is shown and the prompt is hidden
                equippingButtonsUI.SetActive(true);
                promptText.gameObject.SetActive(false);
                Debug.Log("Update: UI panel should now be active.");
            }
            else
            {
                // This debug message will help us find out why the UI isn't showing up
                Debug.Log("Update: Failed to activate UI. Reasons could be: UI is already active, or UI reference is null.");
            }
        }
    }

    public void CloseUI()
    {
        // This method is called by the UI button
        if (equippingButtonsUI != null)
        {
            equippingButtonsUI.SetActive(false);
        }

        if (isPlayerInZone && promptText != null)
        {
            promptText.gameObject.SetActive(true);
        }
    }

    void ToggleHelmet()
    {
        if (helmetEquipped)
        {
            UnequipHelmet();
            helmetEquipped = false;
        }
        else
        {
            EquipHelmet();
            helmetEquipped = true;
        }
    }

    void ToggleArmor()
    {
        if (armorEquipped)
        {
            UnequipArmor();
            armorEquipped = false;
        }
        else
        {
            EquipArmor();
            armorEquipped = true;
        }
    }

    void ToggleSword()
    {
        if (swordEquipped)
        {
            UnequipSword();
            swordEquipped = false;
        }
        else
        {
            EquipSword();
            swordEquipped = true;
        }
    }

    void EquipArmor()
    {
        Transform chest = player.transform.Find("Armature/Root_M/Spine1_M/Spine2_M/Chest_M");

        if (chest == null)
        {
            Debug.LogError("Chest bone not found. Check the path!");
            return;
        }

        armorInstance = Instantiate(armorPrefab);
        armorInstance.transform.SetParent(chest);
        armorInstance.transform.localPosition = new Vector3(0.0252f, -0.0168f, 0.0112f);
        armorInstance.transform.localRotation = Quaternion.Euler(-82.9f, 90f, 0f);
        armorInstance.transform.localScale = new Vector3(0.4577152f, 0.4339085f, 0.5277648f);
    }

    void UnequipArmor()
    {
        if (armorInstance != null)
        {
            Destroy(armorInstance);
            armorInstance = null;
        }
    }

    void EquipSword()
    {
        Transform hand = player.transform.Find("Armature/Root_M/Spine1_M/Spine2_M/Chest_M/Scapula_R/Shoulder_R/Elbow_R/Wrist_R");

        if (hand == null)
        {
            Debug.LogError("Right hand bone not found. Update the path!");
            return;
        }

        swordInstance = Instantiate(swordPrefab);
        swordInstance.transform.SetParent(hand);
        swordInstance.transform.localPosition = new Vector3(-0.132f, 0.07f, -0.05f);
        swordInstance.transform.localRotation = Quaternion.Euler(-1.585f, -10.167f, -174.308f);
        swordInstance.transform.localScale = new Vector3(0.3f, 0.2f, 0.4f);
    }

    void UnequipSword()
    {
        if (swordInstance != null)
        {
            Destroy(swordInstance);
            swordInstance = null;
        }
    }

    void EquipHelmet()
    {
        Transform head = player.transform.Find("Armature/Root_M/Spine1_M/Spine2_M/Chest_M/Neck_M/Head_M");

        if (head == null)
        {
            Debug.LogError("Head bone not found. Check the path!");
            return;
        }

        helmetInstance = Instantiate(helmetPrefab);
        helmetInstance.transform.SetParent(head);
        helmetInstance.transform.localPosition = new Vector3(0.123f, 0.055f, 0.023f);
        helmetInstance.transform.localRotation = Quaternion.Euler(-86.13f, 180f, -94.6f);
        helmetInstance.transform.localScale = new Vector3(0.5f, 0.6021414f, 0.57f);
    }

    void UnequipHelmet()
    {
        if (helmetInstance != null)
        {
            Destroy(helmetInstance);
            helmetInstance = null;
        }
    }
}
