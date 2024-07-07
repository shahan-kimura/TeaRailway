using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject laserPrefab;    //レーザーprefabのアタッチが必要
    [SerializeField] GameObject laserSpawner;   //レーザー発射地点の子オブジェクト
    [SerializeField] float searchRadius = 10f; // サーチ半径
    [SerializeField] string enemyTag = "Enemy"; // 敵のタグ

    private List<GameObject> lockedTargets = new List<GameObject>();

 
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

        // 敵の数を取得
        int enemyCount = lockedTargets.Count;

        // 敵がいない場合はランダムな位置に発射する
        if (enemyCount == 0)
        {
            FireMultipleLasersAtRandomPosition(20); // 20発発射する
            return;
        }

        // 20発の弾丸を敵の数に応じて分割して発射
        int bulletsPerEnemy = Mathf.CeilToInt(20f / enemyCount);
        int totalBulletsFired = 0;

        foreach (GameObject target in lockedTargets)
        {
            // 各敵に対して `bulletsPerEnemy` 発の弾を発射
            for (int i = 0; i < bulletsPerEnemy; i++)
            {

                 FireLaserAtTarget(target);
                 totalBulletsFired++;

                // 20発を超えた場合は発射を終了する
                if (totalBulletsFired >= 20)
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
        GameObject laser = Instantiate(laserPrefab,laserSpawner.transform.position, laserSpawner.transform.rotation);
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
}
