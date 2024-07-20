using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class addressButton : MonoBehaviour
{
    public TMP_Text addressText;
    public string input;
    public addressPopUp addressPopUp;
    public List<string> addresses;
    public bool useRandomAddress;

    private void Awake()
    {
        if (useRandomAddress) 
        {
            int randomNumber = Random.Range(0, addresses.Count);
            string address = getRandomAddress(randomNumber);
            addressText.text = address;
        }
    }

    public string getRandomAddress(int index)
    {
        return addresses[index];
    }

    public void setPopUpText()
    { 
        addressPopUp.setText(input);
        addressPopUp.gameObject.SetActive(true);
    }
}
