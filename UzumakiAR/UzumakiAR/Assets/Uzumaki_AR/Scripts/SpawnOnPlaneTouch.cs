using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class SpawnOnPlaneTouch : MonoBehaviour
{
    public GameObject spawnPrefab; // Assign in Inspector

    private ARRaycastManager raycastManager;
    private GameObject spawnedObject; // Only one object

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                // ✅ Only spawn once
                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(spawnPrefab, hitPose.position, hitPose.rotation);
                }
            }
        }
    }
}