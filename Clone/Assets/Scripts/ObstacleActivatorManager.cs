using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleActivatorManager : MonoBehaviour{

    public UnityEvent onActivated = new UnityEvent();

    public int numberToActivate = 2;
    int currentNumberToActivate;

    private void Start() {
        currentNumberToActivate = 0;
    }

    public void AddCount() {
        currentNumberToActivate++;
        if (currentNumberToActivate == numberToActivate)
            onActivated.Invoke();
    }
    public void RemoveCount() {
        currentNumberToActivate--;
    }

}
