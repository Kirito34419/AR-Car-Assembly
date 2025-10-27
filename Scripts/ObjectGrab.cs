using UnityEngine;

public class ObjectGrab : MonoBehaviour
{
    private Camera cam;
    private Transform grabbedObject = null;
    private Rigidbody grabbedRigidbody = null;
    private Vector3 grabOffset;
    public float moveSpeed = 10f;
    public float grabDistance = 5f; // distance from camera

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // On Left Click — try to grab
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider != null && hit.collider.CompareTag("Movable"))
                {
                    grabbedObject = hit.collider.transform;
                    grabbedRigidbody = grabbedObject.GetComponent<Rigidbody>();

                    if (grabbedRigidbody != null)
                        grabbedRigidbody.isKinematic = true; // stop physics

                    grabOffset = grabbedObject.position - hit.point;
                }
            }
        }

        // While holding click — move object with mouse
        if (Input.GetMouseButton(0) && grabbedObject != null)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Vector3 targetPos = ray.origin + ray.direction * grabDistance;
            grabbedObject.position = Vector3.Lerp(
                grabbedObject.position,
                targetPos + grabOffset,
                Time.deltaTime * moveSpeed
            );
        }

        // On release — drop it
        if (Input.GetMouseButtonUp(0) && grabbedObject != null)
        {
            if (grabbedRigidbody != null)
                grabbedRigidbody.isKinematic = false; // re-enable physics

            grabbedObject = null;
            grabbedRigidbody = null;
        }
    }
}
