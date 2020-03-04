using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundManager : MonoBehaviour
{
    [Tooltip("Sound names must correspond to enum names!")]
    public List<AudioClip> Sounds;

    public void PlaySound(SoundsNames soundName, bool is3Dsound)
    {
        var audioSource = GetAudioSource(soundName);
        if (audioSource.clip != null)
        {
            audioSource.spatialBlend = (is3Dsound) ? 1f : 0f; // Check if we need sound distance
            audioSource.Play();
            StartCoroutine(WaitAndDestroy(audioSource));
        }      
        else
        {
            Debug.LogError("Sound doesnt exist!");
        }
    }

    private AudioSource GetAudioSource(SoundsNames soundName)
    {
        // Create new audio source per sound
        var audioSource = gameObject.AddComponent<AudioSource>();
        var sound = Sounds.FirstOrDefault(x => x.name == soundName.ToString());
        audioSource.clip = sound;
        return audioSource;
    }


    IEnumerator WaitAndDestroy(AudioSource source)
    {       
        yield return new WaitUntil(() => !source.isPlaying);
        Destroy(source);
    }


    public enum SoundsNames // Make sure to add an enum for every sound in the sounds list.
    {
        GunShot_1
    }
}
