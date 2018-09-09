using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class ShopKeeper : MonoBehaviour
{

    public GameObject shopPanel;
    public int currentSelectedItem=1;
    public int currentItemCost;

    private Player player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<Player>();
            if (player != null)
            {
                UIManager.Instance.OpenShop(player.DiamondsCollected);
            }
            shopPanel.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        switch (item)
        {
            case 0:
                UIManager.Instance.UpdateShopSelection(62);
                currentSelectedItem = 0;
                currentItemCost = 200;
                break;
            case 1:
                UIManager.Instance.UpdateShopSelection(-39);
                currentSelectedItem = 1;
                currentItemCost = 400;
                break;
            case 2:
                UIManager.Instance.UpdateShopSelection(-152);
                currentItemCost = 100;
                currentSelectedItem = 2;
                break;
        }
    }

    public void BuyItem()
    {
        if (player.DiamondsCollected >= currentItemCost)
        {
            if (currentSelectedItem == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
            }
            Debug.Log("Purchased: "+currentSelectedItem);
            player.DiamondsCollected -= currentItemCost;
            UIManager.Instance.OpenShop(player.DiamondsCollected);
        }
        else
        {
            Debug.Log("You do not have enough gems");
        }
    }
}
