using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController singleton { get; private set; }
    public int enemyCount;
    public UnityAction endGameEvent;
    public float timer;
    private EnemyPlaceholders enemyPlaceholders;

    private void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        enemyPlaceholders = Resources.Load("EnemyPlaceholders") as EnemyPlaceholders;
        enemyCount = Random.Range(1, 7);
        enemyPlaceholders.SpawnEnemy(enemyCount);
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (enemyCount <= 0)
        {
            endGameEvent?.Invoke();
        }
    }
}
