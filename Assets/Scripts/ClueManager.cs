using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueManager : MonoBehaviour
{
    [Header("Settings")]
    public int minClues = 3;
    public int maxClues = 10;
    public float minDist = 10.0f;
    public float maxDist = 100.0f;

    public static ClueManager instance;

    [SerializeField] private Dictionary<string,clueClass> existingClues = new Dictionary<string, clueClass>();
    [SerializeField] private List<string> discoveredClues = new List<string>(); // list of clue IDs for clues the user has discovered. todo: dict matching ids to note descriptions?

    private void Awake()
    {
        if (instance!=null && instance!=this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void GenerateClues()
    {
        int numberOfClues = Random.Range(minClues, maxClues);

        //Add a region clue
        int randomRegionNum = Random.Range(1 , (int)clueRegion.WEST);
        int randomRegionSource = Random.Range(1, (int)clueSource.PHOTO);
        string regionClueID = "regionClue";
        clueClass newRegionClue = new clueClass();
        newRegionClue.SetData((clueRegion)randomRegionNum ,(clueSource)randomRegionSource);
        existingClues.Add(regionClueID,newRegionClue);

        //Generate other clues
        while(existingClues.Count < numberOfClues)
        {
            randomRegionSource = Random.Range(1, (int)clueSource.PHOTO);
            float randDist = Random.Range(minDist, maxDist);
            clueClass newClue = new clueClass();
            newClue.SetData(clueRegion.NONE, (clueSource)randomRegionSource, randDist);
            string clueID = newClue.getBaseName;
            int count = 1;

            //To avoid duplicate, add a number to the end if the ID
            //already exists
            while(existingClues.ContainsKey(clueID))
            {
                clueID = newClue.getBaseName + count.ToString();
                count++;
            }
            existingClues.Add(clueID, newClue);
        }

        Debug.Log("LOG[ClueManager]: CLue generation done");
    }

    /// <summary>
    /// First checks if the clueID exists and has not already been discovered
    /// before adding it to the discovered list. Returns false if either is false.
    /// </summary>
    /// <param name="clueID"></param>
    /// <returns></returns>
    public bool FoundClue(string clueID)
    {
        if(existingClues.ContainsKey(clueID) && !discoveredClues.Contains(clueID))
        {
            discoveredClues.Add(clueID);
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the discovered clue list contains the given clueID
    /// </summary>
    /// <param name="clueID"></param>
    /// <returns></returns>
    public bool ClueRevealed(string clueID)
    {
        if (discoveredClues.Contains(clueID))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Get the clue associated with the ID. Returns null
    /// if it doesn't exist
    /// </summary>
    /// <param name="clueID"></param>
    /// <returns></returns>
    public clueClass GetClue(string clueID)
    {
        if(existingClues.TryGetValue(clueID, out clueClass result))
        {
            return result;
        }
        else
        {
            return null;
        }
    }
}
