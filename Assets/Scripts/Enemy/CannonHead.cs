using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonHead : MonoBehaviour
{
    [SerializeField] Transform target;

    private void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(target.transform);
    }
}
