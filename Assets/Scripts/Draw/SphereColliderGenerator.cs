using Dreamteck.Splines;
using UnityEngine;

public class SphereColliderGenerator : SplineUser
{
    [SerializeField] private GameObject collidersGameObject;
    [SerializeField] [HideInInspector] protected Vector3[] vertices = new Vector3[0];
    protected float lastUpdateTime = 0f;
    [HideInInspector] public float updateRate = 0.1f;
    private bool updateCollider = false;
    [SerializeField] [HideInInspector] private float _offset = 0f;

    public float offset
    {
        get { return _offset; }
        set
        {
            if (value != _offset)
            {
                _offset = value;
                Rebuild();
            }
        }
    }

    protected override void LateRun()
    {
        base.LateRun();
        if (updateCollider)
        {
            if (Time.time - lastUpdateTime >= updateRate)
            {
                lastUpdateTime = Time.time;
                updateCollider = false;
            }
        }
    }

    protected override void Build()
    {
        base.Build();
        if (sampleCount == 0) return;
        if (vertices.Length != sampleCount) vertices = new Vector3[sampleCount];
        bool hasOffset = offset != 0f;
        for (int i = 0; i < sampleCount; i++)
        {
            GetSample(i, evalResult);
            vertices[i] = evalResult.position;
        }
    }

    protected override void PostBuild()
    {
        base.PostBuild();
        for (int i = 0; i < vertices.Length; i++) vertices[i] = transform.InverseTransformPoint(vertices[i]);

#if UNITY_EDITOR
        if (!Application.isPlaying || updateRate <= 0f)
        {
        }
        else updateCollider = true;
#endif
    }

    public void GenerateColliders()
    {
        for (var i = 0; i < vertices.Length; i++)
        {
            SphereCollider collider = collidersGameObject.AddComponent<SphereCollider>();
            collider.center = vertices[i];
            collider.radius = 0.40f;
        }
    }
}