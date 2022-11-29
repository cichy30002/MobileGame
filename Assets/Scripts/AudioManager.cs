using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [HideInInspector] public static AudioManager instance;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] musicPlaylist;

    private void Awake()
    {
        if(instance != null)Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            StartCoroutine(PlayMusic());
        }
    }

    private void Update()
    {
        transform.position = Camera.main.transform.position;
    }

    private IEnumerator PlayMusic()
    {
        int i = 0;
        while (true)
        {
            source.clip = musicPlaylist[i++%musicPlaylist.Length];
            source.Play();
            yield return new WaitForSeconds(source.clip.length);
        }
    }
    
}
