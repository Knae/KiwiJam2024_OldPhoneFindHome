using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class addressButton : MonoBehaviour
{
    public string input;
    public addressPopUp addressPopUp;

    public void setPopUpText()
    { 
        addressPopUp.setText(input);
        addressPopUp.gameObject.SetActive(true);
    }
}
