using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger_SceneChange : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string spawnPointName;


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name); // Debugging
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger! Loading scene: " + sceneToLoad);
            LevelManager.SetNextSpawnPoint(spawnPointName);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
