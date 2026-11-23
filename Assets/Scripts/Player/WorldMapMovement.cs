using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class WorldMapMovement : MonoBehaviour
{
    //public Animator anim;
    int levelSelected;
    [SerializeField] GameObject[] levels;

    public Controls controls;
    private InputAction move;
    private InputAction jump;


    void Awake()
    {
        controls = new Controls();
    }

    void OnEnable()
    {
        move = controls.Main.Move;
        move.Enable();

        jump = controls.Main.Jump;
        jump.Enable();
    }

    void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }

    void Start()
    {
        levelSelected = GameManager.instance.lastLevelPlayed;

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
        if (move.WasPressedThisFrame())
        { 
            if (move.ReadValue<float>() > 0)
            {
                if (GameManager.instance.levelCompletion[levelSelected] == true && levelSelected < levels.Length - 1) levelSelected++;
            }
            else if (move.ReadValue<float>() < 0)
            {
                if (levelSelected > 0) levelSelected--;
            }
        }

        if(levels[levelSelected] != null) transform.position = levels[levelSelected].transform.position;

        if (jump.WasPressedThisFrame())
        {
            SceneManager.LoadScene("Level" + levelSelected);
        }

    }
}
