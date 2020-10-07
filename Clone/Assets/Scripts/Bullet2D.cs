using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2D : MonoBehaviour{

    Rigidbody2D rb;
    public bool isFromSelectedPlayer;

    void Start(){
        rb = GetComponent<Rigidbody2D>();       
    }
  
    void Update(){
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);      
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        StartCoroutine(HitSequence());
    }

    public IEnumerator HitSequence() {
        yield return new WaitForSeconds(0.2f);
        LevelManager.instance.AddNewPlayer(transform.position, isFromSelectedPlayer);
        Destroy(gameObject);
    }
}
