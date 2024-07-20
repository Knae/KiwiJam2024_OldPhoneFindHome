using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class testScripts : MonoBehaviour
{
    public TMP_Text text;
    public bool a_bool = true;
    public string outputText = "ERROR 403\nAccess denied";  
    public void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void test() 
    {
        text.text = outputText;
    }
}
