using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TargetTimeBar : MonoBehaviour
{
    GameObject image;
    [SerializeField]

    public GameObject parent;

    private float timeProp = 1.0f;
    private Target props;

    // Start is called before the first frame update1
    void Start()
    {
        props = parent.GetComponent<Target>();
        timeProp = (props.maxSec != 0f) ? props.remainSec / props.maxSec : 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeProp = (props.maxSec != 0f) ? props.remainSec / props.maxSec : 0.0f;
        gameObject.GetComponent<Image>().fillAmount = timeProp;
        // Debug.Log( Input.mousePosition.x);
    }
}
