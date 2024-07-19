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

    [SerializeField] MapRegion[] regions;

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

    public void FullView()
    {
        currentView = MapView.FullMap;

        foreach (MapRegion r in regions)
        {
            r.ToggleView(false);
            r.gameObject.SetActive(true);
        }
    }

    public void ViewRegion(MapRegion region)
    {
        region.ToggleView(true);
        currentView = MapView.Region;

        foreach(MapRegion r in regions)
        {
            if(r != region)
            {
                r.gameObject.SetActive(false);
            }
        }
    }
}
