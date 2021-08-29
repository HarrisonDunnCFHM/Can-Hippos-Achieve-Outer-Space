using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource musicSource;
    [SerializeField] [Range(0f, 1f)] public float musicVolume;
    [SerializeField] [Range(0f, 1f)] public float effectVolume;
    [SerializeField] [Range(0f, 1f)] public float masterVolume;
    [SerializeField] Slider masterVol;
    [SerializeField] Slider effectVol;
    [SerializeField] Slider musicVol;

    PopOutMenu correctMenu;

    private void Awake()
    {
        int numberOfManagers = FindObjectsOfType<IncrementingData>().Length;
        if (numberOfManagers > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void ResetSliders()
    {
        var menus = FindObjectsOfType<PopOutMenu>();
        foreach (PopOutMenu menu in menus)
        {
            if (menu.name == "Options Menu") { correctMenu = menu; }
        }
        var sliders = correctMenu.GetComponentsInChildren<Slider>();
        foreach (Slider slider in sliders)
        {
            if (slider.name == "Master Volume") { masterVol = slider; }
            if (slider.name == "SFX Volume") { effectVol = slider; }
            if (slider.name == "Music Volume") { musicVol = slider; }
        }
        masterVol.value = masterVolume;
        effectVol.value = effectVolume;
        musicVol.value = musicVolume;
    }



    // Update is called once per frame
    void Update()
    {
        musicSource.volume = musicVolume * masterVolume * .5f;
        UpdateSliders();
    }

    private void UpdateSliders()
    {
        masterVolume = masterVol.value;
        effectVolume = effectVol.value;
        musicVolume = musicVol.value;
    }
}
