using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int number;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        print(number);
    }
}
