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
        SetupPage(newPage);
    }

    // random spawn
    public void SpawnPage()
    {
        if (currentPageCount >= maxPages) return;

        float rand = Random.value; // 0~1
        GameObject prefabToSpawn = (rand < page1Probability) ? page1Prefab : page2Prefab;

        GameObject newPage = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
        SetupPage(newPage);
    }

    private void SetupPage(GameObject newPage)
    {
        if (newPage == null) return;

        Page pageScript = newPage.GetComponent<Page>();
        if (pageScript != null) pageScript.spawner = this;

        DragObject dragScript = newPage.GetComponent<DragObject>();
        if (dragScript != null) dragScript.spawner = this;

        currentPageCount++;
    }

    // when click or drag
    public void PageRemoved()
    {
        SpawnPage();
    }

    // clear prefab(s) at the location of a given child transform
    public void ClearByChild(Transform childPos, float tolerance = 0.01f)
    {
        if (childPos == null) return;

        GameObject[] allPages = GameObject.FindGameObjectsWithTag("Page");
        int destroyed = 0;

        foreach (GameObject page in allPages)
        {
            if (page != null && Vector3.Distance(page.transform.position, childPos.position) <= tolerance)
            {
                Destroy(page);
                destroyed++;
            }
        }

        currentPageCount -= destroyed;
        if (currentPageCount < 0) currentPageCount = 0;

        Debug.Log($"Cleared {destroyed} prefab(s) at {childPos.name} ({childPos.position})");
    }
}
