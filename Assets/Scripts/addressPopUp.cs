using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class addressPopUp : MonoBehaviour
{
    public TMP_Text text;

    public void setText(string input) 
    {
        text.text = input;
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }

}
