using UnityEngine;

public class ArmorEquipper : MonoBehaviour
{
    public GameObject player;
    public GameObject armorPrefab;
    public GameObject swordPrefab;
    public GameObject helmetPrefab;

    private bool armorEquipped = false;
    private bool swordEquipped = false;
    private bool helmetEquipped = false;
    private bool canEquip = false; // New variable to check if player can equip items

    private GameObject armorInstance;
    private GameObject swordInstance;
    private GameObject helmetInstance;

    // This method is called when another collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is the player
        if (other.gameObject == player)
        {
            canEquip = true;
            Debug.Log("Player entered the equipping zone.");
        }
    }

    // This method is called when another collider exits the trigger
    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider is the player
        if (other.gameObject == player)
        {
            canEquip = false;
            Debug.Log("Player exited the equipping zone.");
        }
    }

    void Update()
    {
        // Only allow equipping if the player is in the correct zone
        if (canEquip)
        {
            // Toggle helmet with "J"
            if (Input.GetKeyDown(KeyCode.J))
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

            // Toggle armor with "K"
            if (Input.GetKeyDown(KeyCode.K))
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

            // Toggle sword with "L"
            if (Input.GetKeyDown(KeyCode.L))
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
        }
    }

    void EquipArmor()
    {
        Transform chest = player.transform.Find("peasant_2/Armature/Root/Pelvis/Spine_01/Spine_02");

        if (chest == null)
        {
            Debug.LogError("Chest bone not found. Check the path!");
            return;
        }

        armorInstance = Instantiate(armorPrefab);
        armorInstance.transform.SetParent(chest);
        armorInstance.transform.localPosition = new Vector3(0f, 0.043f, -0.029f);
        armorInstance.transform.localRotation = Quaternion.Euler(0f, -180f, 0f);
        armorInstance.transform.localScale = new Vector3(0.3f, 0.4f, 0.4f);
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
        Transform hand = player.transform.Find("peasant_2/Armature/Root/Pelvis/Spine_01/Spine_02/Spine_03/Clavicle_R/Upperarm_R/Lowerarm_R/Hand_R");

        if (hand == null)
        {
            Debug.LogError("Right hand bone not found. Update the path!");
            return;
        }

        swordInstance = Instantiate(swordPrefab);
        swordInstance.transform.SetParent(hand);
        swordInstance.transform.localPosition = new Vector3(0.01819365f, 0.08247816f, -0.01110588f);
        swordInstance.transform.localRotation = Quaternion.Euler(234.587f, 137.655f, 14.591f);
        swordInstance.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
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
        Transform head = player.transform.Find("peasant_2/Armature/Root/Pelvis/Spine_01/Spine_02/Spine_03/Neck_01/Head");

        if (head == null)
        {
            Debug.LogError("Head bone not found. Check the path!");
            return;
        }

        helmetInstance = Instantiate(helmetPrefab);
        helmetInstance.transform.SetParent(head);
        helmetInstance.transform.localPosition = new Vector3(0.005f, -0.04f, 0.03f);
        helmetInstance.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        helmetInstance.transform.localScale = new Vector3(0.44f, 0.4f, 0.47f);
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