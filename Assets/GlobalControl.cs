using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentStage = 0;
    public int playerStatus = 0;
    public bool isReady = false;
    public bool isOption = false;
    public bool isEdit = false;
    public GameObject editUI;
    // mainMenu : 0 , stage : 1 , edit : 2
    public GameObject[] stages = new GameObject[5];

    void Start()
    {
        currentStage = 0;
        stageStart();

    }

    // Update is called once per frame
    void Update()
    {
        if (isEdit)
        {
            editUI.SetActive(true);
        }
    }
    public void stageStart()
    {
        stages[currentStage].SetActive(true);
    }
    public void stageEnd()
    {
        stages[currentStage].SetActive(false);
        currentStage++;
        if (stages.Length < currentStage)
        {
            Debug.Log("game end");
        }
        isEdit = true;
        playerStatus = 2;
        Time.timeScale = 0;
        Cursor.visible = true;
    }
    public void resultStart()
    {
        resultStart();
    }
    public void resultEnd()
    {
        editUI.SetActive(true);


    }
    public void editEnd()
    {
        isEdit = false;
        editUI.SetActive(false);
        playerStatus = 1;
        Cursor.visible = false;
        Time.timeScale = 1;
        stageStart();
    }
}
