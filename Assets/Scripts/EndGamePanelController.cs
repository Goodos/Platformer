using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGamePanelController : MonoBehaviour
{
    [SerializeField] Button restartButton;
    [SerializeField] Button quitButton;
    [SerializeField] Text timer;

    void Start()
    {
        restartButton.onClick.AddListener(RestartButton);
        quitButton.onClick.AddListener(QuitButton);
        timer.text = "Поздравляем!\nВы справились за " + Mathf.Round(GameController.singleton.timer).ToString() + " секунд!" ;
    }

    void RestartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    void QuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
#if UNITY_ANDROID
        Application.Quit();
#endif
    }
}
