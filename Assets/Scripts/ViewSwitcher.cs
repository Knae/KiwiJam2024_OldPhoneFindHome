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
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            SetView(!mapView);
        }
    }

    void SetView(bool right)
    {
        referenceFrame.SetTrigger(right ? "PanRight" : "PanLeft");

        mapView = right;
    }
}
