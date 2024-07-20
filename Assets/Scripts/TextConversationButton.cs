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

    public void Setup(TextConversation conversation)
    {
        contactName.text = conversation.contactName;
        button.onClick.AddListener(() =>
        {
            TextConversationManager.instance.OpenConversation(conversation);
        });
    }
}
