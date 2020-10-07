using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger2D : MonoBehaviour{

    public string tagTrigger = "Player";

    [Space(10)]

    [Tooltip("Function called when trigger enter")]
    [SerializeField] 
    public UnityEvent onTriggerEnter = new UnityEvent(); 

    [Tooltip("Function called when trigger exit")]
    [SerializeField] 
    public UnityEvent onTriggerExit = new UnityEvent(); 

    public bool destroyScriptWhenEventTriggered = false;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == tagTrigger) {
            onTriggerEnter.Invoke();
            if (destroyScriptWhenEventTriggered) {
                Destroy(GetComponent<EventTriggerEnter2D>());
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == tagTrigger) {
            onTriggerExit.Invoke();
        }
    }

}
