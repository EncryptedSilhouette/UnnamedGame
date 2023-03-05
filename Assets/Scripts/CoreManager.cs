using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreManager : MonoBehaviour
{
    public static CoreManager CoreManagerSingleton { get; private set; }

    [SerializeField]
    private Vector3 _spawnPosition = Vector3.up;
    [SerializeField]
    private GameObject _playerPrefab;

    private void Awake()
    {
        CoreManagerSingleton = this; 
    }

    private void Start()
    {
        Instantiate(_playerPrefab, _spawnPosition, Quaternion.identity);
    }

    public void LoadScene(int index) 
    {
        SceneManager.LoadScene(index);
    }
}
