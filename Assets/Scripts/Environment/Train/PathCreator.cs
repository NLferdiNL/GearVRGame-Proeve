using Curves;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{

    [HideInInspector]
    public Path path;

    //The color of an anchor point
    public Color anchorCol = Color.red;
    //The color of a control point
    public Color controlCol = Color.white;
    //The color of a segment section
    public Color segmentCol = Color.green;
    //The color of a segment section when selected
    public Color selectedSegmentCol = Color.yellow;
    //The diameter of an anchor point
    public float anchorDiameter = .1f;
    //The diameter of a control point
    public float controlDiameter = .075f;
    //This can let the user decide if they want to see the control points as it can get clutterd when there are loads
    public bool displayControlPoints = true;


    /// <summary>
    /// This function lets the user to create a new Path by clicking this button in the editor
    /// </summary>
    public void CreatePath()
    {
        path = new Path(transform.position);
    }

    /// <summary>
    /// This lets the user reset the path they are making
    /// </summary>
    void Reset()
    {
        CreatePath();
    }
}