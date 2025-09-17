using UnityEngine;
using System.Collections.Generic;

public class DoodleOnObject2D : MonoBehaviour
{
    public GameObject linePrefab;
    public Collider2D drawArea;
    private LineRenderer currentLine;
    private List<Vector3> points = new List<Vector3>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            // mouse on area
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null && hit.collider == drawArea)
            {
                StartLine();
                AddPoint(mousePos);
            }
        }

        if (Input.GetMouseButton(0) && currentLine != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            // can only draw in area
            if (drawArea.OverlapPoint(mousePos))
            {
                if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], mousePos) > 0.05f)
                {
                    AddPoint(mousePos);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            EndLine();
        }
    }

    void StartLine()
    {
        GameObject newLine = Instantiate(linePrefab);

        //let line become child of drawArea
        newLine.transform.SetParent(drawArea.transform);

        currentLine = newLine.GetComponent<LineRenderer>();
        points.Clear();
    }

    void AddPoint(Vector3 point)
    {
        points.Add(point);
        currentLine.positionCount = points.Count;
        currentLine.SetPositions(points.ToArray());
    }

    void EndLine()
    {
        currentLine = null;
    }
}
