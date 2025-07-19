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

    public Vector3 GetMousePosOrthographic()
    {
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out float enter))
        {
            lastPosition = ray.GetPoint(enter);
        }

        return lastPosition;
    }

    public void RotateObjectClockwise(GameObject obj)
    {
        obj.transform.Rotate(0f, 90f, 0f);
    }

    public void RotateObjectAntiClockwise(GameObject obj)
    {
        obj.transform.Rotate(0f, -90f, 0f);
    }
}
