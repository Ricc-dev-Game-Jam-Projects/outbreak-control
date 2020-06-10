using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicBox : MonoBehaviour
{
    public TextMeshProUGUI NP;

    public GameObject MusicBtnPrefab;
    public Transform MusicBtnContainer;
    public Slider VolumeSlider;

    public List<GameObject> MusicButtons;

    private MusicManager tocador;
    
    void Start()
    {
        tocador = MusicManager.instance;

        int conta = 0;
        if (tocador == null) return;

        foreach(AudioClip clip in tocador.Musics)
        {
            GameObject musicObj = Instantiate(MusicBtnPrefab, MusicBtnContainer, false);
            MusicButtons.Add(musicObj);

            musicObj.GetComponent<MusicSelectBtn>().SetMusic(conta, clip.name);
            conta++;
        }

        VolumeSlider.onValueChanged.AddListener((float value) =>
        {
            PlayerPrefs.SetFloat(MusicManager.VolumeKey, value);
        });

        if (PlayerPrefs.HasKey(MusicManager.VolumeKey))
        {
            VolumeSlider.value = PlayerPrefs.GetFloat(MusicManager.VolumeKey);
        }
    }

    void Update()
    {
        // Gerencia o que está tocando no momento
        if (tocador != null && tocador.Source.isPlaying)
        {
            NP.text = tocador.Source.clip.name;
        }
    }
}
