using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] Image chatBox;

    [SerializeField] Sprite playerSpeech;
    [SerializeField] Sprite otherCharacterSpeech;

    [SerializeField] DialogueScript currentConversation;
    [SerializeField] int index = 0;

    [SerializeField] DialogueScript openingDialogue;

    private void Start()
    {
        StartConversation(openingDialogue);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(currentConversation != null)
            {
                NextLine();
            }
        }
    }

    public void StartConversation(DialogueScript conversation)
    {
        currentConversation = conversation;
        chatBox.gameObject.SetActive(true);
        NextLine(0);
    }

    public void NextLine(int _index = -1)
    {
        if(_index > -1)
            index = _index;
        else
        {
            index++;

            if (index >= currentConversation.conversation.Length)
            {
                currentConversation = null;
                chatBox.gameObject.SetActive(false);
            }
        }

        TextConversation.Text line = currentConversation.conversation[index];

        chatBox.sprite = line.isUser ? playerSpeech : otherCharacterSpeech;
        dialogueText.text = line.message;
    }
}
