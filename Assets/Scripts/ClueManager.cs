using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueManager : MonoBehaviour
{
    public static ClueManager instance;

    public List<string> discoveredClues = new List<string>(); // list of clue IDs for clues the user has discovered. todo: dict matching ids to note descriptions?

    private void Awake()
    {
        instance = this;
    }
}
