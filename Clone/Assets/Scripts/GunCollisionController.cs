using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCollisionController : MonoBehaviour{

    public bool isHittingObstacle;

    private void OnTriggerEnter2D(Collider2D collision) {
        isHittingObstacle = true;
    }
    private void OnTriggerExit2D(Collider2D collision) {
        isHittingObstacle = false;
    }
}
