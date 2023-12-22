using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    
    public void PlayMusic(AudioClip clip)
    {
        if (_audioSource.clip == clip) return;
        
        _audioSource.clip = clip;
        _audioSource.loop = true;
        _audioSource.Play();
    }
}
