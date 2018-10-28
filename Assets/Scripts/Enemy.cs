using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {

    [Header("General")]
    [SerializeField] float dwellTime = 1f;
    [SerializeField] Mesh meshForCollider;
    [SerializeField] GameObject hitFX;
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] float levelLoadDelay = 1f;

    [Header("Enemy Specific")]
    [SerializeField] int hitPoints = 10;
    [SerializeField] int scorePerKill = 25;

    Tower tower;

    void Start ()
    {
        tower = FindObjectOfType<Tower>();
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
        GameObject fx = Instantiate(hitFX, transform.position, Quaternion.identity);
        StartCoroutine(DestroyEffects(fx));
        hitPoints = hitPoints - tower.GetGunDamage();
        if (hitPoints <= 0 || hitPoints < 1)
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        StartCoroutine(DestroyEffects(fx));
        Destroy(gameObject);
    }

    IEnumerator DestroyEffects(GameObject fx)
    {
        yield return new WaitForSeconds(.5f);
        Destroy(fx);
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
    }

    void ReloadLevel() // referenced by string
    {
        SceneManager.LoadScene(0);
    }

}
