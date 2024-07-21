using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableAfterClue : MonoBehaviour
{
    [SerializeField] public string clueID;

    [SerializeField] public Button button;

    [SerializeField] Image glow;
    [SerializeField] RectTransform rectTransform;

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
        {
            bool clueRevealed = ClueManager.instance.ClueRevealed(clueID);
            button.interactable = clueRevealed;
            if (glow != null)
            {
                glow.enabled = clueRevealed;
            }
            if(rectTransform != null)
            {
                rectTransform.localScale = Vector3.one * (clueRevealed ? 1.1f : 1);
            }
        }
    }
}
