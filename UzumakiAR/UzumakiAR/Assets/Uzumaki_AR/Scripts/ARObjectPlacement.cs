using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARObjectPlacement : MonoBehaviour
{
    public GameObject objectPrefab;
    public ARRaycastManager raycastManager;

    private GameObject spawnedObject;

    private float minScale = 0.3f;
    private float maxScale = 1.5f;

    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        // ---------------- TAP TO PLACE ----------------
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose pose = hits[0].pose;

                    if (spawnedObject != null)
                        Destroy(spawnedObject);

                    spawnedObject = Instantiate(objectPrefab, pose.position, pose.rotation);
                    spawnedObject.transform.localScale = Vector3.one * 0.5f;
                }
            }

            // ---------------- SINGLE FINGER DRAG ----------------
            if (touch.phase == TouchPhase.Moved && spawnedObject != null)
            {
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose pose = hits[0].pose;
                    spawnedObject.transform.position = pose.position;
                }
            }
        }

        // ---------------- TWO FINGER GESTURE ----------------
        if (Input.touchCount == 2 && spawnedObject != null)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            // ---------- SCALE ----------
            Vector2 prevTouch0 = touch0.position - touch0.deltaPosition;
            Vector2 prevTouch1 = touch1.position - touch1.deltaPosition;

            float prevDistance = Vector2.Distance(prevTouch0, prevTouch1);
            float currentDistance = Vector2.Distance(touch0.position, touch1.position);

            float scaleFactor = (currentDistance - prevDistance) * 0.001f;

            Vector3 newScale = spawnedObject.transform.localScale + Vector3.one * scaleFactor;

            newScale.x = Mathf.Clamp(newScale.x, minScale, maxScale);
            newScale.y = Mathf.Clamp(newScale.y, minScale, maxScale);
            newScale.z = Mathf.Clamp(newScale.z, minScale, maxScale);

            spawnedObject.transform.localScale = newScale;

            // ---------- ROTATE ----------
            Vector2 touchDelta = touch0.deltaPosition;
            spawnedObject.transform.Rotate(Vector3.up, -touchDelta.x * 0.2f);
        }
    }
}