using UnityEngine;

public class CameraZoom : MonoBehaviour {
    private float minFOV = 10.0f;
    private float maxFOV = 40.0f;
    private float sensitivity = 17.0f;

    void Update() {
        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * -sensitivity;
        fov = Mathf.Clamp(fov, minFOV, maxFOV);
        Camera.main.fieldOfView = fov;
    }
}