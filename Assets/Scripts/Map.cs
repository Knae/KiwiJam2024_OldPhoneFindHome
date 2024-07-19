using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public enum MapView {
        FullMap,
        Region
    }

    public MapView currentView;

    private void Start()
    {
        ShowMap();
    }

    public void ShowMap() // to be called when camera pans to map view
    {
        foreach (InteractableAfterClue interactable in GetComponentsInChildren<InteractableAfterClue>())
        {
            interactable.Check();
        }
    }

    public void ViewRegion(MapRegion region)
    {
        region.ToggleView(true);
        currentView = MapView.Region;
    }
}
