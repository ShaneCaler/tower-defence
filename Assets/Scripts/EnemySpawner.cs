using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [Range(2f, 50f)][SerializeField] float secondsBetweenSpawns = 3f;
    [SerializeField] Enemy enemy;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Instantiate(enemy);
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }
}
