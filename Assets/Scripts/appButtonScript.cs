using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class appButtonScript : MonoBehaviour
{
    [SerializeField] private bool functioning = false;
    [SerializeField] private clueSource appType = clueSource.NONE;
    [SerializeField] private Image appImage;

    public bool isWorking => functioning;
    private bool isMinorClue = false;
    private GameObject minorClueObject = null;

    public void SetAsWorkingApp(clueSource type, Sprite imageSprite)
    {
        functioning = true;
        appImage.sprite = imageSprite;

        appType = type;
    }

    public void SetAsHintApp(Sprite imageSprite,GameObject targetObject)
    {
        functioning = true;
        isMinorClue = true;
        minorClueObject = targetObject;
        appImage.sprite = imageSprite;
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

            if (!isMinorClue)
            {
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
                minorClueObject.SetActive(true);
            }
        }
        else
        {
        //Display broken message
        }
    }
}
