using UnityEngine;
using System.Collections;

public class ViewToCamera : MonoBehaviour
{
    public Camera my_camera;

    void Start()
    {
        my_camera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        transform.LookAt (transform.position + my_camera.transform.rotation * Vector3.back,
            my_camera.transform.rotation * Vector3.up);
    }
}
