using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource musicSource;
    [SerializeField] [Range(0f, 1f)] public float musicVolume;
    [SerializeField] [Range(0f, 1f)] public float effectVolume;
    [SerializeField] [Range(0f, 1f)] public float masterVolume;

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

    // Update is called once per frame
    void Update()
    {
        musicSource.volume = musicVolume * masterVolume;
    }
}
