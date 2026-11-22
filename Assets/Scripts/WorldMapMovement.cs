using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMapMovement : MonoBehaviour
{
    //public Animator anim;
    int levelSelected;
    [SerializeField] Transform[] levels;


    void Start()
    {
        if (levels[GameManager.instance.levelNumber] != null)
            levelSelected = GameManager.instance.levelNumber;
        else
            levelSelected = 0;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(levelSelected < GameManager.instance.levelNumber && levels[GameManager.instance.levelNumber + 1] != null) levelSelected++;
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (levelSelected > 0) levelSelected--;
        }

        if(levels[levelSelected] != null) transform.position = levels[levelSelected].position;

        if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene("Level" + levelSelected);
        }

    }
}
