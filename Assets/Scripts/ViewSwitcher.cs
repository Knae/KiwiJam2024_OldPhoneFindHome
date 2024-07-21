using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSwitcher : MonoBehaviour
{
    bool mapView;
    [SerializeField] Animator referenceFrame;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetView(false);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetView(true);
        }
    }

    void SetView(bool right)
    {
        referenceFrame.SetTrigger(right ? "PanRight" : "PanLeft");

        mapView = right;

        if (mapView)
            Map.instance.ShowMap(); // update locations with clues
    }
}
