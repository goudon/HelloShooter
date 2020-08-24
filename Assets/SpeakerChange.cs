using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeakerChange : MonoBehaviour
{
    public int speakerStatus = 2;
    public int[] speakerStatusList = new int[4] { 0, 1, 2, 3 };
    public Sprite[] speakerImage = new Sprite[4];

    void Start()
    {
        GetComponent<Button>().image.sprite = speakerImage[speakerStatus];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeVolume()
    {
        speakerStatus--;
        if (speakerStatus < 0) speakerStatus = 3;
        GetComponent<Button>().image.sprite = speakerImage[speakerStatus];
    }
}
