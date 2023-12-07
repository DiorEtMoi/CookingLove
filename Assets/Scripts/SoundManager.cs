using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    private AudioMusicRef soundRef;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SasimiHolder.OnClickToHolder += SasimiHolder_OnClickToHolder;
        SasimiPlates.OnTrashFood += SasimiPlates_OnTrashFood;
        SasimiMachine.OnServerToPlates += SasimiMachine_OnServerToPlates;
    }

  

    private void SasimiMachine_OnServerToPlates(object sender, System.EventArgs e)
    {
        PlaySound(soundRef.serveFoodAudio,transform.position, 1f);

    }

    private void SasimiPlates_OnTrashFood(object sender, System.EventArgs e)
    {
        PlaySound(soundRef.trashSound,transform.position, 1f);
    }

    private void SasimiHolder_OnClickToHolder(object sender, System.EventArgs e)
    {
        SasimiHolder sashimi = sender as SasimiHolder;
        PlaySound(soundRef.sashimiAudio, sashimi.transform.position, 1f);
    }

    public void PlaySound(AudioClip clips, Vector3 position, float volume = .5f)
    {
        AudioSource.PlayClipAtPoint(clips, position, volume);
    }
}
