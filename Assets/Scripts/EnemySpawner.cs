using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [Range(2f, 50f)][SerializeField] float secondsBetweenSpawns = 3f;
    [SerializeField] int numOfEnemiesToSpawn = 10;
    [SerializeField] Enemy enemy;
    [SerializeField] Transform enemyParent;
    [SerializeField] bool spawnInfiniteEnemies = false;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        if (!spawnInfiniteEnemies)
        {
            for (int i = 0; i < numOfEnemiesToSpawn; i++)
            {
                Enemy newEnemy = Instantiate(enemy);
                newEnemy.transform.parent = enemyParent.transform;
                newEnemy.name = "Enemy " + (i + 1);
                yield return new WaitForSeconds(secondsBetweenSpawns);
            }
        }
        else
        {
            while (true)
            {
                Instantiate(enemy);
                yield return new WaitForSeconds(secondsBetweenSpawns);
            }
        }

    }
}
