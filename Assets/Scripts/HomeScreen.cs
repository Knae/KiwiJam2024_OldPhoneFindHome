using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : MonoBehaviour
{
    public Sprite appSprite_Chat;
    public Sprite appSprite_Photos;
    public Sprite appSprite_Uber;
    public List<Sprite> appSprites_random;

    public static HomeScreen instance;

    [SerializeField] private List<appButtonScript> appButtons = new List<appButtonScript>();

    private bool setupComplete = false;

    private void Awake()
    {
        if(instance!=null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Generate clues and corresponding app connections
    /// </summary>
    private void ConnectApps()
    {
        //List<string> allClueIDs = ClueManager.instance.GetClueIDs();

        //foreach (string id in allClueIDs) 
        //{
        //    clueSource currentSource = ClueManager.instance.GetClue(id).source;

        //    //choose a random app on screen
        //    int randomIndex = Random.Range(0, appButtons.Count - 1);
        //    appButtons[randomIndex].SetAsWorkingApp(id,currentSource,);
        //}

        //set up a chat app
        int randomIndex = Random.Range(0, appButtons.Count - 1);
        appButtons[randomIndex].SetAsWorkingApp(clueSource.CONVERSATION,appSprite_Chat);
        //setup a uber app

        //setup a photos app

        //Set other apps as non-functional
        foreach (var button in appButtons) 
        {
            if(!button.isWorking)
            {
                button.SetAsBroken(appSprites_random[Random.Range(0,appSprites_random.Count-1)]);
            }
        }
    }

    public void ShowHomeScreen()
    {
        gameObject.SetActive(true);
    }

    public void HideShowScreen()
    {
        gameObject.SetActive(false);
    }
}
