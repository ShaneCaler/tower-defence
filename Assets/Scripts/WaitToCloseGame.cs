using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitToCloseGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(CloseGame());
    }

	IEnumerator CloseGame()
	{
		yield return new WaitForSeconds(5f);
		Application.Quit();
	}

}
