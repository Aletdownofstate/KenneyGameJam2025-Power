using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Mixer")]
    [SerializeField] private AudioMixer mainMixer;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxAudioSrc;
    [SerializeField] private AudioSource bgmAudioSrc;
    [SerializeField] private AudioSource gameOverAudioSrc;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip zapSfx;
    [SerializeField] private AudioClip thudSfx;
    [SerializeField] private AudioClip popSfx;
    [SerializeField] private AudioClip birdsSfx;
    [SerializeField] private AudioClip cashSfx;
    [SerializeField] private AudioClip popUpSfx;
    [SerializeField] private AudioClip trashSfx;
    [SerializeField] private AudioClip booingSfx;

    private int cutOffFrequency = 200;

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

        OnParkPlacement.onParkPlaced += OnParkPlaced;
        OnShopPlacement.onShopPlaced += OnShopPlaced;
        OnLandfillPlacement.onLandfillPlaced += OnLandfillPlaced;

        PopUpManager.onPopUp += PopUpOpened;

        MoodManager.onGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        PlacementSystem.onBuildingPlaced -= OnBuildingPlaced;
        PlacementSystem.onPowerPlantPlaced -= OnPowerPlantPlaced;

        GeneratePower.onPowerCollect -= OnPowerCollected;

        OnParkPlacement.onParkPlaced -= OnParkPlaced;
        OnShopPlacement.onShopPlaced -= OnShopPlaced;
        OnLandfillPlacement.onLandfillPlaced -= OnLandfillPlaced;

        PopUpManager.onPopUp -= PopUpOpened;

        MoodManager.onGameOver -= OnGameOver;
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

    private void OnShopPlaced()
    {
        PlaySfx(cashSfx);
    }

    private void OnParkPlaced()
    {
        PlaySfx(birdsSfx);
    }

    private void OnLandfillPlaced()
    {
        PlaySfx(trashSfx);
    }

    private void PopUpOpened()
    {
        PlaySfx(popUpSfx);
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxAudioSrc.PlayOneShot(clip);
    }

    public void ApplyLowPassFilter()
    {
        mainMixer.SetFloat("musicLpf", cutOffFrequency);
    }

    public void BypassLowPassFilter()
    {
        mainMixer.SetFloat("musicLpf", 22000);
    }

    private void OnGameOver()
    {
        bgmAudioSrc.Stop();
        gameOverAudioSrc.Play();
        PlaySfx(booingSfx);
    }
}
