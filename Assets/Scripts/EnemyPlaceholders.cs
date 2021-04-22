using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyPlaceholders : ScriptableObject
{
    [SerializeField] private List<GameObject> placeholders = new List<GameObject>();

    public void SpawnEnemy(int enemyCount)
    {
        GameObject enemyPrefab = Resources.Load("Enemy") as GameObject;

        for (int i = 1; i <= enemyCount; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab);
            Vector3 placeForEnemy = placeholders[Random.Range(0, placeholders.Capacity)].transform.position;
            newEnemy.transform.position = new Vector3(placeForEnemy.x, placeForEnemy.y + .5f);
        }
    }
}
