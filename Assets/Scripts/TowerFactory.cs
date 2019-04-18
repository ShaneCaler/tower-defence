using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerFactory : MonoBehaviour {

    [SerializeField] Tower[] towerPrefabs;
    [SerializeField] Transform towerParentTransform;
	[SerializeField] Button towerAButton, towerBButton;
	[SerializeField] GameObject towerAPanel, towerBPanel;
	[SerializeField] Text towerAUses, towerBUses;

	Queue<Tower>[] towers = new Queue<Tower>[0] { };
	int[] towerLimits = { 5, 1 };
	int[] towerUses = { 0, 0 };
	Waypoint baseWaypoint;
	int towerIndex = 0;

	private void Start()
	{
		towerLimits = GameManager.Instance.towerLimits;
		towerPrefabs = GameManager.Instance.towerPrefabs;
		towers = new Queue<Tower>[towerPrefabs.Length];
		for(int i = 0; i < towerPrefabs.Length; i++)
		{
			towers[i] = new Queue<Tower>();
			towerUses[i] = towerLimits[i];
		}

		if (GameManager.Instance.sceneIndex > 1)
		{
			towerBPanel.SetActive(false);
			towerAButton.onClick.AddListener(SetTowerA);
			towerBButton.onClick.AddListener(SetTowerB);
		}
	}

	public void AddTower(Waypoint baseWaypoint)
    {
		this.baseWaypoint = baseWaypoint;

		if(towerIndex == 0)
		{
			var towersInScene = towers[0].Count;
			if (towersInScene < towerLimits[0])
			{
				InstantiateNewTower(baseWaypoint, towerIndex);
			}
			else
			{
				MoveExistingTower(baseWaypoint, towerIndex);
			}
		}
		else if(towerIndex == 1)
		{
			var towersInScene = towers[1].Count;
			if (towersInScene < towerLimits[1])
			{
				InstantiateNewTower(baseWaypoint, towerIndex);
			}
			else
			{
				MoveExistingTower(baseWaypoint, towerIndex);
			}
		}


    }

	private void Update()
	{
		if(GameManager.Instance.sceneIndex > 1)
		{
			towerAUses.text = towerUses[0].ToString();
			towerBUses.text = towerUses[1].ToString();
		}

	}

	private void InstantiateNewTower(Waypoint baseWaypoint, int index)
    {
        var newTower = Instantiate(towerPrefabs[index], baseWaypoint.transform.position, Quaternion.identity);
        newTower.baseWaypoint = baseWaypoint;
        newTower.transform.parent = towerParentTransform.transform;
        baseWaypoint.isPlaceable = false;
		baseWaypoint.hasTower = true;
		towerUses[index]--;
        towers[index].Enqueue(newTower);
    }

	private void SetTowerA()
	{
		towerIndex = 0;
		towerAPanel.SetActive(true);
		towerBPanel.SetActive(false);
	}

	private void SetTowerB()
	{
		towerIndex = 1;
		towerAPanel.SetActive(false);
		towerBPanel.SetActive(true);
	}


	private void MoveExistingTower(Waypoint newBaseWaypoint, int index)
    {
        var oldTower = towers[index].Dequeue();
        oldTower.baseWaypoint.isPlaceable = true;
		oldTower.baseWaypoint.hasTower = false;
        newBaseWaypoint.isPlaceable = false;
		newBaseWaypoint.hasTower = true;
        oldTower.baseWaypoint = newBaseWaypoint;
        oldTower.transform.position = newBaseWaypoint.transform.position;

        towers[index].Enqueue(oldTower);
    }
}
