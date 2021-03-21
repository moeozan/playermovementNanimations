using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSens = 15f;
    public float zoomSpeed= 20f;
    public float zoomMin = 30f;
    public float zoomMax = 70f;

    private float z;
    private Camera mainCamera;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        z = mainCamera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        z -= Input.GetAxis("Mouse ScrollWheel") * zoomSens;
        z = Mathf.Clamp(z,zoomMin, zoomMax);
    }

    private void LateUpdate()
    {
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView,z,Time.deltaTime * zoomSpeed);
    }
}
