using UnityEngine;

public class DiceSide : MonoBehaviour {
    public int value;
    [HideInInspector] public bool onGround;

    private void OnTriggerStay(Collider collider) {
        if (collider.tag == "Ground") {
            onGround = true;
        }
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.tag == "Ground") {
            onGround = false;
        }
    }
}