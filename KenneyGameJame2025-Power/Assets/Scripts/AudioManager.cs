using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxAudioSrc;
    [SerializeField] private AudioSource bgmAudioSrc;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip zapSfx;
    [SerializeField] private AudioClip thudSfx;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }        
    }

    private void Start()
    {
        bgmAudioSrc.Play();
    }

    private void OnEnable()
    {
        PlacementSystem.onBuildingPlaced += OnBuildingPlaced;
    }

    private void OnDisable()
    {
        PlacementSystem.onBuildingPlaced -= OnBuildingPlaced;
    }

    private void OnBuildingPlaced()
    {
        PlaySfx(zapSfx);
        PlaySfx(thudSfx);
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxAudioSrc.PlayOneShot(clip);
    }
}
