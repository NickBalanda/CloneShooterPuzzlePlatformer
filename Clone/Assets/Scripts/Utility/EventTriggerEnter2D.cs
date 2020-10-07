using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTriggerEnter2D : MonoBehaviour {

    public string tagTrigger = "Player";

    [Tooltip("Function called when trigger enter")]
    [SerializeField] // Serialize to show field in the editor
    public UnityEvent onTriggerEnter = new UnityEvent(); // our Unity Event

    [Header("GIVE THIS GAMEOBJECT A UNIQUE NAME!!!")]
    [Tooltip("Should we remove this component after event is triggered?")]
    public bool destroyScriptWhenEventTriggered = false;

    [Tooltip("Will the event trigger on collision?")]
    public bool triggeredOnCollision = true;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == tagTrigger) {
            if (triggeredOnCollision) {
                InvokeFunction();
            }
        }
    }

    public void InvokeFunction() {
        onTriggerEnter.Invoke();
        if (destroyScriptWhenEventTriggered) {
            Destroy(GetComponent<EventTriggerEnter2D>());
        }
    }
}
