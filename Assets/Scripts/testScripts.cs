using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class testScripts : MonoBehaviour
{
    public TMP_Text text;
    public bool a_bool = true;
    public string outputText = "ERROR 403\nAccess denied";  

    public void test() 
    {
        text.text = outputText;
    }
}
