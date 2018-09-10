using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is null");
            }

            return _instance;
        }
    }

    public Text gemCountShop;
    public Text gemCountHUD;
    public Image selectionImage;
    public Image[] lives;

    public void OpenShop(int gemCount)
    {
        gemCountShop.text = "" + gemCount + "G";
    }

    public void UpdateShopSelection(int yPos)
    {
        selectionImage.rectTransform.anchoredPosition =
            new Vector2(selectionImage.rectTransform.anchoredPosition.x, yPos);
    }
    private void Awake()
    {
        _instance = this;
    }

    public void UpdateGemCount(int count)
    {
        gemCountHUD.text = "" + count;
    }

    public void UpdateLives(int livesRemaining)
    {
        for (int i = 0; i <= livesRemaining; i++)
        {
            if(i==livesRemaining)
            { lives[i].enabled=false;}
        }
    }
}
