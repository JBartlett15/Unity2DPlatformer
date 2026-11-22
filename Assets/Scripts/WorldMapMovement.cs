using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMapMovement : MonoBehaviour
{
    //public Animator anim;
    int levelSelected;
    [SerializeField] GameObject[] levels;


    void Start()
    {
        levelSelected = 0;

        print("level selected: " + levelSelected);

        for (int i = 0; i < levels.Length; i++)
        {
            if (GameManager.instance.levelCompletion[i] == true)
            {
                levels[i].GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                levels[i].GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (GameManager.instance.levelCompletion[levelSelected] == true) levelSelected++;
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (levelSelected > 0) levelSelected--;
        }

        if(levels[levelSelected] != null) transform.position = levels[levelSelected].transform.position;

        if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene("Level" + levelSelected);
        }

    }
}
