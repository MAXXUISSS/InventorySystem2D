using UnityEngine;
using UnityEngine.UI;

public class SoundEffectManager : MonoBehaviour
{
    private static SoundEffectManager Instance;
    private AudioSource audioSource;
    private AudioSource randomPitchAudioSource;
    private AudioSource voiceAudioSource;
    private SoundEffectLibrary soundEffectLibrary;
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            AudioSource[] audioSources = GetComponents<AudioSource>();
            audioSource = audioSources[0];
            randomPitchAudioSource = audioSources[1];
            voiceAudioSource = audioSources[2];
            soundEffectLibrary = GetComponent<SoundEffectLibrary>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void Play(string soundName, bool randomPitch = false)
    {
        AudioClip audioClip = Instance.soundEffectLibrary.GetRandomClip(soundName);

        if (audioClip != null)
        {
            if (randomPitch)
            {
                
                Instance.audioSource.pitch = Random.Range(1f, 1.5f);
                Instance.audioSource.PlayOneShot(audioClip);

            }
            else
            {
                Instance.audioSource.PlayOneShot(audioClip);
            }
        }
    }

    public static void PlayVoice(AudioClip audioClip, float pitch = 1f)
    {
        Instance.voiceAudioSource.pitch = pitch;
        Instance.voiceAudioSource.PlayOneShot(audioClip);

    }


    private void Start()
    {
        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.AddListener(delegate { OnValuedChanged(); });
        }
    }

    public static void SetVolume(float volume)
    {
        Instance.audioSource.volume = volume;
        Instance.randomPitchAudioSource.volume = volume;
        Instance.voiceAudioSource.volume = volume;
    }

    private void OnValuedChanged()
    {
        SetVolume(sfxSlider.value);
    }
}
