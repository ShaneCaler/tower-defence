using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
    [SerializeField] float waitTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, waitTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
