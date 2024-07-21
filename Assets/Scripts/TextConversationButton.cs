using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextConversationButton : MonoBehaviour
{
    static string[] orderedDates =
    {
        "21/07/2024",
        "20/07/2024",
        "15/04/2024",
        "03/11/2023",
        "22/09/2023",
        "30/03/2023",
        "02/10/2022",
        "18/02/2015",
        "16/08/2006",
    };

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
            TextConversationManager.instance.conversationScrollRect.verticalNormalizedPosition = 0;
            TextConversationManager.instance.OpenConversation(conversation, hasClue,attachedClueID);
        });
        lastMessage.text = "Last message: " + orderedDates[this.transform.GetSiblingIndex()];
    }

    //Overriden function to add clueID as well as setup the conversation
    public void Setup(TextConversation conversation, string clueID)
    {
        hasClue = true;
        attachedClueID = clueID;
        Setup(conversation);
    }
}
