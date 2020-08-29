using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageControl : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentStage = 0;
    public int playerStatus = 0;
    public bool isReady = false;
    // mainMenu : 0 , stage : 1 , edit : 2
    public GameObject[] stages = new GameObject[5];

    void Start()
    {
        stages[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void stageEnd() { }
    public void editEnd() { }
}
