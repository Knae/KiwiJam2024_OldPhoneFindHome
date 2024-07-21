using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class addressButton : MonoBehaviour
{
    public TMP_Text addressText;
    public TMP_Text distanceText;
    public string input;
    public addressPopUp addressPopUp;
    public List<string> addresses;
    public bool useRandomAddress;

    public bool setAsClue = false;

    private void Awake()
    {
        //if (useRandomAddress) 
        //{
        //    int randomNumber = Random.Range(0, addresses.Count);
        //    string address = getRandomAddress(randomNumber);
        //    addressText.text = address;
        //}
    }

    public void SetLogDisplay( string text,string distance, bool isClue)
    {
        addressText.text = text;
        distanceText.text = distance;
        setAsClue = isClue;
    }

    //public string getRandomAddress(int index)
    //{
    //    return addresses[index];
    //}

    public void onClick()
    {
        if(setAsClue)
        {

        }
    }

    //public void setPopUpText()
    //{ 
    //    addressPopUp.setText(input);
    //    addressPopUp.gameObject.SetActive(true);
    //}
}
