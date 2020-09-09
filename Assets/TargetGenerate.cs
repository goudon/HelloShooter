using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetGenerate : MonoBehaviour
{
    // Start is called before the first frame update
    public Target instantiateTarget;
    public Player player;
    public GlobalControl globalControl;
    public float maxTime = 60;
    public float elapsedTime = 0;
    public GameObject timeBar;
    private int totalTarget = 0;

    void Start()
    {
        elapsedTime = 0.0f;
        totalTarget = 0;
        Target[] stageTargets = gameObject.GetComponentsInChildren<Target>(true);
        foreach (Target target in stageTargets)
        {
            target.Activation(false);
            globalControl.addTargetCount();
        }
        timeBar.GetComponent<RectTransform>().sizeDelta = new Vector2(0,3);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime < maxTime)
        {
            //end stage
            Target[] stageTargets = gameObject.GetComponentsInChildren<Target>(true);
            foreach (Target target in stageTargets)
            {
                if (elapsedTime > target.generateTime)
                {
                    target.Activation(true);
                }
            }
            timeBar.GetComponent<RectTransform>().sizeDelta = new Vector2(550 * (elapsedTime/maxTime), 3);
        }
        else {
            globalControl.endStage();
        }
    }

}
