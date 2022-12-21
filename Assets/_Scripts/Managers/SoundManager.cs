using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip[] mergeClips;
    

    void Awake() 
    {
       audioSource = GetComponent<AudioSource>();
    }

    public void PlayMergeSound(int evolutionIndex)
    {
        audioSource.clip = mergeClips[evolutionIndex];
        audioSource.Play();
    }
}
