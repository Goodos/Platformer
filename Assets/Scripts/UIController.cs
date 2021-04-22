using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text enemiesLeft;
    [SerializeField] GameObject endGamePanel;
    private bool spawnPanelFlag = true;

    void Start()
    {
        GameController.singleton.endGameEvent += ShowEndGamePanel;
    }

    void Update()
    {
        enemiesLeft.text = "Enemies: " + GameController.singleton.enemyCount.ToString();
    }

    void ShowEndGamePanel()
    {
        if (spawnPanelFlag)
        {
            Instantiate(endGamePanel, transform);
            spawnPanelFlag = false;
        }
    }
}
