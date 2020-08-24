using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int destroyScore = 10000;
    [SerializeField]
    public float maxSec = 5.0f, maxHealth = 100.0f;
    [SerializeField]
    public float remainSec, remainHealth;
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
        remainSec -= Time.deltaTime;
        if (remainSec <= 0.0f) remainSec = maxSec;

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
