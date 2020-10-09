using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour{

    public GameObject transitionPanel;
    Animator transitionAnim;

    public bool startTransition = true;

    public static LoadingManager instance;
    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
        transitionAnim = transitionPanel.GetComponent<Animator>();
    }

    private void Start() {
        if(startTransition)
            StartCoroutine(StartTransition());
    }
    public IEnumerator StartTransition() {
        transitionPanel.SetActive(true);
        transitionAnim.Play("transitionStart");
        yield return new WaitForSeconds(1);
        transitionPanel.SetActive(false);
    }

    public void LoadScene(int scene) {
        Time.timeScale = 1;
        StartCoroutine(LoadTransition(scene));
    }
    public void LoadNextScene() {
        Time.timeScale = 1;
        StartCoroutine(LoadTransition(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void ReloadScene() {
        Time.timeScale = 1;
        StartCoroutine(LoadTransition(SceneManager.GetActiveScene().buildIndex));
    }
    public IEnumerator LoadTransition(int scene) {
        transitionPanel.SetActive(true);
        Cursor.visible = true;
        transitionAnim.Play("transitionEnd");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }

    public void Quit() {
        Application.Quit();
    }
}
