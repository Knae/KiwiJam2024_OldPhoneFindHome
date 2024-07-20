using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextConversationButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI contactName;
    [SerializeField] TextMeshProUGUI lastMessage;
    [SerializeField] Button button;
    
    public bool hasClue { get; private set; } = false;

    public string attachedClueID { get; private set; } = string.Empty;

    public void Setup(TextConversation conversation)
    {
        contactName.text = conversation.contactName;
        button.onClick.AddListener(() =>
        {
            TextConversationManager.instance.OpenConversation(conversation, hasClue,attachedClueID);
        });
    }

    //Overriden function to add clueID as well as setup the conversation
    public void Setup(TextConversation conversation, string clueID)
    {
        hasClue = true;
        attachedClueID = clueID;
        Setup(conversation);
    }
}
