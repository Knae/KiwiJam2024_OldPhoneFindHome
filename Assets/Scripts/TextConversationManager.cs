using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextConversationManager : MonoBehaviour
{
    public static TextConversationManager instance;

    [SerializeField] GameObject conversationView;
    [SerializeField] TextMeshProUGUI contactNameText;
    [SerializeField] Transform messagesParent;

    [SerializeField] GameObject otherMessage;
    [SerializeField] GameObject userMessage;

    [Header("Setup")]
    [SerializeField] Transform conversationsParent;
    [SerializeField] GameObject conversationPrefab;

    [SerializeField] TextConversation[] randomConversations;
    [SerializeField] TextConversation[] clueConversations;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        Setup();
    }

    void Setup() // to be called once clues are generated
    {
        int clueIndex = Random.Range(0, randomConversations.Length);

        for (int i = 0; i < clueIndex; i++)
        {
            CreateConversation(randomConversations[i]);
        }
        // todo: if there's a clue in this app
        if(true)
        {
            CreateConversation(clueConversations[Random.Range(0, clueConversations.Length)]);
        }
        for (int i = clueIndex; i < randomConversations.Length; i++)
        {
            CreateConversation(randomConversations[i]);
        }
    }

    void CreateConversation(TextConversation conversation)
    {
        TextConversationButton c = Instantiate(conversationPrefab, conversationsParent).GetComponent<TextConversationButton>();
        c.Setup(conversation);
    }

    public void OpenConversation(TextConversation conversation)
    {
        contactNameText.text = conversation.contactName;

        foreach (Transform child in messagesParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < conversation.conversation.Length; i++) {
            TextConversation.Text text = conversation.conversation[i];

            GameObject prefab = text.isUser ? userMessage : otherMessage;
            Instantiate(prefab, messagesParent).GetComponentInChildren<TextMeshProUGUI>().text = text.message;
        }

        conversationView.SetActive(true);
    }
}
