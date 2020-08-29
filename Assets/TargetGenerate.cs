using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerate : MonoBehaviour
{
    // Start is called before the first frame update
    public Target instantiateTarget;
    public Player player;
    public StageControl stageControl;
    public float maxTime = 60 * 3;
    public float elapsedTime = 0;
    public GameObject timeBar;

    void Start()
    {
        elapsedTime = 0.0f;
        Target[] stageTargets = gameObject.GetComponentsInChildren<Target>(true);
        foreach (Target target in stageTargets)
        {
            Debug.Log(target);
            target.Activation(false);
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
        }
        else {
            stageControl.stageEnd();

        }
        // time progress bar
        timeBar.GetComponent<RectTransform>().sizeDelta = new Vector2(550 * (elapsedTime/maxTime), 3);
 

        // 
        // 
        //     instantiateTarget.maxSec = 2.0f;
        //     instantiateTarget.maxHealth = 2.0f;
        //     instantiateTarget.destroyScore = 500;
        //     instantiateTarget.player = player;
        //     Instantiate(instantiateTarget, randomRange, transform.rotation);
        //     generateTime = 2.0f;
        // 
        // 
    }
}
