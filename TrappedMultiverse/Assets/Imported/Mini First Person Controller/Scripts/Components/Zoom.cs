using Cinemachine;
using UnityEngine;

[ExecuteInEditMode]
public class Zoom : MonoBehaviour
{
    CinemachineVirtualCamera camera;
    public float defaultFOV = 60;
    public float maxZoomFOV = 15;
    [Range(0, 1)]
    public float currentZoom;
    public float sensitivity = 1;


    void Awake()
    {
        // Get the camera on this gameObject and the defaultZoom.
        camera = GetComponent<CinemachineVirtualCamera>();
        if (camera)
        {
            defaultFOV = camera.m_Lens.FieldOfView;
        }
    }

    void Update()
    {
        // Update the currentZoom and the camera's fieldOfView.
        currentZoom += Input.mouseScrollDelta.y * sensitivity * .05f;
        currentZoom = Mathf.Clamp01(currentZoom);
        camera.m_Lens.FieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
    }
}
