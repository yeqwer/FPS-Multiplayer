using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HpAlwaysToCamera : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        Vector3 v = cam.transform.position - transform.position;

        v.x = v.z = 0.0f;

        transform.LookAt(cam.transform.position - v); 

        transform.Rotate(0,180,0);
    }
}