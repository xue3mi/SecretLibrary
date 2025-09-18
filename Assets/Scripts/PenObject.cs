using UnityEngine;

public class PenObject : MonoBehaviour
{
    private Vector3 originalPos;
    public static int isHoldingPen = 0;
    public Camera mainCamera;
    public Collider2D cd;

    void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        originalPos = transform.position;
        if (cd == null) cd = GetComponent<Collider2D>();
        isHoldingPen = 0;
    }

    void Update()
    {
        if (isHoldingPen>1)
        {

            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;

            // if click elsewhere, put down the pen
            if (Input.GetMouseButtonDown(0)&&isHoldingPen>2)
            {
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                // if clicked on the pen itself, do nothing
                if (hit.collider != null && hit.collider.gameObject == gameObject) return;
                if (hit.collider==null||!hit.collider.CompareTag("Page"))
                    PutDownPen();
            }
            else if (isHoldingPen < 0xa)
            {  //magic number
                ++isHoldingPen;
            }
        }
    }

    void OnMouseDown()
    {
        if ((isHoldingPen)>0)
        {
            return;
        }
        isHoldingPen = 2;
        cd.enabled = false;
    }

    void PutDownPen()
    {
        isHoldingPen = 0;
        transform.position = originalPos;
        cd.enabled = true;
    }
}
