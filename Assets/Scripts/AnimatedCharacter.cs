using UnityEngine;

public class AnimatedCharacter : MonoBehaviour
{
    private PlayerController _playerController;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();      
        _playerController = GetComponentInParent<PlayerController>();
        _playerController.onStateChanged.AddListener((state) => _animator.SetBool(state, true));
    }
}
