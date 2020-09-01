using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOption : MonoBehaviour
{
    public GameObject soundOptionWindow;
    public GlobalControl gloabalObject;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OpenOption()
    {
        Time.timeScale = 0;
        soundOptionWindow.SetActive(true);
        gloabalObject.isOption = true;
        Cursor.visible = true;

    }
    public void CloseOption()
    {
        Time.timeScale = 1;
        soundOptionWindow.SetActive(false);
        gloabalObject.isOption = false;
        Cursor.visible = false;
    }
}
