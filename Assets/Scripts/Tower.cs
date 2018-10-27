using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;
    [SerializeField] float rangeOfAttack = 25f;
    [SerializeField] GameObject gunEffects;
    [SerializeField] int gunDamage = 1;

    // Update is called once per frame
    void Update () {
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

    public int GetGunDamage()
    {
        return gunDamage;
    }

    void ProcessFiring()
    {
        var emissionModule = gunEffects.GetComponent<ParticleSystem>().emission;
        float distance = Vector3.Distance(targetEnemy.position, objectToPan.position);
        if(distance < rangeOfAttack)
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
