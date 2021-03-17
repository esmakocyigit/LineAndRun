using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float pointSaveDistanceThreshold = 0.2f;
    [SerializeField] private MeshCreator meshCreator;
    private List<Vector3> linePoints = new List<Vector3>();
    private List<Vector3> colliderPoints = new List<Vector3>();
    private Vector3 lastLinePoint;
    private Camera camera;
    private bool isFingerDown;
    private Ray ray;
    private RaycastHit hit;

    public List<Vector3> LinePoints => linePoints;

    public List<Vector3> ColliderPoints => colliderPoints;
    private void OnEnable()
    {
        camera = Camera.main;
        meshCreator.OnMeshGenerated.AddListener(ClearPoints);
        LeanTouch.OnFingerDown += OnFingerDown;
        LeanTouch.OnFingerUp += OnFingerUp;
    }

    private void Update()
    {
        if(!isFingerDown) return;
        OnFixedUpdate();
    }

    private void FixedUpdate()
    {
        if(!isFingerDown) return;
        OnFixedUpdate();
    }

    private void LateUpdate()
    {
        if(!isFingerDown) return;
        OnFixedUpdate();
    }

    private void OnFixedUpdate()
    {
        ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("DrawArea")))
        {
            lineRenderer.gameObject.SetActive(true);
            Vector3 point = hit.point;
            point.x = 0;
            colliderPoints.Add(point);
            if (Vector3.Distance(lastLinePoint, point) > pointSaveDistanceThreshold)
            {
                linePoints.Add(point);
                lastLinePoint = point;
                SetLineRenderer();
            }
        }
    }

    private void SetLineRenderer()
    {
        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions(linePoints.ToArray());
    }

    private void OnFingerDown(LeanFinger obj)
    {
        isFingerDown = true;
    }

    private void OnFingerUp(LeanFinger obj)
    {
        isFingerDown = false;
        lineRenderer.gameObject.SetActive(false);
    }

    private void ClearPoints()
    {
        linePoints.Clear();
        colliderPoints.Clear();
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerDown -= OnFingerDown;
        LeanTouch.OnFingerUp -= OnFingerUp;
        meshCreator.OnMeshGenerated.RemoveListener(ClearPoints);

    }
}