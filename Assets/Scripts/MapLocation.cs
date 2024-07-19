using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLocation : InteractableAfterClue
{
    [SerializeField] GameObject radiusImage;
    RectTransform radiusTransform;
    [SerializeField] float radius;

    private void Start()
    {
        button.onClick.AddListener(RevealRadius);
        radiusTransform = radiusImage.GetComponent<RectTransform>();
    }

    void RevealRadius()
    {
        radiusImage.SetActive(true);
        radiusTransform.sizeDelta = Vector2.one * radius;
    }
}
