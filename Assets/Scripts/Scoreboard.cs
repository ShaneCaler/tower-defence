using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scoreboard : MonoBehaviour {

    [SerializeField] int healthPoints;
    Text healthText;
    int currentHealth;

    void Start()
    {
        healthText = GetComponent<Text>();
        healthText.text = healthPoints.ToString();
        currentHealth = healthPoints;
    }

    public void ScoreHit(int scorePerHit)
    {
        currentHealth -= scorePerHit;
        if (currentHealth <= 0)
        {
            Invoke("SceneLoader", 1f);
        }
        else
        {
            healthText.text = currentHealth.ToString();
        }
    }

    private void SceneLoader()
    {
        SceneManager.LoadScene(0);
    }
}
