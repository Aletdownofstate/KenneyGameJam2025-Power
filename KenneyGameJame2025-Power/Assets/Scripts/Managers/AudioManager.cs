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
    [SerializeField] private AudioClip popSfx;

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
        PlacementSystem.onPowerPlantPlaced += OnPowerPlantPlaced;

        GeneratePower.onPowerCollect += OnPowerCollected;
    }

    private void OnDisable()
    {
        PlacementSystem.onBuildingPlaced -= OnBuildingPlaced;
        PlacementSystem.onPowerPlantPlaced -= OnPowerPlantPlaced;

        GeneratePower.onPowerCollect -= OnPowerCollected;
    }

    private void OnBuildingPlaced()
    {        
        PlaySfx(thudSfx);
    }

    private void OnPowerPlantPlaced()
    {
        PlaySfx(zapSfx);
    }

    private void OnPowerCollected()
    {
        PlaySfx(popSfx);
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxAudioSrc.PlayOneShot(clip);
    }
}
