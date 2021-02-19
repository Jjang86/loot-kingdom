using UnityEngine;

public class CameraOrbit : MonoBehaviour {
    [SerializeField] private GameObject target;

    private float speed = 5.0f;

    void Update() {
        if (Input.GetMouseButton(1)) {
            transform.RotateAround(target.transform.position, transform.up, Input.GetAxis("Mouse X") * speed);
        }
    }
}