using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClueManager : MonoBehaviour
{
    [Header("Settings")]
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

    public void AddClue(string id, clueClass clue)
    {
        existingClues.Add(id, clue);
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

    public List<string> GetClueIDs()
    {
        List<string> ids = new List<string>();

        foreach (var item in existingClues)
        {
            ids.Add(item.Key);
        }

        return ids;
    }

    public List<string> GetCluesOfType(clueSource type)
    {
        var filtered = existingClues
            .Select(x => x)
            .Where(y => y.Value.source == type);

        List<string> ids = new List<string>();

        foreach (var item in filtered)
        {
            ids.Add(item.Key);
        }
        return ids;
    }
}
