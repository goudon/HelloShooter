using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float alphaControlTime;
    public float destroyControlTime;
    public float waitTime;
    public float alpha;
    public bool hitFlag = false;
    public GameObject spriteObject; //bulletHole
    private float initializationTime;

    void Start()
    {
        initializationTime = Time.timeSinceLevelLoad;
    }
    void FixedUpdate()
    {
        float timeSinceInitialization = Time.timeSinceLevelLoad - initializationTime;
        if (waitTime < timeSinceInitialization)
        {
            if (destroyControlTime < timeSinceInitialization)
            {
                Destroy(gameObject);
            }

            spriteObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, alpha);
            alpha -= alphaControlTime;
            if (alpha <= 0f)
            {
                alpha = 0f;
            }
        }
    }
}