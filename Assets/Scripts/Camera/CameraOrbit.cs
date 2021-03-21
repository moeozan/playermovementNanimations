using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : Orbit
{
    public Vector3 targetOffset = new Vector3(0,2,0);
    public Vector3 cameraPositionZoom = new Vector3(-0.5f,0,0);
    public float cameraLenght = -10f;
    public float cameraLenghtZoom = -5f;
    public Vector2 orbitSpeed = new Vector2(0.01f,0.01f);
    public Vector2 orbitOffset = new Vector2(0,-0.8f);
    public Vector2 angleOffset = new Vector2(0,-0.25f);

    private float zoomValue;
    private Vector3 cameraPositionTemp;
    private Vector3 cameraPosition;

    private Transform playerTarget;
    private Camera mainCamera;




    // Start is called before the first frame update
    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;

        sphericalVector.Lenght = cameraLenght;
        sphericalVector.Azimuth = angleOffset.x;
        sphericalVector.Zenith = angleOffset.y;

        mainCamera = Camera.main;

        cameraPositionTemp = mainCamera.transform.localPosition;
        cameraPosition = cameraPositionTemp;

        MouseLock.MouseLocked = true;

    }

    // Update is called once per frame
    void Update()
    {
        HandleCamera();
        MouseLockHandler();
    }

    void HandleCamera()
    {

        if (MouseLock.MouseLocked) 
        {
            sphericalVector.Azimuth += Input.GetAxis("Mouse X") * orbitSpeed.x;
            sphericalVector.Zenith += Input.GetAxis("Mouse Y") * orbitSpeed.y;
        }
        

        sphericalVector.Zenith = Mathf.Clamp(sphericalVector.Zenith + orbitOffset.x, orbitOffset.y, 0f);

        float distanceToObject = zoomValue;
        float deltaDinstance = Mathf.Clamp(zoomValue,distanceToObject, -distanceToObject);
        sphericalVector.Lenght += (deltaDinstance - sphericalVector.Lenght);

        Vector3 lookAt = targetOffset;
        lookAt += playerTarget.position;

        base.FixedUpdate();

        transform.position += lookAt;
        transform.LookAt(lookAt);

        if (zoomValue == cameraLenghtZoom)
        {
            Quaternion targetRotation = transform.rotation;
            targetRotation.x = 0;
            targetRotation.z = 0;
            playerTarget.rotation = targetRotation;
        }
        cameraPosition = cameraPositionTemp;
        zoomValue = cameraLenght;
    }

    void MouseLockHandler()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (MouseLock.MouseLocked)
            {
                MouseLock.MouseLocked = false;
            }
            else
            {
                MouseLock.MouseLocked = true;
            }
        }
    }
}
