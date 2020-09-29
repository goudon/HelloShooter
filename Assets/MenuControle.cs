using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuControle : MonoBehaviour
{
    public static MenuControle Instance{
		get; private set;
	}
    // Start is called before the first frame update
    private AsyncOperation async;
    private bool isClick, isBlack;
    public Animator CloseTransition;
    public AudioSource StartFire, debugOn, debugOff;
    public float transitionTime = 1.0f;
    public Text debugText;
    public bool isDebug = false;

    void Awake()
	{
		if (Instance != null) {
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad (gameObject);

		isDebug = false;
	}
    void Start()
    {
        Time.timeScale = 1;
        isClick = false;
        isDebug = false;
        debugText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (!isClick && Input.GetMouseButtonDown(0))
        {
            isClick = true;
            StartFire.PlayOneShot(StartFire.clip);
            LoadNextScene();
        }
        if (Input.GetMouseButtonDown(2))
        {
            isDebug = !isDebug;
            if (isDebug)
            {
                debugOn.PlayOneShot(debugOn.clip);
                debugText.text = "debug mode";
            }
            else
            {
                debugOff.PlayOneShot(debugOff.clip);
                debugText.text = "";
            }
            
        }
    }
    public void LoadNextScene()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        CloseTransition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        async = SceneManager.LoadSceneAsync("MainGame");
        while (!async.isDone)
        {
            //progress
            yield return null;
        }
    }
}
