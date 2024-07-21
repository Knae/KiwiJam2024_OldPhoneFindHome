using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Map : MonoBehaviour
{
    public static Map instance;

    public enum MapView {
        FullMap,
        Region
    }

    public MapView currentView;

    [System.Serializable]
    public struct Region
    {
        public MapRegion region;
        public MapLocation[] landmarks;

    }

    [SerializeField] Region[] regions;

    [SerializeField] GameObject backButton;

    [SerializeField] ScrollRect scrollRect;

    [SerializeField] GameObject guessPanel;
    [SerializeField] TextMeshProUGUI guessText;
    MapLocation selectedLocation;

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
    [SerializeField] Region homeRegion;
    [SerializeField] MapLocation home;

    [SerializeField] RectTransform scaler;

    //private bool setupComplete = false;
    //public bool setupDone=> setupComplete;

    public event Action onCluesGenerated;
    private void Awake()
    {
        instance = this;

        for (int i = 0; i < regions.Length; i++)
        {
            regions[i].landmarks = regions[i].region.GetComponentsInChildren<MapLocation>(true);
            Debug.Log(regions[i].region.clueRegion + " " + regions[i].landmarks.Length);
        }
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

        // create random houses in each region
        foreach(Region r in regions)
        {
            for (int i = 0; i < UnityEngine.Random.Range(5, 15); i++)
            {
                MapLocation newLoc = CreateRandomLocation(r.region, LandmarkType.House);
                newLoc.guessable = true;
                newLoc.landmarkName = "House " + locID;
                locID++;
            }
        }

        // pick region for home to be in
        homeRegion = regions[UnityEngine.Random.Range(0, regions.Length)];

        // create clue for region
        clueClass regionClue = new clueClass();
        int source = UnityEngine.Random.Range(0, (int)clueSource.MAX);
        regionClue.SetData(homeRegion.region.clueRegion, (clueSource)source);
        ClueManager.instance.AddClue(regionClue.getBaseName, regionClue);

        // create home location
        home = CreateRandomLocation(homeRegion.region, LandmarkType.House);
        home.guessable = true;
        home.landmarkName = "House " + locID;

        // create clues based on distance to 3 random landmarks in that region
        // using loop with a random start index to avoid randomly selecting the same landmark multiple times
        Debug.Log(homeRegion.region.clueRegion);
        Debug.Log(homeRegion.landmarks.Length);
        int index = UnityEngine.Random.Range(0, homeRegion.landmarks.Length);
        int landmarkCount = 0;
        while(landmarkCount < 3)
        {
            // pick a random landmark
            MapLocation landmark = homeRegion.landmarks[index];
            // get distance
            int dist = (int)Vector2.Distance(
                    landmark.RectTransform.anchoredPosition,
                    home.RectTransform.anchoredPosition
            );


            // create and addclue
            clueClass landmarkClue = new clueClass();
            source = UnityEngine.Random.Range(0, (int)clueSource.MAX);
            landmarkClue.SetData(clueRegion.NONE, (clueSource)source, dist, landmark.GetName);

            ClueManager.instance.AddClue(landmarkClue.getBaseName, landmarkClue);

            // set up landmark object
            landmark.clueID = landmarkClue.getBaseName;
            landmark.radius = dist;

            // add another house that distance away so you actually need multiple clues to narrow it down
            // trig to find random point on circle
            float angle = UnityEngine.Random.Range(0, 360);
            Vector2 pos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * dist;
            // create house
            CreateRandomLocation(homeRegion.region, LandmarkType.House).RectTransform.anchoredPosition = landmark.RectTransform.anchoredPosition + pos;

            landmarkCount++;

            index++;
            if (index >= homeRegion.landmarks.Length)
                index = 0;
        }

        //setupComplete = true;
        onCluesGenerated?.Invoke();
    }

    MapLocation CreateRandomLocation(MapRegion region)
    {
        // create location object
        MapLocation loc = Instantiate(locationPrefab, region.locationsParent).GetComponent<MapLocation>();

        // random position
        Vector2 pos = new Vector2(
            UnityEngine.Random.Range(region.minLocationPos.x, region.maxLocationPos.x),
            UnityEngine.Random.Range(region.minLocationPos.y, region.maxLocationPos.y)
        );
        loc.RectTransform.anchoredPosition = pos;

        // random type of location (e.g. mountain, lake, house) and sprite based on that
        System.Array vals = System.Enum.GetValues(typeof(LandmarkType));
        loc.landmarkType = (LandmarkType)vals.GetValue(UnityEngine.Random.Range(0, vals.Length));
        loc.image.sprite = landmarkSprites[(int)loc.landmarkType];

        return loc;
    }
    MapLocation CreateRandomLocation(MapRegion region, LandmarkType type)
    {
        // create location object
        MapLocation loc = Instantiate(locationPrefab, region.locationsParent).GetComponent<MapLocation>();

        // random position
        Vector2 pos = new Vector2(
            UnityEngine.Random.Range(region.minLocationPos.x, region.maxLocationPos.x),
            UnityEngine.Random.Range(region.minLocationPos.y, region.maxLocationPos.y)
        );
        loc.RectTransform.anchoredPosition = pos;

        // random type of location (e.g. mountain, lake, house) and sprite based on that
        loc.landmarkType = type;
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

        foreach (Region r in regions)
        {
            r.region.ToggleView(false);
            r.region.gameObject.SetActive(true);
        }

        scrollRect.normalizedPosition = Vector2.one * 0.5f;

        scaler.localScale = Vector3.one;

        backButton.SetActive(false);
    }

    public void ViewRegion(MapRegion region)
    {
        region.ToggleView(true);
        currentView = MapView.Region;

        backButton.SetActive(true);

        scaler.localScale = Vector3.one * 0.85f;

        foreach (Region r in regions)
        {
            if(r.region != region)
            {
                r.region.gameObject.SetActive(false);
            }
        }
    }

    public void SelectLocation(MapLocation loc)
    {
        selectedLocation = loc;
        guessPanel.SetActive(loc != null); // todo if there's time: animate
        if(loc != null)
        {
            guessText.text = "You have selected " + loc.GetName + ". Lock in guess?";
        }
    }

    public void ConfirmGuess()
    {
        Dialogue.instance.Ending(selectedLocation == home);
    }
    public void CancelGuess()
    {
        SelectLocation(null);
    }
}
