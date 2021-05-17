using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> reloadList;

    public List<AudioClip> fireList;

    private AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    public void playFireSound(int index)
    {
        playSound(fireList[index]);
    }

    public void playReloadSound(int index)
    {
        playSound(reloadList[index]);
    }

    private void playSound(AudioClip clip)
    {
        if (_audioSource.isPlaying)
        {
            AudioSource newChannel = gameObject.AddComponent<AudioSource>();
            newChannel.clip = clip;
            newChannel.Play();
        } else
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}
