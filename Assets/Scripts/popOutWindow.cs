using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popOutWindow : MonoBehaviour
{
    public GameObject pic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void picRemove()
    {
        if (pic != null) 
        {
            pic.SetActive(false);
        }
        
    }
}
