using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCollisionController : MonoBehaviour{

    public bool isHittingObstacle;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Obstacle")
            isHittingObstacle = true;
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Obstacle")
            isHittingObstacle = false;
    }
}
