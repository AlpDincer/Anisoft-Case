using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject coinText;
    public GameObject startText;
    public GameObject shopScreen;

    public Material interactMat;

    public Button unlockOnionBttn;
    public Button unlockTable1Bttn;
    public Button unlockTable2Bttn;
    public Button unlockTable3Bttn;
    public Button unlockTable4Bttn;
    public Button unlockTable5Bttn;

    void Start()
    {
        if (GameManager.Instance.gameData.isPlayed == false)
        {
            startText.SetActive(true);

            GameManager.Instance.gameData.isPlayed = true;
        }
    }
    void Update()
    {
        coinText.GetComponent<TMP_Text>().text = GameManager.Instance.GetCoin().ToString();
    }

    public void ShopButton()
    {
        if (shopScreen.activeInHierarchy == true)
        {
            shopScreen.SetActive(false);
        }
        else
        {
            shopScreen.SetActive(true);
        }
    }

    public void UnlockVegetable(string vegetableName)
    {
        if(GameManager.Instance.GetCoin() >= 100 && GameManager.Instance.GetVegetable(vegetableName) != null)
        {
            GameManager.Instance.GetVegetable(vegetableName).isUnlocked = true;
            GameManager.Instance.gameData.currentCoin -= 100;
            unlockOnionBttn.interactable = false;
        }
    }

    public void UnlockTable(int tableNumber)
    {
        if (GameManager.Instance.GetCoin() >= 100 && GameManager.Instance.GetTable(tableNumber) != null)
        {
            GameManager.Instance.GetTable(tableNumber).isUnlocked = true;
            GameManager.Instance.gameData.currentCoin -= 100;

            switch (tableNumber)
            {
                case 1:
                    unlockTable1Bttn.interactable = false;
                    break;
                case 2:
                    unlockTable2Bttn.interactable = false;
                    break;
                case 3:
                    unlockTable3Bttn.interactable = false;
                    break;
                case 4:
                    unlockTable4Bttn.interactable = false;
                    break;
                case 5:
                    unlockTable5Bttn.interactable = false;
                    break;
            }
        }
    }
}
