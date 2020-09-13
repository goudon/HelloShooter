using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;
    public string targetStatusName;
    public Sprite offSprite, onSprite;
    public Text buttonText;
    public int[] cost = new int[5] { 1000, 2500, 5000, 7500, 10000 };
    public AudioSource upgrade,maxUpgrade;
    void Start()
    {
        buttonText.text = "Need Point : " + cost[0] + "pt";;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LevelUp()
    {
        // player.GetType().GetProperty().GetValue();
        var SpriteList = gameObject.GetComponentsInChildren<SpriteRenderer>();
        var propaty = typeof(Player).GetProperty(targetStatusName);
        int level = (int)propaty.GetValue(player);
        int nextLevel = level + 1;
        if (level >= cost.Length) return;
        if (player.point >= cost[level])
        {
            player.UsePoint(cost[level]);
            upgrade.PlayOneShot(upgrade.clip);

        }
        else
        {
            return;
        }

        propaty.SetValue(player, nextLevel);
        if (nextLevel >= cost.Length)
        {
            buttonText.text = "Max status !!";
            maxUpgrade.PlayOneShot(maxUpgrade.clip);
        }
        else
        {
            buttonText.text = "Need Point : " + cost[nextLevel] + "pt";
        }
        foreach (var sprite in SpriteList)
        {
            if (sprite.name == "Level" + nextLevel)
            {
                sprite.GetComponent<SpriteRenderer>().sprite = onSprite;
            }
        }
        player.GunRebuildOnEdit();
    }
}
