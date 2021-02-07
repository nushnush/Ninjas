using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    const float YDISPLAY_INIT = 214f, YDISPLAY_DIFFERENCE = 107f, XDISPLAY_INIT = 424f, XDISPLAY_DIFFERENCE = 220f;
    [SerializeField] private Transform ninjaItem;
    private Ninja[] shopItems;
    
    void Start()
    {
        shopItems = new Ninja[Manager.MAX_NINJAS];
        for (int i = 0; i < Manager.MAX_NINJAS; i++)
        {
            int power = Random.Range(2, 4);
            int health = Random.Range(140, 151);
            shopItems[i] = new Ninja(power, health);

            Transform item = Instantiate(ninjaItem, transform);
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(XDISPLAY_INIT - XDISPLAY_DIFFERENCE * (i / 5), YDISPLAY_INIT - YDISPLAY_DIFFERENCE * (i % 5)); // Set location
            item.gameObject.AddComponent<Button>().onClick.AddListener(delegate { Buy(i); }); // Add OnClick event
        }
    }

    void Buy(int i)
    {
        /*int price = 1500 * Manager.ninjaCount;
        if(Manager.BuyItem(price))
        {
            Manager.AddNinja(n);
        }
        else
        {
            // Not enough gold
        }*/

        Debug.Log(i);
    }
}
