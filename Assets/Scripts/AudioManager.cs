using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private Sound[] sounds;
    public GameObject mainMusic;

    private AudioSource audioSource;

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

    public void SetMainVolume(float volume) => mainMusic.GetComponent<AudioSource>().volume = volume;
    public void SetEffectsVolume(float volume)
    {
        foreach (Sound sound in sounds)
            sound.volume = volume;

        FindObjectOfType<AudioManager>().PlaySound("Death1");
    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip audioClip;

    [Range(0f, 1f)]
    public float volume = 1f;

    [HideInInspector]
    public AudioSource audioSource;
}