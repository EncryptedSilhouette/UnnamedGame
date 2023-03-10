using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreManager : MonoBehaviour
{
    public static CoreManager CoreManagerSingleton { get; private set; }

    [SerializeField]
    private bool _spawnPlayer = true;
    [SerializeField]
    private Vector3 _spawnPosition = Vector3.up;
    [SerializeField]
    private GameObject _playerPrefab;

    private bool _playerExists = false;

    private void Awake()
    {
        CoreManagerSingleton = this; 
    }

    private void Start()
    {
        if (_spawnPlayer && !_playerExists)
        {
            _playerExists = true;
            Instantiate(_playerPrefab, _spawnPosition, Quaternion.identity);
        }
    }

    public void LoadScene(int index) 
    {
        SceneManager.LoadScene(index);
    }
}
