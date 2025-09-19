using UnityEngine;

public class PageSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject page1Prefab;
    public GameObject page2Prefab;

    [Header("Settings")]
    [Range(0f, 1f)]
    public float page1Probability = 0.8f;
    public Transform spawnPoint;
    public int maxPages = 10;

    private int currentPageCount = 0;

    void Start()
    {
        currentPageCount = 0;
        SpawnFirstPage();
    }

    // first to spawn page1
    private void SpawnFirstPage()
    {
        if (currentPageCount >= maxPages) return;

        GameObject newPage = Instantiate(page1Prefab, spawnPoint.position, Quaternion.identity);

        Page pageScript = newPage.GetComponent<Page>();
        if (pageScript != null)
            pageScript.spawner = this;

        DragObject dragScript = newPage.GetComponent<DragObject>();
        if (dragScript != null)
            dragScript.spawner = this;

        currentPageCount++;
    }

    // random spawn
    public void SpawnPage()
    {
        if (currentPageCount >= maxPages)
            return; // reached maximum pages

        float rand = Random.value; // 0~1
        GameObject prefabToSpawn = (rand < page1Probability) ? page1Prefab : page2Prefab;

        GameObject newPage = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);

        Page pageScript = newPage.GetComponent<Page>();
        if (pageScript != null)
            pageScript.spawner = this;

        DragObject dragScript = newPage.GetComponent<DragObject>();
        if (dragScript != null)
            dragScript.spawner = this;

        currentPageCount++;
    }

    // when click or drag
    public void PageRemoved()
    {
        SpawnPage();
    }
}
