using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapRegion : MonoBehaviour//InteractableAfterClue
{
    bool detailed = false;

    [SerializeField] GameObject detailedView;
    [SerializeField] GameObject simpleView;

    [SerializeField] Button button;

    RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

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
            rectTransform.sizeDelta = new Vector2(2000, 2000);
        }
        else
        {
            rectTransform.sizeDelta = new Vector2(150, 150);
        }
    }
}
