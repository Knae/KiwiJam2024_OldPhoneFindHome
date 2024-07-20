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

    public enum LandmarkType
    {
        House,
        Lake,
        Mountain,
        Tower,
        Pin,
    }
    [Header("Map Generation")]
    [SerializeField] Sprite[] landmarkSprites;
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
        home.landmarkType = LandmarkType.House;
        home.image.sprite = landmarkSprites[0];

        // create landmarks around home and create clues for them
        for (int i = 0; i < Random.Range(2, 5); i++)
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

            // create a random location to ensure the home isn't the only place in the radius
            MapLocation randomLoc = CreateRandomLocation(homeRegion);
            angle = Random.Range(0, 360);
            pos = new Vector2(
                Mathf.Cos(angle),
                Mathf.Sin(angle)
            ) * landmarkDistance;
            randomLoc.RectTransform.anchoredPosition = landmark.RectTransform.anchoredPosition + pos;
            locID++;

            // todo: create clue in cluemanager
            // todo: clue ID based on type
            landmark.clueID = landmark.landmarkType.ToString() + locID.ToString();
            ClueManager.instance.discoveredClues.Add(landmark.clueID); // this is just for debug, should be adding to a list of undiscovered clues

            locID++;
        }

        // create clue for region

        // create random non-clue landmarks to clutter map
        foreach(MapRegion r in regions)
        {
            for (int i = 0; i < Random.Range(5, 15); i++)
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

        // random type of location (e.g. mountain, lake, house) and sprite based on that
        System.Array vals = System.Enum.GetValues(typeof(LandmarkType));
        loc.landmarkType = (LandmarkType)vals.GetValue(Random.Range(0, vals.Length));
        loc.image.sprite = landmarkSprites[(int)loc.landmarkType];

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
