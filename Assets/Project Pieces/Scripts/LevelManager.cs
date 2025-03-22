using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static string nextSpawnPoint;
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    public static void SetNextSpawnPoint(string spawnPoint)
    {
        nextSpawnPoint = spawnPoint;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && !string.IsNullOrEmpty(nextSpawnPoint))
        {
            Transform spawnPoint = GameObject.Find(nextSpawnPoint)?.transform;
            if (spawnPoint != null)
            {
                player.transform.position = spawnPoint.position;
            }
            else
            {
                Debug.LogWarning("Spawn point not found!");
            }
        }
    }
}
