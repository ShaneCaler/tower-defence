using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {

    [Header("General")]
    [SerializeField] float dwellTime = 1f;
    [SerializeField] Mesh meshForCollider;
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
        hitPoints = hitPoints - tower.GetGunDamage();
        print(hitPoints);
        if (hitPoints <= 0 || hitPoints < 1)
        {
            GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
            fx.transform.parent = parent;
            Destroy(gameObject);
            Invoke("ReloadLevel", levelLoadDelay);
        }
    }

    private void FindAndFollowPath()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>(); // can be dangerous if we have another pathfinder
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        print("Starting patrol");
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(dwellTime);
        }
        print("ending patrol");
    }

    void ReloadLevel() // referenced by string
    {
        SceneManager.LoadScene(0);
    }

}
