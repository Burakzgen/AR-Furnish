using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class PlacementManager : MonoBehaviour
{
    [SerializeField] GameObject _spawnFurniture;
    [SerializeField] ARSessionOrigin _sessionOrigin;
    [SerializeField] ARRaycastManager _raycastManager;
    [SerializeField] ARPlaneManager _planeManager;

    private List<ARRaycastHit> raycasthit = new();

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                bool collision = _raycastManager.Raycast(Input.GetTouch(0).position, raycasthit, TrackableType.PlaneWithinPolygon);

                if (collision && IsButtonPressed())
                {
                    GameObject _object = Instantiate(_spawnFurniture);
                    _object.transform.SetPositionAndRotation(raycasthit[0].pose.position, raycasthit[0].pose.rotation);

                }
                foreach (var planes in _planeManager.trackables)
                {
                    planes.gameObject.SetActive(false);
                }
                _planeManager.enabled = false;
            }
        }
    }

    private bool IsButtonPressed()
    {
        if (EventSystem.current.currentSelectedGameObject?.GetComponent<Button>() == null)
            return false;

        return true;
    }
    public void SwitchFurniture(GameObject furnitureObject)
    {
        _spawnFurniture = furnitureObject;
    }
}
