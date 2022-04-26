using System.Collections;
using UnityEngine;

public class ScreenChanger : MonoBehaviour
{
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private PlayerMovement _playerMovement;

    private float _delayBeforeOpeningGameOverScreen = 1.8f;
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
        StartCoroutine(OpenGameOverScreen());
    }

    private IEnumerator OpenGameOverScreen()
    {
        yield return new WaitForSeconds(_delayBeforeOpeningGameOverScreen);
        _gameOverScreen.Open();
    }
}
