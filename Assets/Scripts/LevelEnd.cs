using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] int levelNumber;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            GameManager.instance.levelCompletion[levelNumber] = true;
            SceneManager.LoadScene(1);
        }
    }
}
