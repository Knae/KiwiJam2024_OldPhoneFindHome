using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ubbeeLogList : MonoBehaviour
{
    [SerializeField] private GameObject logPrefab;
    [SerializeField] private GameObject logListArea;
    [SerializeField] private int randomizerLoopCount = 10;
    [SerializeField] private int totalLogs_Random = 6;
    [SerializeField] private int totalLogs_Friends = 2;
    [Range(2,60)]
    [SerializeField] private Vector2 randomDistanceRange = new Vector2(2,50);
    [SerializeField] private List<string> friendNames = new List<string>();
    [SerializeField] private List<string> friendPlace = new List<string>();

    private void Awake()
    {
        Map.instance.onCluesGenerated += GenerateLogs;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(randomDistanceRange.x > randomDistanceRange.y)
        {
            float temp = randomDistanceRange.x;
            randomDistanceRange.x = randomDistanceRange.y;
            randomDistanceRange.y = temp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateLogs()
    {
        //Set up list of random names
        List<string> randomName = new List<string>();

        for (int f = 0; f < totalLogs_Friends; f++)
        {
            int index = Random.Range(0, friendNames.Count);
            randomName.Add(friendNames[index] + "'s " + friendPlace[Random.Range(0, friendPlace.Count)]);
        }

        randomName.Add("Mall");randomName.Add("University");randomName.Add("Hair Saloon");

        List<string> ubbeeClueIDs = ClueManager.instance.GetCluesOfType(clueSource.TRAVEL);

        //Generate which index to be the clue
        int clueIndex = Random.Range(0, totalLogs_Random - 1);
        List<GameObject> newLogs = new List<GameObject>();
        //Generate random logs
        for (int i = 0; i < randomName.Count; i++) 
        {
            GameObject newRandomLog = Instantiate(logPrefab, logListArea.transform);
            newLogs.Add(newRandomLog);
            newRandomLog.GetComponent<addressButton>().SetLogDisplay(randomName[i], Random.Range(randomDistanceRange.x, randomDistanceRange.y).ToString("F2") + " km", false) ;
        }

        foreach (var item in ubbeeClueIDs)
        {
            clueClass logClue = ClueManager.instance.GetClue(item);
            if (logClue.containedRegion == clueRegion.NONE)
            {
                GameObject newLog = Instantiate(logPrefab, logListArea.transform);
                newLogs.Add(newLog);
                newLog.GetComponent<addressButton>().SetLogDisplay(logClue.containedLandmark, (logClue.distance / 10).ToString("F2") + " km", true, item); 
            }
        }


        for (int r = 0; r < randomizerLoopCount; r++)
        {
            int switchT_1 = Random.Range(0, newLogs.Count);
            int switchT_2 = Random.Range(0, newLogs.Count);

            //Need a better way to do this
            GameObject tempObject = newLogs[switchT_1];
            newLogs[switchT_1] = newLogs[switchT_2];
            newLogs[switchT_2] = tempObject;
        }

        foreach (var item in newLogs)
        {
            item.transform.SetParent(logListArea.transform);
        }

        Map.instance.onCluesGenerated -= GenerateLogs;
    }
}
