using Unity.VisualScripting;
using UnityEngine;

public class ReturnBook : MonoBehaviour
{
    [Header("Positions to Check")]
    public Transform targetChildPos1;  // deskPos
    public Transform targetChildPos2;  // flamePos
    public Transform targetChildPos3;  // bookPos
    public string prefabTag = "Page";

    [Header("Book Object")]
    public GameObject book;

    [Header("GameOver UI")]
    public GameObject gameOverSprite;

    [Header("Spawner Reference")]
    public PageSpawner pageSpawner;

    [Header("Tolerance")]
    public float positionTolerance = 0.1f;

    void Start()
    {
        gameObject.SetActive(false);
        if (gameOverSprite != null) gameOverSprite.SetActive(false);
    }

    void OnMouseDown()
    {
        if (targetChildPos1 == null || targetChildPos2 == null || targetChildPos3 == null) return;

        //If Pos1 & targetChildPos2 all empty
        if (IsPositionEmpty(targetChildPos1.position) && IsPositionEmpty(targetChildPos2.position))
        {
            bool allCorrect = AllPrefabsAtPos3AreRealPages();

            // clear all prefab at Pos3
            int cleared = ClearPrefabsAtPosition(targetChildPos3.position);

            if (allCorrect)
            {
                // if correct, hide book
                if (cleared > 0 && book != null)
                    book.SetActive(false);

                Debug.Log($"ReturnBook clicked: cleared {cleared} prefab(s) at pos3 ¡ú book hidden.");
            }
            else
            {
                // if wrong, GameOver
                if (gameOverSprite != null)
                    gameOverSprite.SetActive(true);

                Debug.Log($"ReturnBook clicked: wrong prefab(s) found ¡ú cleared {cleared}, GameOver!");

                // reset Spawner
                if (pageSpawner != null)
                {
                    pageSpawner.ResetSpawner();
                    Debug.Log("PageSpawner has been reset after GameOver.");
                }
            }
        }
        else
        {
            Debug.Log("ReturnBook clicked, but position1/position2 not empty. No clear.");
        }
    }

    private bool IsPositionEmpty(Vector3 pos)
    {
        GameObject[] allPrefabs = GameObject.FindGameObjectsWithTag(prefabTag);
        foreach (GameObject go in allPrefabs)
        {
            if (Vector3.Distance(go.transform.position, pos) <= positionTolerance)
                return false;
        }
        return true;
    }

    private int ClearPrefabsAtPosition(Vector3 pos)
    {
        GameObject[] allPrefabs = GameObject.FindGameObjectsWithTag(prefabTag);
        int destroyed = 0;
        foreach (GameObject go in allPrefabs)
        {
            if (Vector3.Distance(go.transform.position, pos) <= positionTolerance)
            {
                Destroy(go);
                destroyed++;
            }
        }
        return destroyed;
    }

    private bool AllPrefabsAtPos3AreRealPages()
    {
        GameObject[] allPrefabs = GameObject.FindGameObjectsWithTag(prefabTag);
        foreach (GameObject go in allPrefabs)
        {
            if (Vector3.Distance(go.transform.position, targetChildPos3.position) <= positionTolerance)
            {
                if (!go.name.Contains("realPage"))
                    return false; //if wrong return false
            }
        }
        return true;
    }
}
