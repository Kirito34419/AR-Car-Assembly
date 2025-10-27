using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SnapZoneDistance : MonoBehaviour
{
    [Header("Snap Settings")]
    public Transform targetTransform;
    public Renderer fixedRenderer;
    public Renderer originalRenderer;
    public float snapDistance = 0.3f;
    public float ghostAlpha = 0.4f;
    public float fadeSpeed = 10f;

    [Header("Next Part Activation (Optional)")]
    public GameObject nextFixedPart;   // The next ghost part to show
    public GameObject nextMovablePart; // The next movable part to allow pickup

    [Header("Rotation Control")]
    public float rotationSpeed = 60f; // degrees per second
    public KeyCode rotateModifier = KeyCode.LeftShift; // hold to rotate faster
    private float rotationOffset = 0f;

    private bool isSnapped = false;
    private Material[] originalFixedMaterials; // store transparent ghost mats
    private Color initialFixedColor;

    void Start()
    {
        if (fixedRenderer == null && targetTransform != null)
            fixedRenderer = targetTransform.GetComponent<Renderer>();

        if (fixedRenderer != null)
            originalFixedMaterials = fixedRenderer.materials;

        // start visible but partially transparent
        SetFixedAlpha(ghostAlpha);
    }

    void HandleRotationInput()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0)
        {
            float speed = rotationSpeed * (Input.GetKey(rotateModifier) ? 3f : 1f);
            rotationOffset += scroll * speed * Time.deltaTime;
            transform.Rotate(Vector3.up, scroll * speed * Time.deltaTime, Space.World);
        }
    }

    void Update()
    {
        if (isSnapped || targetTransform == null) return;

        float dist = Vector3.Distance(transform.position, targetTransform.position);

        // Fade the ghost visibility
        if (dist < snapDistance * 3f)
        {
            float currentA = GetFixedAlpha();
            float newA = Mathf.Lerp(currentA, ghostAlpha, Time.deltaTime * fadeSpeed);
            SetFixedAlpha(newA);
        }
        else
        {
            float currentA = GetFixedAlpha();
            float newA = Mathf.Lerp(currentA, 0f, Time.deltaTime * fadeSpeed);
            SetFixedAlpha(newA);
        }

        if (dist <= snapDistance)
            SnapIntoPlace();
    }

    void SnapIntoPlace()
    {
        transform.position = targetTransform.position;
        transform.rotation = targetTransform.rotation;

        gameObject.SetActive(false); // hide movable part

        // Make fixed part fully visible
        if (fixedRenderer != null && originalRenderer != null)
        {
            fixedRenderer.materials = CloneMaterials(originalRenderer.materials);
        }

        // Activate next part(s)
        if (nextFixedPart != null)
            nextFixedPart.SetActive(true);

        if (nextMovablePart != null)
            nextMovablePart.SetActive(true);

        isSnapped = true;
        // After setting isSnapped = true;
        // AssemblyManager.Instance.PartSnapped();
    }

    void SetFixedAlpha(float a)
    {
        if (fixedRenderer == null) return;
        if (!fixedRenderer.material.HasProperty("_Color")) return;
        Color c = fixedRenderer.material.color;
        c.a = Mathf.Clamp01(a);
        fixedRenderer.material.color = c;
    }

    float GetFixedAlpha()
    {
        if (fixedRenderer == null) return 0f;
        if (!fixedRenderer.material.HasProperty("_Color")) return 0f;
        return fixedRenderer.material.color.a;
    }

    // Safely clone material instances (so we don’t overwrite shared ones)
    Material[] CloneMaterials(Material[] sourceMats)
    {
        Material[] newMats = new Material[sourceMats.Length];
        for (int i = 0; i < sourceMats.Length; i++)
        {
            newMats[i] = new Material(sourceMats[i]);
        }
        return newMats;
    }
    public bool IsSnapped()
    {
        return isSnapped;
    }
}
