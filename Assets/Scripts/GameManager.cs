using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField] AudioClip backgroundMusic;

	public static GameManager Instance;

	[Header("Level Settings")]
	public int sceneIndex = 1; // start on level 1
	public int playerTowerHP = 0;
	public int[] towerLimits = { 0, 0 };
	public Tower[] towerPrefabs;

	[Header("Enemy Settings")]
	public float spawnRateMin = 0f;
	public float spawnRateMax = 0f;
	public float enemyDwellTime = 0f;
	public Enemy enemyPrefab;
	public EnemyType enemyType;
	public Vector3 enemySpawnPosition;
	public int enemyCount = 0;

    AudioSource audioSource;
	Enemy[] totalEnemies;

	public void SetupLevel(int sceneIndex, int playerTowerHP, Tower[] towerPrefabs , int[] towerLimits)
	{
		this.sceneIndex = sceneIndex;
		this.playerTowerHP = playerTowerHP;
		this.towerPrefabs = towerPrefabs;
		this.towerLimits = towerLimits;
	}

	public void SetupEnemies(Enemy enemyPrefab, EnemyType enemyType, Vector3 enemySpawnPosition, 
		int enemyCount, float spawnRateMin, float spawnRateMax, float enemyDwellTime)
	{
		this.enemyPrefab = enemyPrefab;
		this.enemyType = enemyType;
		this.enemySpawnPosition = enemySpawnPosition;
		this.enemyCount = enemyCount;
		this.spawnRateMin = spawnRateMin;
		this.spawnRateMax = spawnRateMax;
		this.enemyDwellTime = enemyDwellTime;
	}

    private void Awake()
    {
		if (Instance == null)
			//if not, set instance to this
			Instance = this;
		//If instance already exists and it's not this:
		else if (Instance != this)
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);

	}
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(backgroundMusic);
    }

}
