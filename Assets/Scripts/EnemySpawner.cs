using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemySpawner : MonoBehaviour {

    [SerializeField] int numOfEnemiesToSpawn = 10;
    [SerializeField] Enemy enemy;
    [SerializeField] Transform enemyParent;
    [SerializeField] Text remainingNumberText;
    [SerializeField] Text killedNumberText;
    [SerializeField] GameObject spawnEnemyFX;
    [SerializeField] AudioClip spawnEnemySFX;

    int numEnemiesRemaining;
    int numEnemiesKilled;
    float secondsBetweenSpawns = .5f;

    // Use this for initialization
    void Start () {
        StartCoroutine(SpawnEnemies());
        numEnemiesRemaining = numOfEnemiesToSpawn - 1;
        numEnemiesKilled = 0;
        remainingNumberText.text = numEnemiesRemaining.ToString();
        killedNumberText.text = "0/" + numOfEnemiesToSpawn;
    }

    private void Update()
    {
        secondsBetweenSpawns = Random.Range(1f, 5f);
    }


    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < numOfEnemiesToSpawn; i++)
        {
            DecreaseRemainingEnemies();
            GetComponent<AudioSource>().PlayOneShot(spawnEnemySFX);
            Enemy newEnemy = Instantiate(enemy);
            newEnemy.transform.parent = enemyParent.transform;
            newEnemy.name = "Enemy " + (i + 1);
            GameObject fx = Instantiate(spawnEnemyFX, newEnemy.transform.position, Quaternion.identity);
            fx.transform.parent = newEnemy.transform;
            StartCoroutine(DestroyEffects(fx));
            remainingNumberText.text = numEnemiesRemaining.ToString();
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }

    IEnumerator DestroyEffects(GameObject fx)
    {
        yield return new WaitForSeconds(.5f);
        Destroy(fx);
    }

    public void DecreaseRemainingEnemies()
    {
        if(numEnemiesRemaining > 0)
        {
            numEnemiesRemaining--;
            print(numEnemiesRemaining);
            remainingNumberText.text = numEnemiesRemaining.ToString();
        }
        else
        {
            remainingNumberText.text = 0.ToString();
        }
    }

    public void IncreaseEnemiesKilled()
    {
        if (numEnemiesKilled == numOfEnemiesToSpawn)
        {
            killedNumberText.text = numEnemiesKilled.ToString() + "/" + numOfEnemiesToSpawn;
        }
        else
        {
            numEnemiesKilled++;
            killedNumberText.text = numEnemiesKilled.ToString() + "/" + numOfEnemiesToSpawn;
        }
    }

}
