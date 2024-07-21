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

    private bool setAsClue = false;
    private string attachedClueID = string.Empty;

    private void Awake()
    {
        //if (useRandomAddress) 
        //{
        //    int randomNumber = Random.Range(0, addresses.Count);
        //    string address = getRandomAddress(randomNumber);
        //    addressText.text = address;
        //}
    }

    public void SetLogDisplay( string text,string distance, bool isClue, string clueID = "")
    {
        addressText.text = text;
        distanceText.text = distance;
        setAsClue = isClue;
        attachedClueID = clueID;
    }

    //public string getRandomAddress(int index)
    //{
    //    return addresses[index];
    //}

    public void onClick()
    {
        if(setAsClue && !ClueManager.instance.ClueRevealed(attachedClueID))
        {
            ClueManager.instance.FoundClue(attachedClueID);
        }
    }

    //public void setPopUpText()
    //{ 
    //    addressPopUp.setText(input);
    //    addressPopUp.gameObject.SetActive(true);
    //}
}
