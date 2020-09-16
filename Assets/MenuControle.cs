using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuControle : MonoBehaviour
{
    // Start is called before the first frame update
    private AsyncOperation async;
    private bool isClick,isBlack;
    public Animator CloseTransition;
    public AudioSource StartFire;
    public float transitionTime = 1.0f;
    void Start()
    {
        Time.timeScale = 1;
        isClick = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isClick && Input.GetMouseButtonDown(0)) {
            isClick = true;
            StartFire.PlayOneShot(StartFire.clip);
            LoadNextScene();
        }
    }
    public void LoadNextScene() {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene() {
        CloseTransition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        async = SceneManager.LoadSceneAsync("MainGame");
        while (!async.isDone) {
            //progress
            yield return null;
        }
    }
}
