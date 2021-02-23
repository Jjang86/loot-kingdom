using UnityEngine;

public class TableTopCameraOrbit : MonoBehaviour {
    [SerializeField] private GameObject target;

    private float speed = 5.0f;
    private Vector3 lowerLimitPosition = new Vector3(40.04994f, 19.08083f, -4.350029f);
    private Vector3 lowerLimitRotation = new Vector3(24.334f, -83.436f, -18.109f);
    private Vector3 upperLimitPosition = new Vector3(5.522896f, 19.43174f, -39.73541f);
    private Vector3 upperLimitRotation = new Vector3(24.83f, -8.267f, 17.4f);

    void Update() {
        if (Input.GetMouseButton(1)) {
            transform.RotateAround(target.transform.position, transform.up, Input.GetAxis("Mouse X") * speed);
            // transform.RotateAround(target.transform.position, transform.right, Input.GetAxis("Mouse Y") * -speed);
            if (transform.position.x > 40.04994f) {
                transform.position = lowerLimitPosition;
                transform.eulerAngles = lowerLimitRotation;
            }
            if (transform.position.x < 5.522896f) {
                transform.position = upperLimitPosition;
                transform.eulerAngles = upperLimitRotation;
            }
            Debug.Log(transform.position.x);
        }
    }
}