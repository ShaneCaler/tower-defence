using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum EnemyType
{
	Dog,
	Warrior
}


public class EnemySpawner : MonoBehaviour {

    [SerializeField] Text remainingNumberText;
    [SerializeField] Text killedNumberText;
    [SerializeField] GameObject spawnEnemyFX;
    [SerializeField] AudioClip spawnEnemySFX;

	public int spawnCounter = 0;
	public int numOfEnemiesToSpawn = 10;

	Enemy enemy;
	Enemy[] totalEnemies;
	Vector3 enemySpawnPosition;
	int numEnemiesRemaining;
    int numEnemiesKilled;
	float secondsBetweenSpawns = .5f;
	[SerializeField] float spawnRateMin, spawnRateMax = 0f;

    // Use this for initialization
    void Start () {
		numOfEnemiesToSpawn = GameManager.Instance.enemyCount;
		spawnRateMin = GameManager.Instance.spawnRateMin;
		spawnRateMax = GameManager.Instance.spawnRateMax;
		enemy = GameManager.Instance.enemyPrefab;
		Debug.Log("Enemy " + enemy.gameObject.name);

		numEnemiesRemaining = numOfEnemiesToSpawn;
        numEnemiesKilled = 0;
        remainingNumberText.text = numEnemiesRemaining.ToString();
        killedNumberText.text = "0/" + numOfEnemiesToSpawn;

		StartCoroutine(SpawnEnemies());
	}

	private void Update()
    {
        secondsBetweenSpawns = Random.Range(spawnRateMin, spawnRateMax);
    }


	IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < numOfEnemiesToSpawn; i++)
        {
			spawnCounter++;

			GetComponent<AudioSource>().PlayOneShot(spawnEnemySFX);
			Enemy newEnemy = Instantiate(enemy, enemySpawnPosition, Quaternion.identity);
			newEnemy.transform.LookAt(new Vector3(newEnemy.transform.position.x + 20f, 0f, 0f));
           // newEnemy.transform.parent = transform;
            newEnemy.name = "Enemy " + (i + 1);

			GameObject fx = Instantiate(spawnEnemyFX, newEnemy.transform.position, Quaternion.identity);
            fx.transform.parent = newEnemy.transform;
            StartCoroutine(DestroyEffects(fx));

			numEnemiesRemaining--;
			Debug.Log("num remaining " + numEnemiesRemaining);
			remainingNumberText.text = numEnemiesRemaining.ToString();

			yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }

    IEnumerator DestroyEffects(GameObject fx)
    {
        yield return new WaitForSeconds(.5f);
        Destroy(fx);
    }

    public void IncreaseEnemiesKilled()
    {
		numEnemiesKilled++;
		killedNumberText.text = numEnemiesKilled.ToString() + "/" + numOfEnemiesToSpawn;
	}

}
