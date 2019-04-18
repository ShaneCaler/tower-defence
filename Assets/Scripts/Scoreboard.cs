using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scoreboard : MonoBehaviour {

	[SerializeField] AudioClip victorySFX;
	[SerializeField] Text winText;

    int healthPoints;
    Text healthText;
    int currentHealth;
	Enemy[] totalEnemies;
	EnemySpawner enemySpawner;
	AudioSource audioSource;

    void Start()
    {
		enemySpawner = FindObjectOfType<EnemySpawner>();
		audioSource = GetComponent<AudioSource>();
		healthPoints = GameManager.Instance.playerTowerHP;
        healthText = GetComponent<Text>();
        healthText.text = healthPoints.ToString();
        currentHealth = healthPoints;
    }

	private void Update()
	{
		totalEnemies = FindObjectsOfType<Enemy>();
		if ((totalEnemies.Length == 0 || totalEnemies == null) && enemySpawner.spawnCounter == enemySpawner.numOfEnemiesToSpawn)
		{
			Debug.Log("Game Over, win!");
			winText.text = "Victory!";
			audioSource.PlayOneShot(victorySFX);
			Invoke("SceneLoader", 3f);
		}
	}

	public void ScoreHit(int scorePerHit)
    {
        currentHealth -= scorePerHit;
        if (currentHealth <= 0)
        {
			healthText.text = 0.ToString();
			winText.text = "Failed :(";
			Invoke("SceneLoader", 3f);
        }
        else
        {
            healthText.text = currentHealth.ToString();
        }
    }

    private void SceneLoader()
    {
		Debug.Log("Game over");
		if (GameManager.Instance.sceneIndex > 1)
			SceneManager.LoadScene(4);
		else
			SceneManager.LoadScene(2);
    }
}
