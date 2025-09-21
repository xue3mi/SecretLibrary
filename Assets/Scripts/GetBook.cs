using UnityEngine;

public class GetBook : MonoBehaviour
{
    [Header("Book Object")]
    public GameObject book;
    [Header("Page Spawner")]
    public PageSpawner pageSpawner;

    void OnMouseDown()
    {
        // show book
        if (book != null)
        {
            book.SetActive(true);
        }

        //reset PageSpawner
        if (pageSpawner != null)
        {
            pageSpawner.ResetSpawner();
        }

    }
}
