using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public static Map instance;

    public enum MapView {
        FullMap,
        Region
    }

    public MapView currentView;

    [SerializeField] MapRegion[] regions;

    [SerializeField] GameObject backButton;

    [SerializeField] ScrollRect scrollRect;

    [Header("Map Generation")]
    [SerializeField] GameObject locationPrefab;

    [Header("Clues & Home")]
    [SerializeField] MapRegion homeRegion;
    [SerializeField] MapLocation home;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InitialMapSetup();

        FullView();
        ShowMap();
    }

    void InitialMapSetup()
    {
        int locID = 0;

        // pick home location and create pin for it
        homeRegion = regions[Random.Range(0, regions.Length)];
        home = CreateRandomLocation(homeRegion);

        // create landmarks around home and create clues for them
        for (int i = 0; i < Random.Range(1, 4); i++)
        {
            MapLocation landmark = CreateRandomLocation(homeRegion);
            int landmarkDistance = Random.Range(50, 400);

            // put position at random point landmarkDistance away from home
            float angle = Random.Range(0, 360);
            Vector2 pos = new Vector2(
                Mathf.Cos(angle),
                Mathf.Sin(angle)
            ) * landmarkDistance;

            landmark.RectTransform.anchoredPosition = home.RectTransform.anchoredPosition + pos;

            landmark.radius = landmarkDistance;

            // todo: create clue in cluemanager
            // todo: clue ID based on type
            landmark.clueID = "typeoflandmark" + locID.ToString();

            locID++;
        }

        // create clue for region

        // create random non-clue landmarks to clutter map
        foreach(MapRegion r in regions)
        {
            for (int i = 0; i < Random.Range(5, 10); i++)
            {
                CreateRandomLocation(r);
                locID++;
            }
        }
    }

    MapLocation CreateRandomLocation(MapRegion region)
    {
        // create location object
        MapLocation loc = Instantiate(locationPrefab, region.locationsParent).GetComponent<MapLocation>();

        // random position
        Vector2 pos = new Vector2(
            Random.Range(region.minLocationPos.x, region.maxLocationPos.x),
            Random.Range(region.minLocationPos.y, region.maxLocationPos.y)
        );
        loc.RectTransform.anchoredPosition = pos;

        // todo: random type of location (e.g. mountain, shop, house) and sprite based on that

        return loc;

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

        scrollRect.normalizedPosition = Vector2.one * 0.5f;

        backButton.SetActive(false);
    }

    public void ViewRegion(MapRegion region)
    {
        region.ToggleView(true);
        currentView = MapView.Region;

        backButton.SetActive(true);

        foreach (MapRegion r in regions)
        {
            if(r != region)
            {
                r.gameObject.SetActive(false);
            }
        }
    }
}
