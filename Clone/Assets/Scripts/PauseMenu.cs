using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenu;
    CanvasGroup canvasGroupMenu;

    bool ispaused;
    public float tweenTime = 0.3f;
    public Ease ease = Ease.Linear;
    bool isTweening;

    void Start() {
        canvasGroupMenu = pauseMenu.GetComponent<CanvasGroup>();
        canvasGroupMenu.alpha = 0;
    }
    void Update () {
        if (Input.GetButtonDown("Cancel")) {
            if (ispaused) {
                UnpauseGame();
            } else {
                PauseGame();
            }
        }
    }

    public void PauseGame() {
        Cursor.visible = true;
        if (!ispaused) {
            if (!isTweening)
                StartCoroutine(Pause());
        }
    }
    IEnumerator Pause() {
        //LevelManager2D.instance.DisablePlayerControl();
        MusicManager.instance.PauseMusic(true);
        MusicManager.instance.PlaySound("ui_pause");
        
        isTweening = true;
        pauseMenu.SetActive(true);
        //pauseMenu.transform.localScale = Vector3.zero;
        //Tween myTween = pauseMenu.transform.DOScale(1, tweenTime).SetEase(ease);
        Tween myTween = canvasGroupMenu.DOFade(1, tweenTime).SetEase(ease);
        yield return myTween.WaitForCompletion();
        ispaused = true;
        isTweening = false;
        Time.timeScale = 0;
    }

    public void UnpauseGame() {
        Cursor.visible = false;
        if (ispaused) {
            if (!isTweening)
                StartCoroutine(Unpause());
        }
    }
    IEnumerator Unpause() {
        MusicManager.instance.PauseMusic(false);
        Time.timeScale = 1;
        isTweening = true;
        //Tween myTween = pauseMenu.transform.DOScale(0, tweenTime).SetEase(ease);
        Tween myTween = canvasGroupMenu.DOFade(0, tweenTime).SetEase(ease);
        yield return myTween.WaitForCompletion();
        pauseMenu.SetActive(false);
        ispaused = false;
        isTweening = false;
        //LevelManager2D.instance.EnablePlayerControl();
    }

}
