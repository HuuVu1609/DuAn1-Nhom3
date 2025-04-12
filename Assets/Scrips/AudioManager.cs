using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource audioBackGround;
    public AudioSource audioClip;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip backGroundAudio;
    [SerializeField] private AudioClip backGroundResultAudio;
    [SerializeField] private AudioClip jumpAudio;
    [SerializeField] private AudioClip carrotAudio;
    [SerializeField] private AudioClip dieAudio;
    [SerializeField] private AudioClip GoldcarrotAudio;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        PlayAudio(audioBackGround, backGroundAudio);
    }

    public void PlayAudio(AudioSource source, AudioClip clip)
    {
        if (source != null && clip != null)
        {
            source.clip = clip;
            source.Play();
        }
    }
    public void PlayResultBackgroundAudio()
    {
        if (audioBackGround != null && backGroundResultAudio != null)
        {
            audioBackGround.clip = backGroundResultAudio;
            audioBackGround.Play();
        }
    }

    public void PlayOneShotAudio(AudioSource source, AudioClip clip)
    {
        if (source != null && clip != null)
        {
            source.PlayOneShot(clip);
        }
    }

    public void ToggleAudio(AudioSource source, bool isOn)
    {
        if (source != null)
        {
            source.mute = !isOn;
        }
    }

    public void PlayJumpAudio()
    {
        PlayOneShotAudio(audioClip, jumpAudio);
    }

    public void PlayCarrotAudio()
    {
        PlayOneShotAudio(audioClip, carrotAudio);
    }
    public void PlayGoldCarrotAudio()
    {
        PlayOneShotAudio(audioClip, GoldcarrotAudio);
    }
    public void PlayDieAudio()
    {
        PlayOneShotAudio(audioClip, dieAudio);
    }
}