using System.Collections.Generic;
using Dreamteck.Splines;
using Lean.Touch;
using UnityEngine;
using UnityEngine.Events;

public class MeshCreator : MonoBehaviour
{
    [SerializeField] private GameObject drawedMeshPrefab;
    [SerializeField] private LineGenerator lineGenerator;
    private UnityEvent onMeshGenerated = new UnityEvent();
    private bool isDestroyed;
    private Vector3 lastPos;
    private Vector3 currentPos;
    private float timer;

    public UnityEvent OnMeshGenerated => onMeshGenerated;

    private void OnEnable()
    {
        LeanTouch.OnFingerDown += OnFingerDown;
        LeanTouch.OnFingerUp += OnFingerUp;
    }
    private void OnFingerDown(LeanFinger obj)
    {
        isDestroyed = false;
        DestroyMesh();
        timer = 0;
        Time.timeScale = 0.25f;
    }

    private void DestroyMesh()
    {
        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);
    }

    private void OnFingerUp(LeanFinger obj)
    {
        GameObject drawedMesh = Instantiate(drawedMeshPrefab, transform);
        Generate(drawedMesh, lineGenerator.LinePoints);
        Time.timeScale = 1f;

    }


    private void Generate(GameObject drawMesh, List<Vector3> linePoints)
    {
        DrawedMesh drawedMesh = drawMesh.GetComponent<DrawedMesh>();
        for (var i = 0; i < linePoints.Count; i++)
        {
            SplinePoint point = new SplinePoint(linePoints[i]);
            drawedMesh.SplineComputer.SetPoint(i, point);
            drawedMesh.SplineComputer.SetPointNormal(i, Vector3.right);
        }
        drawedMesh.MeshGenerator.Bake(false, false);
        onMeshGenerated?.Invoke();
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerDown -= OnFingerDown;
        LeanTouch.OnFingerUp -= OnFingerUp;
    }
}