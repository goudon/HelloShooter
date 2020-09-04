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
    public float distance = 1.0f, moveDirection = 1.0f, moveSpeed = 1.0f;

    private int isReflectX = 1, isReflectY = 1;
    private Vector2 movementVector;
    const float minX = -8.0f, minY = -4.0f, maxX = 8.0f, maxY = 3.0f;
    // static Vector2 randomRange = new Vector2(UnityEngine.Random.Range(-7.0f, 7.0f), UnityEngine.Random.Range(-5.0f, 5.0f));
    private Vector2 fixedPos;
    private float currentRot = 0;
    public Player player;
    public AudioSource soundCrush;

    // Start is called before the first frame update
    void Start()
    {
        remainSec = maxSec;
        remainHealth = maxHealth;
        fixedPos = gameObject.transform.position;
        currentRot = 0;
        if (targetType == 1 || targetType == 2)
        {
            Vector2 tempVec2 = new Vector2(moveSpeed, 0);
            movementVector = Quaternion.Euler(0, 0, moveDirection) * tempVec2;
        }
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
                checkDist(targetPos);
                gameObject.transform.position = new Vector2(targetPos.x + movementVector.x, targetPos.y + movementVector.y);
                break;
            case 2:
                // Refrection move
                checkWall(targetPos);
                gameObject.transform.position = new Vector2(targetPos.x + movementVector.x * isReflectX, targetPos.y + movementVector.y * isReflectY);
                break;
            case 3:
                // circurate move 
                gameObject.transform.position = new Vector2(fixedPos.x + distance, fixedPos.y);
                currentRot += moveSpeed;
                gameObject.transform.position = Quaternion.Euler(0, 0, currentRot) * gameObject.transform.position;
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
            soundCrush.PlayOneShot(soundCrush.clip);
        }
    }
    public void Activation(bool flag)
    {
        gameObject.SetActive(flag);
    }
    private void checkDist(Vector2 targetPos)
    {
        if (Vector2.Distance(fixedPos, targetPos) > distance)
        {
            movementVector = Quaternion.Euler(0, 0, 180) * movementVector;

        }
    }
    private void checkWall(Vector2 targetPos)
    {
        if (targetPos.x >= maxX)
        {
            isReflectX = -1;
        }
        else if (targetPos.x <= minX)
        {
            isReflectX = 1;
        }
        if (targetPos.y >= maxY)
        {
            isReflectY = -1;
        }
        else if (targetPos.y <= minY)
        {
            isReflectY = 1;
        }
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
