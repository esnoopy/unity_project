using UnityEngine;

public class ArmorEquipper : MonoBehaviour
{
    public GameObject player;        // Drag your player GameObject here
    public GameObject armorPrefab;  // Drag your armor prefab here

    private bool armorEquipped = false;
    private GameObject armorInstance;

    void Update()
    {
        // Toggle armor with "L" key
        if (Input.GetKeyDown(KeyCode.L))
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
        armorInstance.transform.localPosition = new Vector3(0f, 0.05f, -0.03f);
        armorInstance.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        armorInstance.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }

    void UnequipArmor()
    {
        if (armorInstance != null)
        {
            Destroy(armorInstance);
            armorInstance = null;
        }
    }
}
