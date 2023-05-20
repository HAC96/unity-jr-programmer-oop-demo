using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private AudioSource musicSource { get => GetComponent<AudioSource>(); }

    private void Update()
    {
        musicSource.volume = GameManager.Instance.musicVolume;
    }
}
