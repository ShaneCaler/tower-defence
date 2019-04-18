using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    Scoreboard scoreboard;
    [SerializeField] int scorePerHit = 100;
    [SerializeField] AudioClip goalSFX;

    // Use this for initialization
    void Start () {
        scoreboard = FindObjectOfType<Scoreboard>();
	}
	
    void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().PlayOneShot(goalSFX);
        scoreboard.ScoreHit(scorePerHit);
        print("Scored");
    }

}
