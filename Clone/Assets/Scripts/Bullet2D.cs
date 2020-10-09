using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2D : MonoBehaviour{

    Rigidbody2D rb;
    public float timeToBurst = 0.2f;
    public bool isFromSelectedPlayer;
    public GameObject destroyedParticle;
    public bool enemyBullet;

    void Start(){
        rb = GetComponent<Rigidbody2D>();       
    }
  
    void Update(){
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);      
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        MusicManager.instance.PlaySound("cute_bounce_01");
        if(collision.gameObject.tag == "Player" && enemyBullet) {
            collision.gameObject.GetComponent<PlayerController2D>().Kill();
        } else {
            StartCoroutine(HitSequence());
        }
    }

    public IEnumerator HitSequence() {
        yield return new WaitForSeconds(timeToBurst);
        MusicManager.instance.PlaySound("tiny_whitenoise_laser_rnd_01");
        LevelManager.instance.AddNewPlayer(transform.position, isFromSelectedPlayer);
        Instantiate(destroyedParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
