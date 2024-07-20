using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapLocation : InteractableAfterClue
{
    [SerializeField] GameObject radiusImage;
    RectTransform radiusTransform;
    [SerializeField] public float radius;

    public Map.LandmarkType landmarkType;
    public Image image;

    public string landmarkName;
    public string GetName
    {
        get
        {
            if (!string.IsNullOrEmpty(landmarkName))
            {
                return landmarkName;
            }
            else
            {
                return landmarkType.ToString() + Random.Range(0, 999);
            }
        }
    }

    public RectTransform RectTransform
    {
        get { return GetComponent<RectTransform>();  }
    }

    [SerializeField] TextMeshProUGUI label;

    private void Start()
    {
        label = GetComponentInChildren<TextMeshProUGUI>();

        label.gameObject.SetActive(!string.IsNullOrEmpty(landmarkName));
        label.text = GetName;

        button.onClick.AddListener(RevealRadius);
        radiusTransform = radiusImage.GetComponent<RectTransform>();
    }

    void RevealRadius()
    {
        radiusImage.SetActive(true);
        radiusTransform.sizeDelta = Vector2.one * radius * 2;
    }

}
