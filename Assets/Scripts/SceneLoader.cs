using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour {

	[Header("General")]
    [SerializeField] float levelLoadDelay = 4f;
    [SerializeField] Text countdown;

	[Header("First Level Settings")]
	[SerializeField] Enemy firstLevelEnemyPrefab;
	[SerializeField] int playerTowerHealth = 500;
	[SerializeField] int[] towerLimits = { 5 };
	[SerializeField] Tower[] towerPrefabs;
	[SerializeField] Vector3 enemySpawnPosition = new Vector3(-40f, 0f, 0f);
	[SerializeField] int enemyCount = 1;
	[SerializeField] float enemySpawnRateMin = 1.5f;
	[SerializeField] float enemySpawnRateMax = 5f;
	[SerializeField] float enemyDwellTime = 2.5f;

	int sceneIndex = 1;
	
    // Use this for initialization
    void Start()
    {
        StartCoroutine(Countdown());
		sceneIndex = GameManager.Instance.sceneIndex;
        Invoke("LoadNextLevel", levelLoadDelay);
		if(sceneIndex == 1)
		{
			GameManager.Instance.SetupLevel(sceneIndex, playerTowerHealth, towerPrefabs, towerLimits);
			GameManager.Instance.SetupEnemies(firstLevelEnemyPrefab, EnemyType.Dog,
				enemySpawnPosition, enemyCount, enemySpawnRateMin, enemySpawnRateMax, enemyDwellTime);
		}
		else
		{
			// let the script on previous scene setup the level/enemies for levels beyond the first
		}
	}

    IEnumerator Countdown()
    {
        for (float i = levelLoadDelay; i >= 0; i--)
        {
            countdown.text = i.ToString() + " seconds";
            yield return new WaitForSeconds(1f);
        }

    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
