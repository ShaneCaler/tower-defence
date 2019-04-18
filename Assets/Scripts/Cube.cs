using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
	[SerializeField] private Material isPlaceable;
	[SerializeField] private Material placed;
	[SerializeField] private Material unPlaceable;

	[SerializeField] private bool useMeshFilter = false;
	[SerializeField] private Mesh isPlaceableFilter;
	[SerializeField] private Mesh placedFilter;
	[SerializeField] private Mesh unPlaceableFilter;

	// Start is called before the first frame update
	void Start()
    {
		if(gameObject.GetComponent<Waypoint>() == null)
		{
			if(useMeshFilter)
				gameObject.GetComponentInChildren<MeshFilter>().mesh = unPlaceableFilter;
			else
				gameObject.GetComponentInChildren<MeshRenderer>().material = unPlaceable;
		}
		else if (!gameObject.GetComponent<Waypoint>().isPlaceable)
		{
			if (useMeshFilter)
				gameObject.GetComponentInChildren<MeshFilter>().mesh = unPlaceableFilter;
			else
				gameObject.GetComponentInChildren<MeshRenderer>().material = unPlaceable;
		}
	}

    // Update is called once per frame
    void Update()
    {
		if (gameObject.GetComponent<Waypoint>() != null && gameObject.GetComponent<Waypoint>().hasTower)
		{
			if (useMeshFilter)
				gameObject.GetComponentInChildren<MeshFilter>().mesh = placedFilter;
			else
				gameObject.GetComponentInChildren<MeshRenderer>().material = placed;
		}
		else if(gameObject.GetComponent<Waypoint>() != null 
			&& !gameObject.GetComponent<Waypoint>().hasTower
			&& gameObject.GetComponent<Waypoint>().isPlaceable)
		{
			if (useMeshFilter)
				gameObject.GetComponentInChildren<MeshFilter>().mesh = isPlaceableFilter;
			else
				gameObject.GetComponentInChildren<MeshRenderer>().material = isPlaceable;
		}
	}
}
