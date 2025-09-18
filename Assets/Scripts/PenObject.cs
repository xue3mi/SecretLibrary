using UnityEngine;

public class PenObject : MonoBehaviour
{
    private Vector3 originalPos;
    public static bool isHoldingPen = false;
    public Camera mainCamera;

    void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        originalPos = transform.position;
    }

    void Update()
    {
        if (isHoldingPen)
        {

            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;

            // if click elsewhere, put down the pen
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                // if clicked on the pen itself, do nothing
                if (hit.collider != null && hit.collider.gameObject == gameObject) return;

                PutDownPen();
            }
        }
    }

    void OnMouseDown()
    {
        isHoldingPen = true;
    }

    void PutDownPen()
    {
        isHoldingPen = false;
        transform.position = originalPos;
    }
}
