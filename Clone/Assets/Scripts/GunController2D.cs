using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController2D : MonoBehaviour
{
    public GameObject cursor;

    public GameObject gun;
    public GunCollisionController gunCollisionController;
    SpriteRenderer gunSprite;

    public GameObject bullet;
    public float launchForce;
    public Transform shotPoint;

    private Vector2 mousePosition;
    private Camera cam;
    PlayerController2D pc;

    void Start(){
        Cursor.visible = false;
        cam = Camera.main;
        gunSprite = gun.GetComponentInChildren<SpriteRenderer>();
        pc = GetComponent<PlayerController2D>();
    }

    // Update is called once per frame
    void LateUpdate(){
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 gunPosition = gun.transform.position;
        Vector2 direction = mousePosition - gunPosition;

        cursor.transform.position = mousePosition;
        gun.transform.right = direction;

        FlipDetection();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }

    void Shoot() {
        if (!gunCollisionController.isHittingObstacle) {
            GameObject newBullet = Instantiate(bullet, shotPoint.position, shotPoint.rotation);
            newBullet.GetComponent<Rigidbody2D>().velocity = shotPoint.transform.right * launchForce;
            newBullet.GetComponent<Bullet2D>().isFromSelectedPlayer = pc.isCurrentSelected;
        }
    }

    void FlipDetection() {
        if (pc.facingRight == false && mousePosition.x > pc.transform.position.x) {
            pc.Flip();
            gun.transform.localScale *= -1;
        } else if (pc.facingRight == true && mousePosition.x < pc.transform.position.x) {
            pc.Flip();
            gun.transform.localScale *= -1;
        }
    }
}
