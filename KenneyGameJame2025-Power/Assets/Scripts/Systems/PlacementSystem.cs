using System;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputManager inputManager;

    [Header("Grid Settings")]
    [SerializeField] private Grid grid;

    private GridData objectData;

    [Header("Database")]
    [SerializeField] private BuildingDatabasSO buildingDatabase;

    private int selectedObjectIndex = -1;

    [Header("Materials")]
    [SerializeField] private Material validPlacementMaterial;
    [SerializeField] private Material invalidPlacementMaterial;

    private GameObject previewObject;
    private int selectedApartmentVariant = -1;

    public static event Action onBuildingPlaced, onEscPressed, onPowerPlantPlaced;

    private void Start()
    {
        StopPlacement();

        objectData = new();
    }

    private void Update()
    {
        if (previewObject == null && Input.GetKeyDown(KeyCode.Escape)) onEscPressed?.Invoke();

        if (selectedObjectIndex < 0 || previewObject == null) return;

        Vector3 mousePos = inputManager.GetMousePosOrthographic();
        Vector3Int gridPos = grid.WorldToCell(mousePos);
        Vector3 snappedWorldPos = grid.CellToWorld(gridPos);
        snappedWorldPos.y = inputManager.GetMousePosOrthographic().y;

        bool placementValidity = CheckPlacementValidity(gridPos, selectedObjectIndex);

        previewObject.transform.position = snappedWorldPos;

        SetPreviewMaterial(previewObject, placementValidity ? validPlacementMaterial : invalidPlacementMaterial);

        if (Input.GetKeyDown(KeyCode.E)) inputManager.RotateObjectClockwise(previewObject);

        if (Input.GetKeyDown(KeyCode.Q)) inputManager.RotateObjectAntiClockwise(previewObject);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;

        if (previewObject != null) Destroy(previewObject);

        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
    }

    public void StartPlacement(int index)
    {
        selectedObjectIndex = buildingDatabase.buildingData.FindIndex(data => data.ID == index);
        
        if (selectedObjectIndex < 0) return;

        if (buildingDatabase.buildingData[selectedObjectIndex].PowerCost > ResourceManager.Instance.availablePower)
        { 
            StopPlacement();
            return; 
        }

        if (buildingDatabase.buildingData[selectedObjectIndex].Name == "Power Plant" 
            && ResourceManager.Instance.currentPowerPlants == ResourceManager.Instance.maximumPowerPlants) return;

        previewObject = Instantiate(buildingDatabase.buildingData[selectedObjectIndex].Prefab);

        // Randomise the building variant if the preview building is an apartment

        var randomiser = previewObject.GetComponent<GetRandomApartment>();
        if (randomiser != null)
        {
            selectedApartmentVariant = randomiser.GetRandomVariant();
            randomiser.SetVariant(selectedApartmentVariant);
        }

        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (inputManager.IsPointerOverUI()) return;

        Vector3 mousePos = inputManager.GetMousePosOrthographic();
        Vector3Int gridPos = grid.WorldToCell(mousePos);

        if (!CheckPlacementValidity(gridPos, selectedObjectIndex)) return;

        GameObject newObject = Instantiate(buildingDatabase.buildingData[selectedObjectIndex].Prefab);
        Vector3 placePosition = grid.CellToWorld(gridPos);
        placePosition.y = inputManager.GetMousePosOrthographic().y;

        newObject.transform.position = placePosition;
        newObject.transform.rotation = previewObject.transform.rotation;

        // Apply randomised building variant if applicable

        var randomiser = newObject.GetComponent<GetRandomApartment>();
        if (randomiser != null && selectedApartmentVariant >= 0)
        {
            randomiser.SetVariant(selectedApartmentVariant);
        }

        objectData.AddObjectAt(gridPos, buildingDatabase.buildingData[selectedObjectIndex].Size,
            buildingDatabase.buildingData[selectedObjectIndex].ID, selectedObjectIndex);

        ActivateBuildingTraits(selectedObjectIndex, newObject);
        onBuildingPlaced?.Invoke();

        Destroy(previewObject);
        StopPlacement();
    }

    private bool CheckPlacementValidity(Vector3Int gridPos, int selectedObjectIndex)
    {
        return objectData.CanPlaceObjectAt(gridPos, buildingDatabase.buildingData[selectedObjectIndex].Size);
    }

    private void SetPreviewMaterial(GameObject obj, Material mat)
    {
        foreach (var renderer in obj.GetComponentsInChildren<Renderer>())
        {
            renderer.material = mat;
        }
    }

    private void ActivateBuildingTraits(int index, GameObject newObject)
    {
        switch (index)
        {
            case 0: // Power Plant
                var powerGen = newObject.GetComponent<GeneratePower>();
                if (powerGen != null)
                {
                    powerGen.enabled = true;
                }

                ResourceManager.Instance.RemovePower(buildingDatabase.buildingData[selectedObjectIndex].PowerCost);

                onPowerPlantPlaced?.Invoke();
                break;

            case 1: // Shop - nothing currently                
                break;

            case 2: // Apartment
                var addPopulation = newObject.GetComponent<OnApartmentPlacement>();
                if (addPopulation != null)
                {
                    addPopulation.enabled = true;
                }

                ResourceManager.Instance.RemovePower(buildingDatabase.buildingData[selectedObjectIndex].PowerCost);
                break;

            case 3: // Park
                var addMood = newObject.GetComponent<OnParkPlacement>();
                if (addMood != null)
                {
                    addMood.enabled = true;
                }

                ResourceManager.Instance.RemovePower(buildingDatabase.buildingData[selectedObjectIndex].PowerCost);
                break;

        }
    }
}