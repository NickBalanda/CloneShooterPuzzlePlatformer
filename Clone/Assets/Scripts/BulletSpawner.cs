using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour{

    public GameObject bullet;
    public float launchForce = 10;
    public float fireRate = 2;
    public bool isShooting = true;

    void Start(){
        StartCoroutine(ShootSequence());
    }
    public IEnumerator ShootSequence() {
        while (isShooting) {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }
    void Shoot() {
        MusicManager.instance.PlaySound("Gunshot");
        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
        newBullet.GetComponent<Rigidbody2D>().velocity = transform.transform.right * launchForce;
        
    }
}
