using System;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputManager inputManager;

    [Header("Grid Settings")]
    [SerializeField] private Grid grid;
    [SerializeField] private LayerMask roadLayerMask;

    private GridData objectData;

    [Header("Database")]
    [SerializeField] private BuildingDatabasSO buildingDatabase;

    private int selectedObjectIndex = -1;

    [Header("Materials")]
    [SerializeField] private Material validPlacementMaterial;
    [SerializeField] private Material invalidPlacementMaterial;

    private GameObject previewObject;
    private int selectedApartmentVariant = -1;
    private int selectedHouseVariant = -1;

    public static event Action onBuildingPlaced, onEscPressed, onPowerPlantPlaced;

    private void Start()
    {
        StopPlacement();

        objectData = new();

        RegisterRoads();
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

        GetPreviewBuildingVariant();

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

        GetPlacedBuildingVariant(newObject);

        objectData.AddObjectAt(gridPos, buildingDatabase.buildingData[selectedObjectIndex].Size,
            buildingDatabase.buildingData[selectedObjectIndex].ID, selectedObjectIndex);

        ActivateBuildingTraits(selectedObjectIndex, newObject);
        onBuildingPlaced?.Invoke();

        Destroy(previewObject);
        StopPlacement();
    }

    private void GetPreviewBuildingVariant()
    {
        var apartmentRandomiser = previewObject.GetComponent<GetRandomApartment>();
        if (apartmentRandomiser != null)
        {
            selectedApartmentVariant = apartmentRandomiser.GetRandomVariant();
            apartmentRandomiser.SetVariant(selectedApartmentVariant);
        }

        var houseRandomiser = previewObject.GetComponent<GetRandomHouse>();
        if (houseRandomiser != null)
        {
            selectedHouseVariant = houseRandomiser.GetRandomVariant();
            houseRandomiser.SetVariant(selectedHouseVariant);
        }
    }

    private void GetPlacedBuildingVariant(GameObject newObject)
    {
        // Randomise the building variant if the preview building is an apartment

        var apartmentRandomiser = newObject.GetComponent<GetRandomApartment>();
        if (apartmentRandomiser != null && selectedApartmentVariant >= 0)
        {
            apartmentRandomiser.SetVariant(selectedApartmentVariant);
        }

        // Randomise the building variant if the preview building is an apartment

        var houseRandomiser = newObject.GetComponent<GetRandomHouse>();
        if (houseRandomiser != null && selectedHouseVariant >= 0)
        {
            houseRandomiser.SetVariant(selectedHouseVariant);
        }
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
            // Power Plant

            case 0:
                var powerGen = newObject.GetComponent<GeneratePower>();

                if (powerGen != null)
                {
                    powerGen.enabled = true;
                }

                ResourceManager.Instance.RemovePower(buildingDatabase.buildingData[selectedObjectIndex].PowerCost);

                onPowerPlantPlaced?.Invoke();
                break;

            // Shop

            case 1:
                var shop = newObject.GetComponent<OnShopPlacement>();

                if (shop != null)
                {
                    shop.enabled = true;
                }

                ResourceManager.Instance.RemovePower(buildingDatabase.buildingData[selectedObjectIndex].PowerCost);
                break;

            // Apartment

            case 2: 
                var addApartmentPopulation = newObject.GetComponent<OnApartmentPlacement>();

                if (addApartmentPopulation != null)
                {
                    addApartmentPopulation.enabled = true;
                }

                ResourceManager.Instance.RemovePower(buildingDatabase.buildingData[selectedObjectIndex].PowerCost);
                break;

            // Park

            case 3:
                var addMood = newObject.GetComponent<OnParkPlacement>();

                if (addMood != null)
                {
                    addMood.enabled = true;
                }

                ResourceManager.Instance.RemovePower(buildingDatabase.buildingData[selectedObjectIndex].PowerCost);
                break;

            // House

            case 4: 
                var addHousePopulation = newObject.GetComponent<OnHousePlacement>();

                if (addHousePopulation != null)
                {
                    addHousePopulation.enabled = true;
                }

                ResourceManager.Instance.RemovePower(buildingDatabase.buildingData[selectedObjectIndex].PowerCost);
                break;
        }
    }

    private void RegisterRoads()
    {
        GameObject[] roadObjects = GameObject.FindGameObjectsWithTag("Road");

        foreach (var road in roadObjects)
        {
            Vector3Int gridPos = grid.WorldToCell(road.transform.position);

            objectData.AddObjectAt(gridPos, new Vector2Int(2, 2), -1, -1);
        }
    }
}