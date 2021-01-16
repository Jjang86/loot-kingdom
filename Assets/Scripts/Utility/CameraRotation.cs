using UnityEngine;

public class CameraRotation : MonoBehaviour {
    [SerializeField] private GameObject target;

    private float speed = 5.0f;
    private float minFOV = 35.0f;
    private float maxFOV = 100.0f;
    private float sensitivity = 17.0f;

    void Update() {
        if (Input.GetMouseButton(1)) {
            Debug.Log(Input.GetAxis("Mouse X"));
            Debug.Log(Input.GetAxis("Mouse Y"));
            transform.RotateAround(target.transform.position, transform.up, Input.GetAxis("Mouse X") * speed);
            transform.RotateAround(target.transform.position, transform.right, Input.GetAxis("Mouse Y") * -speed);
            Debug.Log("hi");
        }

        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * -sensitivity;
        fov = Mathf.Clamp(fov, minFOV, maxFOV);
        Camera.main.fieldOfView = fov;
    }
}