using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    [Header("General")]
    [SerializeField] float dwellTime = .5f;
    [SerializeField] Mesh meshForCollider;
    [SerializeField] ParticleSystem hitFX;
    [SerializeField] ParticleSystem deathFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] ParticleSystem goalFX;
    [SerializeField] Transform parent;
    EnemySpawner enemySpawner;
    Camera mainCamera;

    [Header("Enemy Specific")]
    [SerializeField] int hitPoints = 10;

    Tower tower;
    int numEnemiesKilled;

    void Start ()
    {
        AddCollider();
        FindAndFollowPath();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        mainCamera = FindObjectOfType<Camera>();
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
        AudioSource.PlayClipAtPoint(deathSFX, mainCamera.transform.position);
        Destroy(gameObject);
        enemySpawner.IncreaseEnemiesKilled();
    }

    private void FindAndFollowPath()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>(); // can be dangerous if we have another pathfinder
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(dwellTime);
        }
        SelfDestruct();
    }

    private void SelfDestruct()
    {
        ParticleSystem fx = Instantiate(goalFX, transform.position, Quaternion.identity);
        fx.Play();
        Destroy(gameObject);
    }

}
