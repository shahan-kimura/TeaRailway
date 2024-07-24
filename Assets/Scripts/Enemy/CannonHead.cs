using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーへの首振りと敵レーザーの発射関係のクラス
/// </summary>
public class CannonHead : MonoBehaviour
{
    [SerializeField] Transform target;                      // 今回は敵用なのでPlayerをStartでGet

    [SerializeField, Header("敵レーザーPrefab")] 
    GameObject EnemyLaserPrefab;                            //敵レーザーのPrefab

    private float laserTimer = 0;
    [SerializeField, Header("射撃インターバル")]
    private float attackInterval = 5f;
    [SerializeField, Header("射撃インターバルの個体差")]
    float fireDeltaPeriod = 5f;

    private void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        attackInterval += Random.Range(0, fireDeltaPeriod);     //射撃インターバルに個体乱数追加
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(target.transform);

        laserTimer += Time.deltaTime;
        if(laserTimer > attackInterval)
        {
            laserTimer = 0;
            EnemyLaserAttack();

        }
    }

    private void EnemyLaserAttack()
    {
        // ターゲットがnullであるかどうかを確認する
        if (target == null)
        {
            Debug.LogWarning("Trying to fire laser at a null target.");
            return;
        }

        GameObject enemylaser = Instantiate(EnemyLaserPrefab, transform.position, transform.rotation);
        LaserFire enemylaserScript = enemylaser.GetComponent<LaserFire>();
        enemylaserScript.SetTarget(target.transform);
    }
}
