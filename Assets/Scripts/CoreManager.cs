using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreManager : MonoBehaviour
{
    public static CoreManager CoreManagerSingleton { get; private set; }

    public GameControls GameControls 
    {
        get 
        {
            if (_gameControls is null) _gameControls = new GameControls();
            return _gameControls;
        }
        private set => _gameControls = value;
    }
    private GameControls _gameControls;

    public CoreManager() 
    {
        CoreManagerSingleton = this;
    }

    private void Awake()
    {
        CoreManagerSingleton = this;
        GameControls = new GameControls();    
    }

    public void LoadScene(int index) 
    {
        SceneManager.LoadScene(index);
    }
}
