using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VelocityCheck : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float velocityX;
    [SerializeField] Text velocityText;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = target.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        velocityX = rb.velocity.x;
        velocityText.text = velocityX.ToString("F2") + "km/s";
    }
}
