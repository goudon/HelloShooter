using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalControl : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentStage = 0;
    public int playerStatus = 0;
    public bool isReady = false;
    public bool isOption = false;
    public bool isEdit = false;
    public bool isExplanation = false;
    public bool isResult = false;
    public GameObject[] explanationUIs;
    private int explanationPage = 0;
    public GameObject editUI;
    public float StaticReadyTime = 3.0f;
    private float readyTime = 3.0f;
    public Text readyText;
    public GameObject resultUI;
    public Text resultText;
    public GameObject endResultUI;
    public Text endResultText;

    // mainMenu : 0 , stage : 1 , edit : 2
    public GameObject[] stages = new GameObject[5];

    // result status
    private int oneStageTargetTotalCount = 0, oneStageTargetTotalBreakCount = 0, targetTotalCount = 0, targetBreakCount = 0;
    private int oneStageFireCount = 0, oneStageFireHitCount = 0, fireCount = 0, fireHitCount = 0, reloadCount = 0;
    private int stageLevel = 0;
    private int oneStageScore = 0, oneStageEditPoint = 0, score = 0, editPoint = 0;
    private int maxAmmoLevel = 0, penetrateLevel = 0, reloadSpeedLevel = 0, damageLevel = 0, fireRateLevel = 0, recoilSuppressionLevel = 0;

    private string uuid = "";

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

            if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
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
                stageStart();
                isReady = false;
            }
            else
            {
                readyTime -= Time.deltaTime;
                readyText.text = "Ready : " + readyTime.ToString("f2");
            }
        }
        if (isEdit)
        {
            editUI.SetActive(true);
        }
    }
    public void stageStart()
    {
        Time.timeScale = 1;
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

    }
    public void resultEnd()
    {
        editUI.SetActive(true);
    }
    public void endResultStart()
    {
    }
    public void endResultEnd()
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
        isReady = true;
        readyTime = StaticReadyTime;

    }
    public void setTargetCount()
    {
        targetTotalCount++;
    }
    public void addBreakTargetCount()
    {
        oneStageTargetTotalBreakCount++;
    }
    public void addFireCount()
    {
        fireCount++;
    }
    public void addFireHitCount()
    {
        fireHitCount++;
    }
    public void addReloadCount()
    {
        reloadCount++;
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
}
