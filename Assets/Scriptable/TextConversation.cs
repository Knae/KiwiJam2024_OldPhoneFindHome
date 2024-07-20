using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/TextConversation")]
public class TextConversation : ScriptableObject
{
    [System.Serializable]
    public struct Text
    {
        public string message;
        public bool isUser;
    }

    public string contactName;

    public Text[] conversation;
}
