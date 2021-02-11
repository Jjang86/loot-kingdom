using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dice : View {
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private List<DiceSide> diceSides;
    private bool hasLanded = true;
    private bool thrown;
    private int diceValue;
    private Vector3 initialPosition;

    private void Start() {
        initialPosition = transform.position;
    }

    private void Update() {
        if (rigidBody.IsSleeping() && !hasLanded && thrown) {
            thrown = false;
            hasLanded = true;
            SideValueCheck();
            NotificationCenter.Notify(Notifications.Dice.animationComplete, diceValue);
        }
    }

    public void Roll() {
        if (!thrown && hasLanded) {
            thrown = true;
            hasLanded = false;
            transform.position = initialPosition;
            rigidBody.AddForce(new Vector3(RandomForce() ,RandomForce() ,RandomForce()), ForceMode.Impulse);
            rigidBody.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        }
    }

    private void SideValueCheck() {
        diceValue = 0;
        foreach (DiceSide side in diceSides) {
            if (side.onGround) {
                Debug.Log("Side Value: " + side.value);
                diceValue = side.value;
            }
        }
    }

    private float RandomForce() {
        return Random.Range(0, 10);
    }
}