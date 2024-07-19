using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRegion : InteractableAfterClue
{
    bool detailed = false;

    [SerializeField] GameObject detailedView;
    [SerializeField] GameObject simpleView;

    RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
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
            rectTransform.sizeDelta = new Vector2(2000, 2000);
        }
        else
        {
            rectTransform.sizeDelta = new Vector2(150, 150);
        }
    }
}
