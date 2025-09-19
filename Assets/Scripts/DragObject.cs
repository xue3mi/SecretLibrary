using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private bool hasSpawnedNext = false;

    [Header("SnapParent")]
    public Transform snapParent;
    public float snapDistance = 1f;
    private Transform[] snapTargets;

    [Header("Sprites")]
    public Sprite sprite1;
    public Sprite sprite2;
    private SpriteRenderer spriteRenderer;
    private bool hasSwitched = false;

    [HideInInspector] public PageSpawner spawner;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && sprite1 != null)
            spriteRenderer.sprite = sprite1;

        if (snapParent != null)
        {
            int childCount = snapParent.childCount;
            snapTargets = new Transform[childCount];
            for (int i = 0; i < childCount; i++)
                snapTargets[i] = snapParent.GetChild(i);
        }
    }

    void OnMouseDown()
    {
        if (PenObject.isHoldingPen > 0) return;

        // click to switch sprite
        if (!hasSwitched && spriteRenderer != null && sprite2 != null)
        {
            spriteRenderer.sprite = sprite2;
            hasSwitched = true;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        offset = transform.position - mousePos;
        isDragging = true;

        //drag then to spawn next
        if (!hasSpawnedNext && spawner != null)
        {
            spawner.PageRemoved();
            hasSpawnedNext = true;
        }
    }

    void OnMouseDrag()
    {
        if (isDragging && PenObject.isHoldingPen == 0)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (PenObject.isHoldingPen == 0 && snapTargets != null && snapTargets.Length > 0)
        {
            Transform nearestTarget = null;
            float nearestDistance = Mathf.Infinity;

            foreach (Transform target in snapTargets)
            {
                float distance = Vector2.Distance(transform.position, target.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestTarget = target;
                }
            }

            if (nearestTarget != null && nearestDistance <= snapDistance)
                transform.position = nearestTarget.position;
        }
    }
}
