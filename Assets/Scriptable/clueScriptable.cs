using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Clue Data", menuName = "Scriptable/New Clue Data")]
public class clueScriptable : ScriptableObject
{

    [SerializeField] private string clueID;
    [SerializeField] private string clueName;
    [SerializeField] private string clueDescrip;
    // Start is called before the first frame update

    public void copyData( clueScriptable source)
    {
        clueID = source.clueID;
        clueName = source.clueName;
        clueDescrip = source.clueDescrip;
    }
}
