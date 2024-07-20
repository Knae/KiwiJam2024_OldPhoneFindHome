using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : MonoBehaviour
{
    public Sprite appSprite_Chat;
    public Sprite appSprite_Photos;
    public Sprite appSprite_Uber;
    public Sprite appSprite_CallLog;
    public List<Sprite> appSprites_random;

    public static HomeScreen instance;
    [Header("Connected Elements")]
    [SerializeField] private GameObject messageApp;
    [SerializeField] private GameObject ubeeApp;
    [SerializeField] private GameObject phoneLogApp;
    [SerializeField] private List<appButtonScript> appButtons = new List<appButtonScript>();

    private bool setupComplete = false;

    public GameObject GetChatObject => messageApp;
    public GameObject GetUbeeObject => ubeeApp;
    public GameObject GetCallLogObject => phoneLogApp;

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
        StartCoroutine(ConnectApps());
        ShowHomeScreen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Generate clues and corresponding app connections
    /// </summary>
    private IEnumerator ConnectApps()
    {
        Func<int> getRandomIndex = () => 
        {
            int validRandom = 0;

            do
            {
                validRandom = UnityEngine.Random.Range(0, appButtons.Count - 1);
            } while (appButtons[validRandom].isWorking);

            return validRandom;
        };
        //set up a chat app
        //int randomIndex = getRandomIndex();//UnityEngine.Random.Range(0, appButtons.Count - 1);
        appButtons[getRandomIndex()].SetAsWorkingApp(clueSource.CONVERSATION,appSprite_Chat);

        //setup a uber app
        appButtons[getRandomIndex()].SetAsWorkingApp(clueSource.TRAVEL, appSprite_Uber);

        //setup call log
        appButtons[getRandomIndex()].SetAsHintApp( appSprite_CallLog, phoneLogApp);

        //setup a photos app

        //Set other apps as non-functional
        //List<int> usedN
        foreach (var button in appButtons)
        {
            if (!button.isWorking)
            {
                button.SetAsBroken(appSprites_random[UnityEngine.Random.Range(0, appSprites_random.Count - 1)]);
            }
        }

        yield return null;
    }

    public void ShowHomeScreen()
    {
        gameObject.SetActive(true);
        messageApp.gameObject.SetActive(false);
        ubeeApp.gameObject.SetActive(false);
        phoneLogApp.gameObject.SetActive(false);
    }

    public void HideShowScreen()
    {
        gameObject.SetActive(false);
    }
}
