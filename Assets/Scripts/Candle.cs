using UnityEngine;

public class CandleToggle : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite sprite1; //off
    public Sprite sprite2; //lit

    private SpriteRenderer spriteRenderer;
    public bool isLit = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && sprite1 != null)
        {
            spriteRenderer.sprite = sprite1;
        }


        if (CandleManager.instance != null)
            CandleManager.instance.RegisterCandle(this);
    }

    void OnMouseDown()
    {
        //change sprite
        isLit = !isLit;
        if (spriteRenderer != null)
            spriteRenderer.sprite = isLit ? sprite2 : sprite1;


        if (CandleManager.instance != null)
            CandleManager.instance.CheckAllCandles();
    }
}
