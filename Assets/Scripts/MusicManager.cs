using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource music;
    public AudioClip tem;
    // Start is called before the first frame update
    void Start()
    {
        music = gameObject.GetComponent<AudioSource>();
        tem = Resources.Load<AudioClip>("music/spring");
        music.clip = tem; //播放音效
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayNext()//切换音效
    {
        music.clip = tem;
        music.Play();
    }
}
