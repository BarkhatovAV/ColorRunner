using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Button _button;

    private string _sceneName = "SampleScene";

    private void OnEnable()
    {
        _button.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnRestartButtonClick);
    }

    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        
    }
}
