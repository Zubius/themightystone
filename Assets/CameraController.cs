using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform Target;

    public float ZoomSpeedTouch = 0.05f;
    public float ZoomSpeedMouse = 0.5f;
    public float RotateSpeedTouch = 5f;
    public float RotateSpeedMouse = 10f;
    public float[] ZoomBounds = new float[] { 10f, 85f };


    private Camera _camera;

    public float rotateMod;
    private bool wasZoomingLastFrame;
    private Vector2[] lastZoomPositions;

    // Use this for initialization
    void Awake () {
        _camera = GetComponent<Camera>();
        this.transform.LookAt(Target);
        rotateMod = Application.isEditor ? RotateSpeedMouse : RotateSpeedTouch;
    }
	
	// Update is called once per frame
	void Update () {
		if (Target != null && Input.GetMouseButton(0))
        {
            HandleRotate();
        }

        if (Input.touchSupported)
        {
            HandleTouch();
        } 
        else
        {
            HandleMouse();
        }
    }

    private void HandleRotate()
    {
        if (Input.touchSupported && Input.touchCount > 1 || Input.GetTouch(0).phase != TouchPhase.Moved)
            return;

        this.transform.RotateAround(Target.position, Vector3.up, Input.GetAxis("Mouse X") * rotateMod);
        this.transform.LookAt(Target);
    }

    private void HandleTouch()
    {
        if (Input.touchCount == 2)
        {
            var newPositions = new Vector2[] { Input.GetTouch(0).position, Input.GetTouch(1).position };

            if (!wasZoomingLastFrame)
            {
                lastZoomPositions = newPositions;
                wasZoomingLastFrame = true;
            }
            else
            {
                float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                float oldDistance = Vector2.Distance(lastZoomPositions[0], lastZoomPositions[1]);
                float offset = newDistance - oldDistance;

                ZoomCamera(offset, ZoomSpeedTouch);

                lastZoomPositions = newPositions;
            }
        }
        else
        {
            wasZoomingLastFrame = false;
        }
    }

    private void HandleMouse()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scroll, ZoomSpeedMouse);
    }

    private void ZoomCamera(float offset, float speed)
    {
        if (offset == 0)
            return;

        _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView - (offset * speed), ZoomBounds[0], ZoomBounds[1]);
    }
}
