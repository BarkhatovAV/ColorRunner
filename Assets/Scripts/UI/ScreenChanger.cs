using UnityEngine;

public class ScreenChanger : MonoBehaviour
{
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private PlayerMovement _playerMovement;

    private void OnEnable()
    {
        _playerMovement.FellDownStairs += OnFellDownStairs;
    }

    private void OnDisable()
    {
        _playerMovement.FellDownStairs -= OnFellDownStairs;
    }

    private void OnFellDownStairs()
    {
        _gameOverScreen.Open();
    }
}
