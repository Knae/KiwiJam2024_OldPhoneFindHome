using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public static Dialogue instance;

    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] Image chatBox;

    [SerializeField] Sprite playerSpeech;
    [SerializeField] Sprite otherCharacterSpeech;

    [SerializeField] DialogueScript currentConversation;
    [SerializeField] int index = 0;

    [SerializeField] DialogueScript openingDialogue;

    [SerializeField] DialogueScript[] corruptedAppDialogues;

    private void Awake()
    {
        instance = this;
    }

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

    public void CorruptedApp()
    {
        StartConversation(corruptedAppDialogues[Random.Range(0, corruptedAppDialogues.Length)]);
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

                return;
            }
        }

        TextConversation.Text line = currentConversation.conversation[index];

        chatBox.sprite = line.isUser ? playerSpeech : otherCharacterSpeech;
        dialogueText.text = line.message;
    }
}
