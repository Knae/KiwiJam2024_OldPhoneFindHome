using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class imageShower : MonoBehaviour
{
    public TMP_Text text;
    public GameObject pic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void picShower() 
    {
        if (pic != null)
        {
            pic.SetActive(true);
        }
    }

    public void showText(string input)
    {
        text.text = input;
    }

    // Update is called once per frame
    void Update()
    {
     

    }
}
