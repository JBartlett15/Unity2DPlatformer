using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    GameObject manager;
    GameObject player;

    void Start()
    {
        manager = GameObject.Find(GameManager);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            manager.GetComponent<GameManager>().number++;
            SceneManager.LoadScene(1);
        }
    }
}
