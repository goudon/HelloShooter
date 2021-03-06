﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GlobalControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;
    public int currentStage = 0;
    public int playerStatus = 0;
    public bool isReady = false;
    public bool isOption = false;
    public bool isEdit = false;
    public bool isExplanation = false;
    public bool isResult = false;
    public bool isClearResult = false;
    public GameObject[] explanationUIs;
    private int explanationPage = 0;
    public GameObject editUI;
    public float StaticReadyTime = 3.0f;
    private float readyTime = 3.0f;
    public Text readyText;
    public GameObject resultUI;
    public Text resultText;
    public GameObject clearResultUI;
    public Text clearText, gradeText, scoreText;
    private int[] gradeRank = { 20000, 30000, 40000, 50000, 60000, 70000, 80000, 90000, 100000 };
    private string[] gradeRankText = { "F", "E", "D", "C", "B", "A", "S", "SS", "SSS", "Hello God" };
    // mainMenu : 0 , stage : 1 , edit : 2
    public GameObject[] stages = new GameObject[5];
    // result status
    private int oneStageTargetTotalCount = 0, oneStageTargetTotalBreakCount = 0;
    private int allTargetTotalCount = 0, allTargetBreakCount = 0;
    private int oneStageFireCount = 0, oneStageFireHitCount = 0;
    private int allFireCount = 0, allFireHitCount = 0, allReloadCount = 0;
    private int stageLevel = 0;
    private int oneStageScore = 0, oneStageEditPoint = 0, score = 0, editPoint = 0;
    private int maxAmmoLevel = 0, penetrateLevel = 0, reloadSpeedLevel = 0, damageLevel = 0, fireRateLevel = 0, recoilSuppressionLevel = 0;
    private string uuid = "";
    public AudioSource startStageSound,endStageSound;
    private AsyncOperation async;
    void Start()
    {
        foreach (GameObject page in explanationUIs)
        {
            page.SetActive(false);
        }
        explanationPage = 0;
        explanationUIs[explanationPage].SetActive(true);
        currentStage = 0;
        isExplanation = true;
        isReady = false;
        readyTime = StaticReadyTime;
        readyText.text = "";
        uuid = System.Guid.NewGuid().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (isExplanation)
        {
            Time.timeScale = 0;

            if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(0))
            {
                explanationUIs[explanationPage].SetActive(false);
                explanationPage++;
                if (explanationPage >= explanationUIs.Length)
                {
                    isExplanation = false;
                    Time.timeScale = 1;
                    isReady = true;
                }
                else
                {
                    explanationUIs[explanationPage].SetActive(true);
                }
            }
        }
        if (isReady)
        {
            if (readyTime < 0.05)
            {
                readyText.text = "";
                startStage();
                isReady = false;
            }
            else
            {
                readyTime -= Time.deltaTime;
                readyText.text = "Ready : " + readyTime.ToString("f2");
            }
        }
        if (isResult)
        {
            if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
            {
                endStageResult();
            }
        }
        if (isClearResult)
        {
            if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
            {
                endClearResult();
            }
        }
    }
    public void startStage()
    {
        Time.timeScale = 1;
        oneStageFireHitCount = 0;
        oneStageFireCount = 0;
        oneStageTargetTotalCount = 0;
        oneStageTargetTotalBreakCount = 0;
        stages[currentStage].SetActive(true);
        startStageSound.PlayOneShot(startStageSound.clip);
    }
    public void endStage()
    {
        stages[currentStage].SetActive(false);
        endStageSound.PlayOneShot(endStageSound.clip);
        currentStage++;
        if (stages.Length <= currentStage)
        {
            resultUI.SetActive(true);
            setScore();
            setEditPoint();
            setResultText();
            isResult = true;
            isClearResult = true;
        }
        else
        {
            resultUI.SetActive(true);
            setScore();
            setEditPoint();
            setResultText();
            playerStatus = 2;
            isResult = true;
        }
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public void endStageResult()
    {
        resultUI.SetActive(false);
        isResult = false;
        if (isClearResult)
        {
            clearResultUI.SetActive(true);
            setClearResultText();
        }
        else
        {
            isEdit = true;
            editUI.SetActive(true);
        }
    }
    public void editEnd()
    {
        isEdit = false;
        endStageSound.PlayOneShot(endStageSound.clip);
        editUI.SetActive(false);
        playerStatus = 1;
        Cursor.visible = false;
        Time.timeScale = 1;
        isReady = true;
        readyTime = StaticReadyTime;
    }
    public void endClearResult()
    {

    }


    private void setResultText()
    {
        // resultText
        float hitRatio = oneStageFireCount == 0 ? 0 : ((float)oneStageFireHitCount / (float)oneStageFireCount) * 100;
        resultText.text = "stage : " + currentStage + "\n";
        resultText.text += "\n";
        resultText.text += "break target : " + oneStageTargetTotalBreakCount + " / " + oneStageTargetTotalCount + "\n";
        resultText.text += "fire count : " + oneStageFireCount + "\n";
        resultText.text += "hit count : " + oneStageFireHitCount + "\n";
        resultText.text += "hit ratio : " + hitRatio.ToString("F2") + "% \n";
        resultText.text += "get score : " + oneStageScore + "\n";
        resultText.text += "get edit point : " + oneStageEditPoint + "\n";
        resultText.text += "\n";
        resultText.text += "score : " + score + "\n";
    }
    private void setClearResultText()
    {
        float hitRatio = oneStageFireCount == 0 ? 0 : ((float)allFireHitCount / (float)allFireCount) * 100;
        clearText.text = "total break target : " + allTargetBreakCount + " / " + allTargetTotalCount + "\n";
        clearText.text += "total fire count : " + allFireCount + "\n";
        clearText.text += "total hit count : " + allFireHitCount + "\n";
        clearText.text += "total hit ratio : " + hitRatio.ToString("F2") + "% \n";

        scoreText.text = "Your Score : " + player.score;
        int i = 0;
        foreach (int grade in gradeRank)
        {
            if (grade < player.score)
            {
                i++;
            }
        }
        gradeText.text = "Your Grade : " + gradeRankText[i];
    }
    public void addTargetCount()
    {
        allTargetTotalCount++;
        oneStageTargetTotalCount++;
    }
    public void addBreakTargetCount()
    {
        oneStageTargetTotalBreakCount++;
        allTargetBreakCount++;
    }
    public void addFireCount()
    {
        allFireCount++;
        oneStageFireCount++;
    }
    public void addFireHitCount()
    {
        allFireHitCount++;
        oneStageFireHitCount++;
    }
    public void addReloadCount()
    {
        allReloadCount++;
    }
    private void setScore()
    {
        oneStageScore = player.score - score;
        score = player.score;
    }
    private void setEditPoint()
    {
        oneStageEditPoint = player.point - editPoint;
        editPoint = player.point;
    }
    public void updateEditPoint(int inPoint)
    {
        editPoint = inPoint;
    }
    public void setLevel(int ammo = 0, int penetrate = 0, int reload = 0, int damage = 0, int fire = 0, int recoil = 0)
    {

        maxAmmoLevel = ammo;
        penetrateLevel = penetrate;
        reloadSpeedLevel = reload;
        damageLevel = damage;
        fireRateLevel = fire;
        recoilSuppressionLevel = recoil;
    }
    public void GoBackTitle()
    {
        // StartCoroutine(LoadScene());
        SceneManager.LoadScene("MainMenu");
    }
}
