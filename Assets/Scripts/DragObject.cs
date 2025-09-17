using UnityEngine;

public class DragSnapParent2D : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;

    [Header("SnapParent")]
    public Transform snapParent;
    public float snapDistance = 1f;
    private Transform[] snapTargets;

    void Start()
    {
        if (snapParent != null)
        {
            // get objects under snap parent ground
            int childCount = snapParent.childCount;
            snapTargets = new Transform[childCount];
            for (int i = 0; i < childCount; i++)
            {
                snapTargets[i] = snapParent.GetChild(i);
            }
        }
    }

    void OnMouseDown()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        offset = transform.position - mousePos;
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (snapTargets != null && snapTargets.Length > 0)
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
            {
                transform.position = nearestTarget.position;
            }
        }
    }
}
