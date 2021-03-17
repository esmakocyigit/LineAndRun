using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    public Camera cam = null;
    public LineRenderer LineRenderer = null;
    private Vector3 mousePos;
    private Vector3 Pos;
    private Vector3 previousPos;
    public List<Vector3> linePositions = new List<Vector3>();
    public float minimumDistance = 0.05f;
    private float distance = 0;

    void Update ()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
            mousePos.z = 0;
            Pos = cam.ScreenToWorldPoint(mousePos);
            previousPos = Pos;
            linePositions.Add(Pos);
        }
        else if (Input.GetMouseButton(0))
        {
            mousePos = Input.mousePosition;
            mousePos.z = 0;
            Pos = cam.ScreenToWorldPoint(mousePos);
            distance = Vector3.Distance(Pos,previousPos);
            if(distance >= minimumDistance)
            {
                previousPos = Pos;
                linePositions.Add(Pos);
                LineRenderer.positionCount = linePositions.Count;
                LineRenderer.SetPositions(linePositions.ToArray());
            }
        }
    }
}
