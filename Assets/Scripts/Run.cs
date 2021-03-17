using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    void Update()
    {
        Running();
    }

    void Running()
    {
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
    }
}
