using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetHealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject image;
    [SerializeField]

    public GameObject parent;
    private float healthProp = 1.0f;
    private Target props;

    // Start is called before the first frame update1
    void Start()
    {
        props = parent.GetComponent<Target>();
        healthProp = (props.maxHealth != 0f) ? props.remainHealth / props.maxHealth : 0.0f;
        //image = GameObject.Find("Health");
    }

    // Update is called once per frame
    void Update()
    {
        healthProp = props.maxHealth != 0f ? props.remainHealth / props.maxHealth : 0.0f;
        gameObject.GetComponent<Image>().fillAmount = healthProp;
    }
}
