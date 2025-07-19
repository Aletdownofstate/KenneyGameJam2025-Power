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
    [SerializeField] private AudioClip birdsSfx;
    [SerializeField] private AudioClip cashSfx;
    [SerializeField] private AudioClip popUpSfx;

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

        PopUpManager.onPopUp += PopUpOpened;
    }

    private void OnDisable()
    {
        PlacementSystem.onBuildingPlaced -= OnBuildingPlaced;
        PlacementSystem.onPowerPlantPlaced -= OnPowerPlantPlaced;

        GeneratePower.onPowerCollect -= OnPowerCollected;

        OnParkPlacement.onParkPlaced -= OnParkPlaced;

        OnShopPlacement.onShopPlaced -= OnShopPlaced;

        PopUpManager.onPopUp -= PopUpOpened;
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

    private void PopUpOpened()
    {
        PlaySfx(popUpSfx);
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxAudioSrc.PlayOneShot(clip);
    }
}
