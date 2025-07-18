using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask placementLayerMask;
    
    private Vector3 lastPosition;

    public event Action OnClicked, OnExit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) OnClicked?.Invoke();

        if (Input.GetKeyDown(KeyCode.Escape)) OnExit?.Invoke();
    }

    public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();

    public Vector3 GetSelectedMapPosition()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, placementLayerMask))
        {
            lastPosition = hit.point;
        }

        return lastPosition;
    }
}
