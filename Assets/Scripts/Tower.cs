using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    // parameters
    [SerializeField] Transform objectToPan;
    [SerializeField] float distanceToTarget = 25f;
    [SerializeField] GameObject gunEffects;
    [SerializeField] int gunDamage = 1;
    public Waypoint baseWaypoint; // what the tower is standing on

    // state
    Transform targetEnemy;

    // Update is called once per frame
    void Update ()
    {
        SetTargetEnemy();

        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy);
            ProcessFiring();
        }
        else
        {
            StopFiring();
        }
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<Enemy>();
        if(sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach (Enemy individualEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, individualEnemy.transform);
        }

        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        float distanceToClosest = Vector3.Distance(transform.position, transformA.position);
        float distanceToTest = Vector3.Distance(transform.position, transformB.position);

        if (distanceToClosest < distanceToTest)
        { return transformA; }

        return transformB;
    }

    public int GetGunDamage()
    {
        return gunDamage;
    }

    void ProcessFiring()
    {
        var emissionModule = gunEffects.GetComponent<ParticleSystem>().emission;
        float distance = Vector3.Distance(targetEnemy.position, objectToPan.position);
        if(distance < distanceToTarget)
        {
            emissionModule.enabled = true;
        }
        else
        {
            emissionModule.enabled = false;
        }
    }

    void StopFiring()
    {
        var emissionModule = gunEffects.GetComponent<ParticleSystem>().emission;
        emissionModule.enabled = false;
    }
}
