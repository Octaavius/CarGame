using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public AudioClip[] audios;

    private int currentTrackId = 0;
    private AudioSource audio;

    void Awake(){ 
        if(GameObject.FindGameObjectWithTag("musicManager") != gameObject){
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        currentTrackId = 0;
        if(audios.Length > 0){
            audio.clip = audios[currentTrackId];
            audio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!audio.isPlaying)
        {
            if(++currentTrackId == audios.Length){
                currentTrackId = 0;
            }
            audio.clip = audios[currentTrackId];
            audio.Play();
        }
    }
}
