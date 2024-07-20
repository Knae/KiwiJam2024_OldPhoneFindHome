using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class appButtonScript : MonoBehaviour
{
    [SerializeField] private bool functioning = false;
    [SerializeField] private clueSource appType = clueSource.NONE;
    [SerializeField] private Image appImage;

    public bool isWorking => functioning;

    public void SetAsWorkingApp(clueSource type, Sprite imageSprite)
    {
        functioning = true;
        appImage.sprite = imageSprite;

        appType = type;
    }

    public void SetAsBroken( Sprite imageSprite)
    { 
        appImage.sprite = imageSprite;
    }

    public void OpenApp()
    {
        if (functioning)
        {
            HomeScreen.instance.HideShowScreen();

            switch (appType)
            {
                case clueSource.CONVERSATION:
                {
                    HomeScreen.instance.GetChatObject.SetActive(true);
                    break;
                }
                case clueSource.TRAVEL:
                {
                    HomeScreen.instance.GetUbeeObject.SetActive(true);
                    break;
                }
                case clueSource.PHOTO:
                {
                    break;
                }
                default:
                {
                    Debug.LogWarning("WARN[appButtonScript][OpenApp]: App button marked as functioning but has no type assigned to it");
                    HomeScreen.instance.ShowHomeScreen();
                    break;
                }
            } 
        }
        else
        {
            HomeScreen.instance.GetCorruptedObject.SetActive(true);
            if(Random.Range(0, 3) == 0)
            {
                Dialogue.instance.CorruptedApp(); // show some dialogue complaining about corrupted apps
            }
        }
    }
}
