using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource Source;

    public List<AudioClip> Musics;
    
    public int ActualSong;
    public float Volume;

    public bool IsRandom;

    public static string VolumeKey = "Volume";

    void Awake()
    {
        #region SINGLETON
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
            return;
        }
        #endregion

        Source = GetComponent<AudioSource>();

        if(Source == null)
        {
            Source = (AudioSource) gameObject.AddComponent(typeof(AudioSource));
        }

        Source.clip = Musics[0];

        Source.volume = 0.4f;
    }

    private void Start()
    {
        Source.Play();
    }

    private void Update()
    {
        if(!Source.isPlaying)
        {
            PlayNextSong();
        }

        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            PlayNextSong();
        }

        if (PlayerPrefs.HasKey(VolumeKey))
        {
            float vol = PlayerPrefs.GetFloat(VolumeKey);
            Source.volume = vol;
        }
    }

    private void PlayNextSong()
    {
        if (!IsRandom)
        {
            ActualSong++;
            if(ActualSong >= Musics.Count)
            {
                ActualSong = 0;
            }
        } else
        {
            ActualSong = Random.Range(0, Musics.Count);
        }

        Source.clip = Musics[ActualSong];
        Source.Play();
    }

    public void Play(int index)
    {
        if (index >= Musics.Count) return;
        Source.clip = Musics[index];
        Source.Play();
    }
}
