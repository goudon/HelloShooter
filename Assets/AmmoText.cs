using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoText : MonoBehaviour
{
    public Player player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = player.remainAmmo.ToString() + " / " + player.maxAmmoLevelList[player.maxAmmoLevel].ToString();
    }
}
