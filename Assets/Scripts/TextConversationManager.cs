using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.XR;
using UnityEngine.Rendering;

public class TextConversationManager : MonoBehaviour
{
    public static TextConversationManager instance;

    [SerializeField] GameObject conversationView;
    [SerializeField] TextMeshProUGUI contactNameText;
    [SerializeField] Transform messagesParent;

    [SerializeField] GameObject otherMessage;
    [SerializeField] GameObject userMessage;

    [SerializeField] public ScrollRect conversationScrollRect;

    [Header("Setup")]
    [SerializeField] Transform conversationsParent;
    [SerializeField] GameObject conversationPrefab;

    [SerializeField] TextConversation[] randomConversations;
    [SerializeField] TextConversation[] clueConversations;
    [SerializeField] TextConversation[] regionClueConversations;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Setup();
    }

    void Setup() // to be called once clues are generated
    {
        foreach(Transform t in conversationsParent)
        {
            Destroy(t.gameObject);
        }

        int clueIndex = Random.Range(0, randomConversations.Length);
        //TODO: create either a random conversation or a clue conversation at random rather then in sequence?
        for (int i = 0; i < randomConversations.Length; i++)
        {
            CreateConversation(randomConversations[i]);
        }
        // todo: if there's a clue in this app
        //do all of them at once?
        List<string> chatClues = ClueManager.instance.GetCluesOfType(clueSource.CONVERSATION);
        if(chatClues.Count > 0)
        {
            int convIndex = Random.Range(0, clueConversations.Length);
            int regionConvIndex = Random.Range(0, regionClueConversations.Length);
            int chatClueIndex = Random.Range(0, chatClues.Count);
            foreach(string chatClue in chatClues)
            {
                string clueID = chatClues[chatClueIndex];

                if(ClueManager.instance.GetClue(clueID).containedRegion != clueRegion.NONE)
                {
                    CreateConversation_Clue(regionClueConversations[regionConvIndex], clueID);
                }
                else
                {
                    CreateConversation_Clue(clueConversations[convIndex], clueID);
                }

                if (convIndex >= clueConversations.Length)
                    convIndex = 0;
                regionConvIndex++;
                if (regionConvIndex >= regionClueConversations.Length)
                    regionConvIndex = 0;
                chatClueIndex++;
                if (chatClueIndex >= chatClues.Count)
                    chatClueIndex = 0;
                convIndex++;
            }
        }
    }

    void CreateConversation(TextConversation conversation)
    {
        TextConversationButton c = Instantiate(conversationPrefab, conversationsParent).GetComponent<TextConversationButton>();
        c.transform.SetSiblingIndex(Random.Range(0, c.transform.parent.childCount - 1));
        c.Setup(conversation);
    }

    void CreateConversation_Clue(TextConversation conversation, string clueID)
    {
        TextConversationButton c = Instantiate(conversationPrefab, conversationsParent).GetComponent<TextConversationButton>();
        c.transform.SetSiblingIndex(Random.Range(0, c.transform.parent.childCount - 1));
        c.Setup(conversation,clueID);
    }

    public void OpenConversation(TextConversation conversation, bool hasClue = false, string clueID = "")
    {
        contactNameText.text = conversation.contactName;

        foreach (Transform child in messagesParent)
        {
            Destroy(child.gameObject);
        }


        for (int i = 0; i < conversation.conversation.Length; i++) {
            TextConversation.Text text = conversation.conversation[i];

            GameObject prefab = text.isUser ? userMessage : otherMessage;
            Instantiate(prefab, messagesParent).GetComponentInChildren<TextMeshProUGUI>().text = (hasClue?BuildClueText(clueID,text.message):text.message);
        }

        conversationView.SetActive(true);

        //set clue as discovered if it has one?
        if (hasClue)
        {
            ClueManager.instance.FoundClue(clueID); 
        }
    }

    /// <summary>
    /// Build the clue text, substituting keywords with
    /// data from the attached clue
    /// </summary>
    /// <param name="clueID"></param>
    /// <param name="fullText"></param>
    /// <returns></returns>
    private string BuildClueText(string clueID, string fullText)
    {
        string outputString = string.Empty;
        clueClass attachedClue = ClueManager.instance.GetClue(clueID);
        int charIndex = fullText.IndexOf("{");
        int initialIndex = 0;
        while( charIndex != -1)
        {
            outputString += fullText.Substring(initialIndex, charIndex-initialIndex);
            initialIndex = charIndex + 1;
            charIndex = fullText.IndexOf("}", initialIndex);

            string keyword = fullText.Substring(initialIndex, charIndex-initialIndex);
            
            //replace LOC with landmark?
            if(keyword.Equals("LOC"))
            {
                outputString += attachedClue.containedLandmark;
            }
            //replace DISTANCE with distance
            else if(keyword.Equals("DISTANCE"))
            {
                outputString += (attachedClue.distance / 10).ToString();
            }
            else if(keyword.Equals("REGION"))
            {
                outputString += attachedClue.containedRegion.ToString();
            }

            initialIndex = charIndex + 1;
            charIndex = fullText.IndexOf("{", initialIndex);
        }

        if(outputString!= string.Empty)
        {
            outputString += fullText.Substring(initialIndex);
            return outputString;
        }
        else
        {
            return fullText;
        }
    }
}
