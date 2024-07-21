using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ubbeeLogList : MonoBehaviour
{
    [SerializeField] private GameObject logPrefab;
    [SerializeField] private GameObject logListArea;
    [SerializeField] private int randomizerLoopCount = 4;
    [SerializeField] private int totalLogs = 6;
    [SerializeField] private int totalLogs_Friends = 2;
    [Range(2,60)]
    [SerializeField] private Vector2 randomDistanceRange = new Vector2(2,50);
    [SerializeField] private List<string> friendNames = new List<string>();
    [SerializeField] private List<string> friendPlace = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        if(randomDistanceRange.x > randomDistanceRange.y)
        {
            float temp = randomDistanceRange.x;
            randomDistanceRange.x = randomDistanceRange.y;
            randomDistanceRange.y = temp;
        }

        GenerateLogs();
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

        for (int r = 0; r < randomizerLoopCount; r++)
        {
            int switchT_1 = Random.Range(0, randomName.Count);
            int switchT_2 = Random.Range(0, randomName.Count);

            string tempName = randomName[switchT_1];
            randomName[switchT_1] = randomName[switchT_2];
            randomName[switchT_2] = tempName;
        }

        //Generate which index to be the clue
        int clueIndex = Random.Range(0, totalLogs - 1);
        //Generate log at random, run a set amount of times
        for (int i = 0; i < totalLogs - 1; i++) 
        {
            if(i == clueIndex)
            {
                addressButton newLog = Instantiate(logPrefab, logListArea.transform).GetComponent<addressButton>();
                newLog.SetLogDisplay("Work","TODO",true);
            }

            addressButton newRandomLog = Instantiate(logPrefab, logListArea.transform).GetComponent<addressButton>();            
            newRandomLog.SetLogDisplay(randomName[i], Random.Range(randomDistanceRange.x,randomDistanceRange.y).ToString("F2") + " km", true);
        }
    }
}
