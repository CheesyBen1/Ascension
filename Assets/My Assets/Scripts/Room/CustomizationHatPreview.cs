using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationHatPreview : MonoBehaviour
{

    public GameObject hat0;
    public GameObject hat1;
    public GameObject hat2;
    public GameObject hat3;
    public GameObject hat4;
    public GameObject hat5;
    public GameObject hat6;
    public GameObject hat7;
    public GameObject hat8;
    public GameObject hat9;
    public GameObject hat10;
    public GameObject hat11;

    List<GameObject> hats = new List<GameObject>();

    public int hatPreview = 0;

    public GameObject equipButton;
    public GameObject equippedButton;
    public GameObject buyButton;

    public CustomizationHat customizationHat;

    public TextMeshProUGUI priceText;

    public int priceGet = 0;

    public bool buy = false;
    public string buyString = "False";

    // Start is called before the first frame update
    void Start()
    {
        customizationHat = GameObject.FindGameObjectWithTag("CustomizationHat").GetComponent<CustomizationHat>();

        hats.Add(hat0);
        hats.Add(hat1);
        hats.Add(hat2);
        hats.Add(hat3);
        hats.Add(hat4);
        hats.Add(hat5);
        hats.Add(hat6);
        hats.Add(hat7);
        hats.Add(hat8);
        hats.Add(hat9);
        hats.Add(hat10);
        hats.Add(hat11);

        // hatPreview = customizationHat.hat;
    }

    // Update is called once per frame
    void Update()
    {
        switchHat(hatPreview);
    }

    public void switchHat(int num)
    {


        for (int i = 0; i < hats.Count; i++)
        {
            if (i == num)
            {
                hats[i].SetActive(true);
            }
            else
            {
                hats[i].SetActive(false);
            }
        }




    }

    public void onClickHat(int num)
    {
        hatPreview = num;

        PlayFabControl.PFControl.getBuy(num);
        
        

        StartCoroutine(buttons());

        //if (PlayFabControl.PFControl.getBuy(num) == "True")
        //{
        //    buyButton.SetActive(false);
        //    if (hatPreview == customizationHat.hat)
        //    {
        //        equipButton.SetActive(false);
        //        equippedButton.SetActive(true);
        //    }
        //    else
        //    {
        //        equipButton.SetActive(true);
        //        equippedButton.SetActive(false);
        //    }


        //}
        //else {
        //    buyButton.SetActive(true);
        //    equipButton.SetActive(false);
        //    equipButton.SetActive(false);

        //} 
       
    }


    IEnumerator buttons()
    {
        
        buyButton.SetActive(false);
        equipButton.SetActive(false);
        equippedButton.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        if(PlayFabControl.PFControl.returnKey == "True")
        {
            buy = true;
        }
        else
        {
            buy = false;
        }

        if (buy == true)
        {
            buyButton.SetActive(false);
            if (hatPreview == customizationHat.hat)
            {
                equipButton.SetActive(false);
                equippedButton.SetActive(true);
            }
            else
            {
                equipButton.SetActive(true);
                equippedButton.SetActive(false);
            }


        }
        else
        {
            buyButton.SetActive(true);
            equipButton.SetActive(false);
            equippedButton.SetActive(false);
            if (PlayerInfo.playerInfo.apples < priceGet)
            {
                buyButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                buyButton.GetComponent<Button>().interactable = true;
            }

        }
    }

    public void onClickHatSetPrice(int price)
    {
        priceText.text = price.ToString();
        priceGet = price; 
    }

    public void onEquipHat()
    {
        customizationHat.hat = hatPreview;
        PlayFabControl.PFControl.saveEquip(hatPreview);

        if (hatPreview == customizationHat.hat)
        {
            equipButton.SetActive(false);
            equippedButton.SetActive(true);
        }
        else
        {
            equipButton.SetActive(true);
            equippedButton.SetActive(false);
        }
    }

    public void OnClickBuy()
    {
        buyHat(hatPreview, priceGet);
        buyButton.SetActive(false);
        if (hatPreview == customizationHat.hat)
        {
            equipButton.SetActive(false);
            equippedButton.SetActive(true);
        }
        else
        {
            equipButton.SetActive(true);
            equippedButton.SetActive(false);
        }
    }

    public void buyHat(int hatNum, int price)
    {
        var request = new SubtractUserVirtualCurrencyRequest
        {
            VirtualCurrency = "GA",
            Amount = price
        };

        
        PlayFabClientAPI.SubtractUserVirtualCurrency(request, onBuySuccess, onError);

        
    }

    public void onBuySuccess(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log("Bought hat!");
        PlayFabControl.PFControl.saveBuy(hatPreview);
        PlayFabControl.PFControl.GetVirtualCurrencies();
    }

    public void onError(PlayFabError error)
    {
        Debug.Log(error.ErrorMessage);
    }



}
