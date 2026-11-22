using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int levelNumber;

    public static GameManager instance;

    void Awake()
    {
        if (instance == null) //if there is no instance of a game manager
            instance = this; //make this the instance
        else
            Destroy(this); //otherwise, get rid of this one
    }

    void Update()
    {
        print(levelNumber);
    }
}
