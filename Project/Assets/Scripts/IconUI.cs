using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(menuName = "Shop/Item")]
public class ShopUI : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public Sprite price;
}
