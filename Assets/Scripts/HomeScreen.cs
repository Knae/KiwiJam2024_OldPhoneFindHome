using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : MonoBehaviour
{


    [SerializeField] private List<GameObject> appButtons = new List<GameObject>();

    
    // Start is called before the first frame update
    void Start()
    {
        ClueManager.instance.GenerateClues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Generate clues and corresponding app connections
    /// </summary>
    void GenerateClues()
    {
        
    }
}
