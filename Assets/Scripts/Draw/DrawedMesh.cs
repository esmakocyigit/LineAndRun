using System;
using Dreamteck.Splines;
using UnityEngine;
using UniRx;

public class DrawedMesh : MonoBehaviour
{
    [SerializeField] private SplineComputer splineComputer;
    [SerializeField] private MeshGenerator meshGenerator;
    [SerializeField] private TubeGenerator tubeGenerator;
    [SerializeField] private SphereColliderGenerator sphereColliderGenerator;
    [SerializeField] private Rigidbody rigidbody;
    private MeshCreator meshCreator;
    public SplineComputer SplineComputer => splineComputer;
    public MeshGenerator MeshGenerator => meshGenerator;
    private void OnEnable()
    {
        meshCreator = FindObjectOfType<MeshCreator>();
        meshCreator.OnMeshGenerated.AddListener(OnMeshGenerated);
    }

    private void OnMeshGenerated()
    {
        ActivatePhysic();
    }

    private void ActivatePhysic()
    {
        Observable.Timer(TimeSpan.FromSeconds(0.2f)).Subscribe(delegate(long l)
        {
            sphereColliderGenerator.GenerateColliders();
            Destroy(tubeGenerator);
            Destroy(splineComputer);
            Destroy(meshGenerator);
            rigidbody.isKinematic = true;
            rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }).AddTo(this);
    }
}