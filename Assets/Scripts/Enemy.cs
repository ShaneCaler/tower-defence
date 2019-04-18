using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	[SerializeField] int hitPoints = 10;
	[SerializeField] Mesh meshForCollider;
    [SerializeField] ParticleSystem hitFX;
    [SerializeField] ParticleSystem deathFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] ParticleSystem goalFX;

	EnemyType enemyType;
    EnemySpawner enemySpawner;
	Animator anim;
    Camera mainCamera;
    Tower tower;
	Pathfinder pathfinder;
    int numEnemiesKilled;
	float dwellTime = .5f;

	void Start ()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        mainCamera = FindObjectOfType<Camera>();
		pathfinder = FindObjectOfType<Pathfinder>();
		enemyType = GameManager.Instance.enemyType;
		dwellTime = GameManager.Instance.enemyDwellTime;

		if (enemyType == EnemyType.Warrior)
			anim = GetComponent<Animator>();

		AddCollider();
		FindAndFollowPath();
	}

    private void AddCollider()
    {
        MeshCollider meshCol = gameObject.AddComponent<MeshCollider>();
        meshCol.isTrigger = false;
        meshCol.sharedMesh = meshForCollider;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        var fx = Instantiate(hitFX, transform.position, Quaternion.identity);
        fx.transform.parent = gameObject.transform;
        fx.Play();
        tower = FindObjectOfType<Tower>();
        Destroy(fx, .5f);
        // todo support multiple types of tower
        // towers = FindObjectsOfType<Tower>();
        // foreach to cycle through diff. towers ?
        hitPoints = hitPoints - tower.GetGunDamage();
        if (hitPoints <= 0)
        {
            GetComponent<AudioSource>().PlayOneShot(deathSFX);
            DestroyEnemy();
        }
    }

    public void ReachGoal()
    {
        Destroy(gameObject);
    }

    private void DestroyEnemy()
    {
        var fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.Play();
        Destroy(fx, 2f);
		enemySpawner.IncreaseEnemiesKilled();
		AudioSource.PlayClipAtPoint(deathSFX, mainCamera.transform.position);
        Destroy(gameObject);
    }

    private void FindAndFollowPath()
    {
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
		if (enemyType == EnemyType.Warrior)
			anim.SetBool("Moving", true);

		for(int i = 0; i < path.Count; i++)
		{
			Debug.Log("Position = " + transform.position);
			Debug.Log("path[i] = " + path[i].gameObject.name);
			if (path[i] == pathfinder.start)
			{
				// do nothing
			}
			else if(path[i] == pathfinder.end)
			{
				Debug.Log("Looking at " + pathfinder.end.transform);
				transform.LookAt(pathfinder.end.transform);
			}
			else if (path[i+1] != null)
			{
				Debug.Log("Looking at " + path[i + 1].transform);
				transform.LookAt(path[i + 1].transform);
			}


			transform.position = path[i].transform.position;
			yield return new WaitForSeconds(dwellTime);
		}

        /*foreach (Waypoint waypoint in path)
        {

        }*/

		if (enemyType == EnemyType.Warrior)
			anim.SetBool("Moving", false);
		SelfDestruct();
    }

    private void SelfDestruct()
    {
        ParticleSystem fx = Instantiate(goalFX, transform.position, Quaternion.identity);
        fx.Play();
        Destroy(gameObject);
    }

	//Placeholder​ ​functions​ ​for​ ​Animation​ ​events
	public void Hit() { }
	public void Shoot() { }
	public void FootR() { }
	public void FootL() { }
	public void Land() { }
	public void WeaponSwitch() { }

}
