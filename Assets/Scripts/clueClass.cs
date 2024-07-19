using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum clueSource
{
    NONE,
    TRAVEL,
    CONVERSATION,
    PHOTO
}

enum clueRegion
{
    NONE,
    NORTH,
    EAST,
    WEST,
    SOUTH
}


public class clueClass : MonoBehaviour
{
    [SerializeField] private string clueID;
    [SerializeField] private clueSource clueSource;
    [SerializeField] private string keyLandmark;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
