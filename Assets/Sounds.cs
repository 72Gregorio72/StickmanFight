using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource Music;
    public AudioSource SFX;

    [Header("Sounds")]
    public AudioSource walk;
    public AudioSource jump;

    public AudioSource died;

    public AudioSource sliding;

    public AudioSource keyPickUp;

    public AudioSource DoorOpening;

    public AudioSource laserCharging;
    public AudioSource laserShooting;

    public AudioSource boing;
    // Start is called before the first frame update
    void Start()
    {
        walk.volume = 0.5f;
        Music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayWalk()
    {
        if (!walk.isPlaying)
        {
            walk.Play();
        }
    }

    public void StopWalk()
    {
        walk.Stop();
    }

    public void PlaySlide()
    {
        if (!sliding.isPlaying)
        {
            sliding.Play();
        }
    }

    public void StopSlide()
    {
        sliding.Stop();
    }

    public void PlayLaserCharging()
    {
        if (!laserCharging.isPlaying)
        {
            laserCharging.Play();
        }
    }

    public void PlayBoing()
    {
        if (!boing.isPlaying)
        {
            boing.Play();
        }
    }

    public void StopLaserCharging()
    {
        laserCharging.Stop();
    }

    public void PlayLaserShooting()
    {
        laserShooting.Play();
    }

    public void PlayDie()
    {
        died.Play();
    }

    public void PlayKeyPickUp()
    {
        keyPickUp.Play();
    }

    public void PlayDoorOpening()
    {
        DoorOpening.Play();
    }

    public void PlayJump()
    {
        jump.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFX.clip = clip;
        SFX.Play();
    }

    public void StopSFX()
    {
        SFX.Stop();
    }

    public void PlayMusic(AudioClip clip)
    {
        Music.clip = clip;
        Music.Play();
    }

    public void StopMusic()
    {
        Music.Stop();
    }
}
