using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{

	[SerializeField] private Button yes, no;
	[SerializeField] Enemy nextLevelEnemyPrefab;
	[SerializeField] int sceneIndex = 3;
	[SerializeField] int playerTowerHealth = 500;
	[SerializeField] int[] towerLimits = { 5, 1 };
	[SerializeField] Tower[] towerPrefabs;
	[SerializeField] Vector3 enemySpawnPosition = new Vector3(-40f, 0f, 0f);
	[SerializeField] int enemyCount = 25;
	[SerializeField] float enemySpawnRateMin = 1.5f;
	[SerializeField] float enemySpawnRateMax = 4f;
	[SerializeField] float enemyDwellTime = 2f;

	// Start is called before the first frame update
	void Start()
    {
		yes.onClick.AddListener(LoadNextScene);
		no.onClick.AddListener(ExitGame);
    }

	private void LoadNextScene()
	{
		SceneManager.LoadScene(0);
		GameManager.Instance.SetupLevel(sceneIndex, playerTowerHealth, towerPrefabs, towerLimits);
		GameManager.Instance.SetupEnemies(nextLevelEnemyPrefab, EnemyType.Warrior, 
			enemySpawnPosition, enemyCount, enemySpawnRateMin, enemySpawnRateMax, enemyDwellTime);
	}

	private void ExitGame()
	{
		SceneManager.LoadScene(4);
		Debug.Log("Quitting.");
		Application.Quit();
	}

}
