using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    /********
    memo : 
    Target.SetActive(false);
    のときは、時間の減少が有効にならない。
    trueに変更されたタイミングで時間の減少が始まる。
    *********/
    public int destroyScore = 10000;
    public float generateTime = 10000;
    public float maxSec = 5.0f, maxHealth = 100.0f;
    public float remainSec, remainHealth;
    public int targetType = 0;
    public float moveDirection = 1.0f, moveSpeed = 1.0f;

    // static Vector2 randomRange = new Vector2(UnityEngine.Random.Range(-7.0f, 7.0f), UnityEngine.Random.Range(-5.0f, 5.0f));

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        remainSec = maxSec;
        remainHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPos = gameObject.transform.position;
        remainSec -= Time.deltaTime;
        if (remainSec <= 0.0f)
        {
            Destroy(gameObject);
        }
        switch (targetType)
        {
            case 0:
            // don't move

                break;
            case 1:
            // Repetition move
                gameObject.transform.position = new Vector2(targetPos.x + moveSpeed, targetPos.y);
                break;
            case 2:
            // Refrection move

                break;
            case 3:
            // circurate move 
                break;
        }
    }
    public void Damage(float damage)
    {
        remainHealth -= damage;
        if (remainHealth <= 0.0f)
        {
            Destroy(gameObject);
            player.AddScore(destroyScore);
            player.AddPoint(destroyScore);
        }
    }
    public void Activation(bool flag) {
        gameObject.SetActive(flag);
    }
    /*memo: 2020 08 12 Collisionで制御しようとした*/
    // void OnTriggerEnter2D(Collider2D c)
    // {
    //     string layerName = LayerMask.LayerToName(c.gameObject.layer);
    //     if (layerName == "Bullet")
    //     {
    //         Vector2 bulletPos = c.gameObject.transform.position;
    //         float dist = Vector2.Distance(bulletPos, transform.position);
    //         float rad = gameObject.GetComponent<RectTransform>().localScale.x * gameObject.GetComponent<CircleCollider2D>().radius;
    //         // TODO : check my layer count (z layer position)
    //         int myLayerPositionCount = 1;
    //         float proc = (1 - dist / rad);
    //         // TODO : Get Player Damage
    //         float damagePoint = 1 * proc;
    //         Damage(damagePoint);
    //     }

    // }
    // int CheckTargetLayerCount(Vector2 bulletPos)
    // {

    //     return 0;
    // }
}
