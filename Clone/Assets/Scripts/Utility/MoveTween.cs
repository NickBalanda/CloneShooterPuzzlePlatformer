using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveTween : MonoBehaviour{

    public bool enableOnStart;
    public float startDelay;

    public float duration;

    public Vector3 endValue;

    public Ease ease;

    void Start(){
        if (enableOnStart)
            Move();
    }

    public void Move() {
        transform.DOMove(endValue, duration).SetDelay(startDelay);
    }
}
