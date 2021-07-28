using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Sound[] sounds;
    private AudioSource audioSource;

    public static AudioManager instance;

    private void Awake()
    {
        if (!instance)
            instance = this;
        
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();

            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.volume = sound.volume;
        }

        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlaySound(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        audioSource.clip = s.audioClip;
        audioSource.volume = s.volume;

        if (s == null)
            return;
        audioSource.Play();
    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip audioClip;

    [Range(0f, 1f)]
    public float volume = 1;

    [HideInInspector]
    public AudioSource audioSource;
}