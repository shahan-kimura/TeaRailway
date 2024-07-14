using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject laserPrefab;    //レーザーprefabのアタッチが必要
    [SerializeField] GameObject laserSpawner;   //レーザー発射地点の子オブジェクト
    [SerializeField] string enemyTag = "Enemy"; // 敵のタグ

    [SerializeField] private List<GameObject> lockedTargets = new List<GameObject>();

    [SerializeField] private bool isLockingOn = false;               //ロックオン処理継続判定用のbool
    [SerializeField] float lockOnInterval = 0.2f;   //ロックオン間隔
    [SerializeField] float searchRadius = 10f; // サーチ半径


    // 一番近い敵をサーチして弾を発射する関数
    public void FindClosestEnemyAndFire()
    {
        GameObject closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            FireLaserAtTarget(closestEnemy);
        }
        else
        {
            FireLaserAtRandomPosition(); // ターゲットがない場合はランダム位置に発射
        }
    }

    // ステージ内のすべての敵をサーチして弾を発射する関数
    public void FindAllEnemiesAndFire()
    {
        lockedTargets.Clear();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (GameObject enemy in enemies)
        {
            lockedTargets.Add(enemy);
        }

        FireAtTarget(20);
    }

    private void FireAtTarget(int bulletsCount)
    {
        // 敵の数を取得
        int enemyCount = lockedTargets.Count;

        // 敵がいない場合はランダムな位置に発射する(Mathf.CeilToIntのエラー防止のため先実行)
        if (enemyCount == 0)
        {
            FireMultipleLasersAtRandomPosition(bulletsCount); // 20発発射する
            return;
        }

        // 弾丸を敵の数に応じて分割して発射
        int bulletsPerEnemy = Mathf.CeilToInt(bulletsCount / enemyCount);
        int totalBulletsFired = 0;

        foreach (GameObject target in lockedTargets)
        {
            // 各敵に対して `bulletsPerEnemy` 発の弾を発射
            for (int i = 0; i < bulletsPerEnemy; i++)
            {

                FireLaserAtTarget(target);
                totalBulletsFired++;

                // 20発を超えた場合は発射を終了する
                if (totalBulletsFired >= bulletsCount)
                    return;
            }
        }
    }

    // 一番近い敵を見つける関数
    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject closestEnemy = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = (enemy.transform.position - currentPosition).sqrMagnitude;
            if (distanceToEnemy < minDistance)
            {
                closestEnemy = enemy;
                minDistance = distanceToEnemy;
            }
        }

        return closestEnemy;
    }

    // ターゲットに向けて弾を発射する関数
    void FireLaserAtTarget(GameObject target)
    {
        GameObject laser = Instantiate(laserPrefab, laserSpawner.transform.position, laserSpawner.transform.rotation);
        LaserFire laserScript = laser.GetComponent<LaserFire>();
        laserScript.SetTarget(target.transform);
    }
    // ランダムな位置にレーザーを発射する関数
    void FireLaserAtRandomPosition()
    {
        GameObject laser = Instantiate(laserPrefab, laserSpawner.transform.position, laserSpawner.transform.rotation);
        LaserFire laserScript = laser.GetComponent<LaserFire>();
        laserScript.SetTarget(null); // ターゲットはnullに設定することでランダムな位置に向かう
    }

    // ランダムな位置に複数のレーザーを発射する関数
    void FireMultipleLasersAtRandomPosition(int numLasers)
    {
        for (int i = 0; i < numLasers; i++)
        {
            FireLaserAtRandomPosition();
        }
    }



    // 7.15 Pysics.OverlapSphereを用いた球体コライダー内の敵を全てLockOnするスクリプト
    public IEnumerator StartLockOn()
    {
        isLockingOn = true;             // ロックオンフラグをtrue
        lockedTargets.Clear();          // ロックオンリストをクリア

        int enemyLayerMask = LayerMask.GetMask("Enemy"); // 敵のレイヤーを取得
        // 現在のオブジェクトの位置を中心に、半径searchRadiusの球体を描く。この範囲内にあるenemyLaerColliderを取得。
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, searchRadius, enemyLayerMask);

        // 取得したすべてのColliderに対してループを実行。
        foreach (var hitCollider in hitColliders)
        {
            // Colliderが敵のタグを持っているか、かつすでにlockedTargetsリストに含まれていない場合。
            if (hitCollider.CompareTag(enemyTag) && !lockedTargets.Contains(hitCollider.gameObject))
            {
                // lockedTargetsリストに敵を追加。
                lockedTargets.Add(hitCollider.gameObject);

                // ロックオンした敵の情報をデバッグログに出力
                Debug.Log("Locked on to enemy: " + hitCollider.gameObject.name);
            }

            yield return new WaitForSeconds(lockOnInterval);    //ロックオン間隔待機

            // isLockingOnがfalseになったらループを抜け、発射する
            if (!isLockingOn)
            {
                Debug.Log("shot!");
                FireAtTarget((lockedTargets.Count) * 3);
                yield break;
            }
        }
    }

    //7.15 isLockingOn停止＆発射用
    public void StopLockOn()
    {
        isLockingOn = false;
    }


}