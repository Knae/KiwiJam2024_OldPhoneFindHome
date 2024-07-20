using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Dialogue")]
public class DialogueScript : ScriptableObject
{
    public TextConversation.Text[] conversation;
}
