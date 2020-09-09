using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    public Player player;
    private GameObject[] reticles;
    private Vector3[] reticleRot = new Vector3[4] {
        new Vector3(0.0f, 0.0f, 0.0f),
        new Vector3(0.0f, 0.0f, 90.0f),
        new Vector3(0.0f, 0.0f, 180.0f),
        new Vector3(0.0f, 0.0f, 270.0f)
    };
    private Vector3[] reticleDir = new Vector3[4] {
        new Vector3(1.0f, 0.0f, 0.0f),
        new Vector3(0.0f, 1.0f, 0.0f),
        new Vector3(-1.0f, 0.0f, 0.0f),
        new Vector3(0.0f, -1.0f, 0.0f)
    };
    private Vector3[] reticlePos = new Vector3[4] {
        new Vector3(0.2f, 0.0f, 0.0f),
        new Vector3(-0.0f, 0.2f, 0.0f),
        new Vector3(-0.2f, 0.0f, 0.0f),
        new Vector3(-0.0f, -0.2f, 0.0f)
    };

    void Start()
    {
        Cursor.visible = false;
        reticles = GameObject.FindGameObjectsWithTag("MachineGunReticle");
        int index = 0;
        foreach (GameObject reticle in reticles)
        {
            if (index >= 4) index = 0;
            reticle.transform.Rotate(reticleRot[index]);
            index++;
        }
    }

    void Update()
    {
        float range = player.currentRandRange;

        Ray targetPos = Camera.main.ScreenPointToRay(Input.mousePosition);
        transform.position = (Vector2)targetPos.origin;
        int index = 0;
        foreach (GameObject reticle in reticles)
        {
            if (index >= 4) index = 0;
            reticle.transform.position = transform.position + reticlePos[index] + (reticleDir[index] * range);
            index++;
        }
    }
}
