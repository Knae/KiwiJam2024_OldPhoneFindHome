using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapRegion : MonoBehaviour//InteractableAfterClue
{
    bool detailed = false;

    [SerializeField] GameObject detailedView;
    [SerializeField] GameObject simpleView;

    public Transform locationsParent;
    public Vector2 minLocationPos;
    public Vector2 maxLocationPos;

    [SerializeField] Button button;

    RectTransform rectTransform;
    [SerializeField] Vector2 largeSize;
    [SerializeField] Vector2 smallSize;

    public clueRegion clueRegion;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            Map.instance.ViewRegion(this);
        });
    }

    public void ToggleView()
    {
        ToggleView(!detailed);
    }
    public void ToggleView(bool _detailed)
    {
        detailed = _detailed    ;

        detailedView.SetActive(detailed);
        simpleView.SetActive(!detailed);

        button.enabled = !detailed;

        // todo: smoothly animate
        if(detailed)
        {
            rectTransform.sizeDelta = largeSize;
        }
        else
        {
            rectTransform.sizeDelta = smallSize;
        }
    }
}
