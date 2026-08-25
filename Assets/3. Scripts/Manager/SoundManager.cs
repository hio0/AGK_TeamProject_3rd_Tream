using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioData
{
    public AudioSource sound;
    public AudioSource bgm;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] AudioSource sound;
    [SerializeField] AudioSource bgm;

    public Func<AudioData> GetSoundData;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            GetSoundData += ReturnData;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    AudioData ReturnData()
    {
        AudioData data = new AudioData
        {
            sound = sound,
            bgm = bgm
        };

        return data;
    }

    public void BGMPlay(AudioClip audio)
    {
        bgm.clip = audio;
        bgm.Play();
    }

    public void Volumed(AudioSource audioSource, float volume)
    {
        audioSource.volume = volume;
    }

    public void Looped(AudioSource audioSource, bool looped)
    {
        audioSource.loop = looped;
    }

    public void SoundEffectPlay(AudioClip audio)
    {
        sound.PlayOneShot(audio);
    }

    public void ShutUp()
    {
        sound.Stop();
        bgm.Stop();
    }

    public void FadeSound(AudioSource source, float val, float duration)
    {
        IEnumerator FadeOut(AudioSource audioSource, float duration)
        {
            float startVolume = audioSource.volume;

            while (audioSource.volume > val)
            {
                audioSource.volume -= startVolume * Time.deltaTime / duration;
                yield return null;
            }

            audioSource.volume = val;
        }
        StartCoroutine(FadeOut(source, duration));
    }
}
