using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class ButtonExtension
{
    public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate ()
        {
            OnClick(param);
        });
    }
}
public class ShopUIManager : MonoBehaviour
{
    [Serializable]
    public struct Shop
    {
        public string Name;
        public Sprite Icon;
        public Sprite MoneyIcon;

        public string Money;
    }

    [SerializeField] Shop[] allShops;
    void Start()
    {
       GameObject buttonTemplate = transform.GetChild(0).gameObject;
        GameObject g;

        int N = allShops.Length;
        for (int i = 0; i < N; i++)
        {
            g = Instantiate(buttonTemplate, transform);
            g.SetActive(true);
            g.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = allShops[i].Name;
            g.transform.Find("ItemIcon").GetComponent<Image>().sprite = allShops[i].Icon;
            g.transform.Find("Money").GetComponent<TextMeshProUGUI>().text = allShops[i].Money;
            g.transform.Find("MoneyIcon").GetComponent<Image>().sprite = allShops[i].MoneyIcon;

            g.GetComponent<Button>().AddEventListener(i, ItemClicked);

        }
        
        Destroy(buttonTemplate);
    }

    void ItemClicked(int itemIndex)
    {
        Debug.Log("item" + itemIndex + "clicked");
        //Debug.Log("name " + allShops[itemIndex].Name);   use it to sub the money
    }
}