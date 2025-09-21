using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Tentacle : MonoBehaviour
{
    public Transform basePoint;
    public int pointCount = 5;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = pointCount;
    }

    void Update()
    {
        Vector3 tipPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tipPos.z = 0;

        Vector3[] points = new Vector3[pointCount];
        points[0] = basePoint.position;

        //middle points adjust curve
        for (int i = 1; i < pointCount - 1; i++)
        {
            float t = i / (float)(pointCount - 1);

            points[i] = Vector3.Lerp(basePoint.position, tipPos, t);
            points[i].y += Mathf.Sin(t * Mathf.PI) * 1f;
        }

        points[pointCount - 1] = tipPos;

        lineRenderer.SetPositions(points);
    }
}
