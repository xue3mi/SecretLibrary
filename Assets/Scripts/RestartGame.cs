using UnityEngine;

public class RestartGame : MonoBehaviour
{
    [Header("References")]
    public GameObject gameOver;
    public GameObject book;
    public PageSpawner pageSpawner;
    public ReturnBook returnBook;

    void OnMouseDown()
    {

        if (gameOver != null)
            gameOver.SetActive(false);

        if (book != null)
            book.SetActive(true);

        if (pageSpawner != null)
            pageSpawner.ResetSpawner();

        if (returnBook != null)
            returnBook.gameObject.SetActive(false);

        Debug.Log("Game restarted!");
    }
}
