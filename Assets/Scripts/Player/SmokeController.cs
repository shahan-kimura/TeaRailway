using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SmokeController : MonoBehaviour
{
    // クラスレベルでparticleSystemとmainModuleを宣言
    private ParticleSystem particleSystem;
    private MainModule mainModule;
    private EmissionModule emissionModule;

    // Start is called before the first frame update
    void Start()
    {
        //psとpsの各Moduleを初期化
        particleSystem = GetComponent<ParticleSystem>();
        mainModule = particleSystem.main;
        emissionModule = particleSystem.emission;
    }

    // Update is called once per frame


    public void SmokeUp()
    {
        Debug.Log("smoke");
        mainModule.startSpeed = 5f;
        emissionModule.rateOverTime = 20;
    }
    public void SmokeDown()
    {
        Debug.Log("smokeDown");
        mainModule.startSpeed = 1f;
        emissionModule.rateOverTime = 6;

    }
}
