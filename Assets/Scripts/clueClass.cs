using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
//using UnityEngine;

public enum clueSource
{
    [Description("None")]
    NONE,
    [Description("Travel App")]
    TRAVEL,
    [Description("Chat App")]
    CONVERSATION,
    [Description("Photo Gallery")]
    PHOTO
}

public enum clueRegion
{
    [Description ("no region")]
    NONE,
    [Description ("north")]
    NORTH,
    [Description("east")]
    EAST,
    [Description("west")]
    WEST,
    [Description("central")]
    CENTRAL
}


public class clueClass// : MonoBehaviour
{
    private string clueBaseName;
    private clueSource sourceApp;
    private clueRegion region;
    private float distanceToLocation;
    private string keyLandmark;

    public string getBaseName => clueBaseName;
    public clueSource source => sourceApp;
    public clueRegion containedRegion => region;
    public float distance => distanceToLocation;
    public string containedLandmark => keyLandmark;

    public void SetData(clueRegion _region = clueRegion.NONE, clueSource source = clueSource.NONE,float distance = 0.0f, string landmarkName = "N/A")
    {
        sourceApp = (source==clueSource.NONE)?clueSource.TRAVEL:source;
        region = _region;
        keyLandmark = landmarkName;
        distanceToLocation = distance;

        clueBaseName = source.ToString()+ "_" + region.ToString()+ "_" + landmarkName;
    }
}
