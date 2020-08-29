using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

using UnityEngine;
public class Player : MonoBehaviour
{
    public GameObject bullet;
    public int[] maxAmmoLevelList = new int[6] { 20, 25, 30, 35, 40, 100 };
    private int[] penetrateLevelList = new int[6] { 1, 2, 3, 4, 5, 10 };
    private float[] reloadSpeedLevelList = new float[6] { 1.5f, 1.2f, 1.0f, 0.8f, 0.7f, 0.5f };
    private float[] damageLevelList = new float[6] { 1.0f, 1.5f, 2.0f, 2.5f, 3.0f, 5.0f };
    private float[] fireRateLevelList = new float[6] { 0.2f, 0.175f, 0.15f, 0.125f, 0.1f, 0.05f };
    private float[] recoilSuppressionLevelList = new float[6] { 0.01f, 0.1f, 0.2f, 0.35f, 0.6f, 0.9f };

    public int maxAmmoLevel = 0;
    public int penetrateLevel = 0;
    public int reloadSpeedLevel = 0;
    public int damageLevel = 0;
    public int fireRateLevel = 0;
    public int recoilSuppressionLevel = 5;
    private int maxAmmo, penetrate;
    private float reloadSpeed, damage, rappidRate, recoilSuppression;
    private bool isReload;
    public GameObject realoadUI, reloadBar, emptyAmmoUI;
    public int remainAmmo;
    public float currentReloadTime;
    private float maxRandRange = 2.0f, randAccerate = 0.1f, adjustDeltaTime = 2.0f;
    public float currentRandRange = 0.0f;
    private float fireRemainTime;
    private bool fireFlag;
    public int score, point;

    public AudioSource SoundFire;
    public AudioSource SoundReload;

    private string[] gameStatusList = new string[] { "in_game", "reinforcing" };
    private int targetLayerMask = 0;

    void Start()
    {
        Cursor.visible = false;
        targetLayerMask = LayerMask.GetMask("Target");
        fireRemainTime = 0.0f;
        rappidRate = fireRateLevelList[fireRateLevel];
        recoilSuppression = recoilSuppressionLevelList[recoilSuppressionLevel];
        if (recoilSuppression < 0.0f) recoilSuppression = 0.001f;
        penetrate = penetrateLevelList[penetrateLevel];
        damage = damageLevelList[damageLevel];
        score = 0;
        point = 0;
        remainAmmo = maxAmmoLevelList[maxAmmoLevel];
    }

    void Update()
    {
        // float scroll = Input.GetAxis("Mouse ScrollWheel");
        // Vector2 targetPos = Camera.main.ScreenPointToRay(Input.mousePosition);
        float recoilSuppressionNormal = (1 - recoilSuppression);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (currentRandRange < 0.0f) currentRandRange = 0.0f;

        // Debug.Log(Input.mousePosition);
        transform.position = (Vector2)ray.origin;
        if (Input.GetMouseButton(1))
        {
            Reload();
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 hitpos = transform.position;
            int bulletCount = 0;
            if (fireRemainTime < 0.0 && remainAmmo > 0 && !isReload)
            {
                // TODO : use shellBulletCount
                while (bulletCount < 1)
                {
                    // TODO : change base player
                    // get random range
                    float maxSupRange = (maxRandRange * recoilSuppressionNormal);
                    currentRandRange = currentRandRange > maxSupRange ? maxSupRange : currentRandRange;

                    Vector2 randomRange = new Vector2(UnityEngine.Random.Range(-currentRandRange, currentRandRange), UnityEngine.Random.Range(-currentRandRange, currentRandRange));
                    hitpos += randomRange;
                    // ray
                    RaycastHit2D[] hitList = Physics2D.RaycastAll(hitpos, (Vector2)ray.direction, Mathf.Infinity, targetLayerMask);
                    if (hitList.Length > 0)
                    {
                        var penetrateTargetCount = hitList.Length < penetrate ? hitList.Length : penetrate;
                        var sliceHitList = new ArraySegment<RaycastHit2D>(hitList, 0, penetrateTargetCount);
                        foreach (RaycastHit2D hit in sliceHitList)
                        {
                            // damage culc
                            var hitTarget = hit.collider.gameObject;
                            float dist = Vector2.Distance(hitTarget.transform.position, hit.point);
                            float rad = hitTarget.GetComponent<RectTransform>().localScale.x * hitTarget.GetComponent<CircleCollider2D>().radius;
                            float proc = (1 - dist / rad);
                            float damagePoint = damage * proc;
                            AddScore((int)(1 + (damagePoint * damagePoint)*10));
                            hitTarget.GetComponent<Target>().Damage(damagePoint);
                        }
                    }
                    Instantiate(bullet, hitpos, transform.rotation);
                    SoundFire.PlayOneShot(SoundFire.clip);

                    bulletCount++;
                }
                currentRandRange += randAccerate * recoilSuppressionNormal * adjustDeltaTime;
                fireRemainTime = rappidRate;
                remainAmmo--;
            }
            fireRemainTime -= Time.deltaTime;
        }

        // reload UI control
        if (currentReloadTime > 0)
        {
            currentReloadTime -= Time.deltaTime;
            currentReloadTime = currentReloadTime < 0 ? 0 : currentReloadTime;
            reloadBar.GetComponent<RectTransform>().sizeDelta = new Vector2(200 * (1 - currentReloadTime / reloadSpeedLevelList[reloadSpeedLevel]), 10);
        }
        else if (currentReloadTime <= 0 && isReload)
        {
            remainAmmo = maxAmmoLevelList[maxAmmoLevel];
            isReload = false;
            realoadUI.SetActive(false);
        }
        else if (currentReloadTime <= 0 && !isReload && remainAmmo <= 0)
        {
            emptyAmmoUI.SetActive(true);
        }
        currentRandRange -= Input.GetMouseButton(0) ? (Time.deltaTime / adjustDeltaTime) : Time.deltaTime * adjustDeltaTime;
        currentRandRange = currentRandRange < 0 ? 0 : currentRandRange;
    }
    void GunRebuildOnEdit()
    {

    }
    void Reload()
    {
        if (!isReload && remainAmmo != maxAmmoLevelList[maxAmmoLevel])
        {
            isReload = true;
            currentReloadTime = reloadSpeedLevelList[reloadSpeedLevel];
            realoadUI.SetActive(true);
            emptyAmmoUI.SetActive(false);
            SoundReload.PlayOneShot(SoundReload.clip);
        }
    }
    void ContinueInit()
    {

    }
    public void AddPoint(int inPoint) {
        point += inPoint;
     }
    public void AddScore(int inScore){
        score += inScore;
    }
}