using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreManager : MonoBehaviour
{
    public static CoreManager CoreManagerSingleton { get; private set; }

    private void Awake()
    {
        CoreManagerSingleton = this; 
    }

    public void LoadScene(int index) 
    {
        SceneManager.LoadScene(index);
    }
}
