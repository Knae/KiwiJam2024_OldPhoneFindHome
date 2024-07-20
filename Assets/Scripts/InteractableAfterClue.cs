using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableAfterClue : MonoBehaviour
{
    [SerializeField] public string clueID;

    [SerializeField] public Button button;

    public bool overrideInteractable = false;

    // Start is called before the first frame update
    void Start()
    {
        if(button == null)
        {
            button = GetComponent<Button>();
        }

        button.onClick.AddListener(() =>
        {
            button.interactable = false;
        });
    }

    private void OnEnable()
    {
        Check();
    }

    public void Check()
    {
        if(!overrideInteractable)
            button.interactable = ClueManager.instance.ClueRevealed(clueID);
    }
}
