using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEditor;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    public GameObject descripPrefab;
    public static NotesController instance;

    [SerializeField] private GameObject descripArea;
    [SerializeField] private float descripHeight = 100.0f;
    [SerializeField] private float descripPadding = 0.0f;

    private List<GameObject> existingDescrip = new List<GameObject>();

    private void Awake()
    {
        if(instance!=null && instance!=this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Add a note based on the clue
    /// </summary>
    /// <param name="source"></param>
    public void AddNotes(clueClass source)
    {
        GameObject newDescript = Instantiate(descripPrefab, descripArea.transform);
        //newDescript.transform.parent = descripArea.transform;

        //Build description from source
        string fullDescription = string.Empty;

        if (source.containedRegion != clueRegion.NONE)
        {
            fullDescription += "The location seems to be in the ";

            switch (source.containedRegion) 
            {
                case clueRegion.NORTH:
                {
                        fullDescription += "north region.";
                        break;
                }
                case clueRegion.SOUTH:
                {
                        fullDescription += "south region.";
                        break;
                }
                case clueRegion.WEST:
                {
                        fullDescription += "west region.";
                        break;
                }
                case clueRegion.EAST:
                {
                        fullDescription += "east region.";
                        break;
                }
            }
        }
        else
        {
            switch (source.source)
            {
                case clueSource.TRAVEL:
                    fullDescription = "The travel log ";
                    break;
                case clueSource.CONVERSATION:
                fullDescription = "A chat conversation ";
                    break;
                //case clueSource.PHOTO:
                //    fullDescription = "A photo in the gallery ";
                //    break;
                //case clueSource.CALLS:
                //    fullDescription = "A call log ";
                //    break;
                case clueSource.NONE:
                default:
                    fullDescription = "There was something that ";
                    break;
            }
            fullDescription += " hinted that the location is near the " + source.containedLandmark + ", about " + source.distance + " distance units from it";
        }
        newDescript.GetComponentInChildren<TMP_Text>().text = fullDescription;
    }
}
