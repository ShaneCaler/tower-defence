using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] List<Waypoint> path;
    [SerializeField] float dwellTime = 1f;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(FollowWaypoints());
    }

    IEnumerator FollowWaypoints()
    {
        print("Starting patrol");
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            print("visiting " + waypoint.name);
            yield return new WaitForSeconds(dwellTime);
        }
        print("ending patrol");
    }

}
