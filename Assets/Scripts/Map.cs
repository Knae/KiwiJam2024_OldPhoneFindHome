using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    private bool setupComplete = false;
    public bool setupDone=> setupComplete;

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
            for (int i = 0; i < Random.Range(5, 15); i++)
            {
                MapLocation newLoc = CreateRandomLocation(r.region, LandmarkType.House);
                newLoc.guessable = true;
                newLoc.landmarkName = "House " + locID;
                locID++;
            }
        }

        // pick region for home to be in
        homeRegion = regions[Random.Range(0, regions.Length)];

        // create clue for region
        clueClass regionClue = new clueClass();
        int source = Random.Range(0, (int)clueSource.PHOTO);
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
        int index = Random.Range(0, homeRegion.landmarks.Length);
        int landmarkCount = 0;
        while(landmarkCount < 3)
        {
            Debug.Log(index);
            MapLocation landmark = homeRegion.landmarks[index];
            int dist = (int)Vector2.Distance(
                    landmark.RectTransform.anchoredPosition,
                    home.RectTransform.anchoredPosition
            );

            landmark.radius = dist;

            clueClass landmarkClue = new clueClass();
            source = Random.Range(0, (int)clueSource.PHOTO);
            landmarkClue.SetData(clueRegion.NONE, (clueSource)source, dist, landmark.GetName);

            ClueManager.instance.AddClue(landmarkClue.getBaseName, landmarkClue);

            landmark.clueID = landmarkClue.getBaseName;

            landmarkCount++;

            index++;
            if (index >= homeRegion.landmarks.Length)
                index = 0;
        }

        setupComplete = true;
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
    MapLocation CreateRandomLocation(MapRegion region, LandmarkType type)
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

        backButton.SetActive(false);
    }

    public void ViewRegion(MapRegion region)
    {
        region.ToggleView(true);
        currentView = MapView.Region;

        backButton.SetActive(true);

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
        if(selectedLocation == home)
        {
            Debug.Log("win");
            // player wins
        }
        else
        {
            Debug.Log("lose");
            // player loses
        }
    }
    public void CancelGuess()
    {
        SelectLocation(null);
    }
}
