using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class appButtonScript : MonoBehaviour
{
    [SerializeField] private bool functioning = false;
    [SerializeField] private clueSource appType = clueSource.NONE;
    [SerializeField] private Image appImage;

    [SerializeField] private GameObject chatModule;

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
            switch (appType)
            {
                case clueSource.CONVERSATION:
                    {
                        chatModule.SetActive(true);
                        HomeScreen.instance.HideShowScreen();
                        break;
                    }
                case clueSource.TRAVEL:
                    {
                        break;
                    }
                case clueSource.PHOTO:
                    {
                        break;
                    }
            } 
        }
        else
        {
        //Display broken message
        }
    }
}
