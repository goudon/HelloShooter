using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderChangeSoundValue : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource[] targetAudioSources;
    void Start()
    {
        foreach (AudioSource targetAudioSource in targetAudioSources)
        {
            targetAudioSource.volume = gameObject.GetComponent<Slider>().value;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void changeValue(float value)
    {
        foreach (AudioSource targetAudioSource in targetAudioSources)
        {
            targetAudioSource.volume = value;
        }
    }
}
