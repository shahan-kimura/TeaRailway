using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の生成を管理するクラス。
/// フェーズに応じて敵の出現位置や種類を制御する。
/// </summary>
public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints; // 敵の出現ポイント
    [SerializeField] private GameObject[] enemyPrefabs; // 敵のプレハブ
    [SerializeField] private float spawnInterval = 2f; // 敵の出現間隔
    [SerializeField] private int enemiesPerPhase = 5; // 各フェーズで出現する敵の数

    Transform spawnPoint;   //現在のspawnPoint
    GameObject enemyPrefab; //現在のenemyPrefab


    private PhaseManager.Phase currentPhase; // 現在のフェーズ

    private void Start()
    {
        spawnPoint = spawnPoints[(int)currentPhase];

        // 初期設定
        // ここでは特に処理を行わない
    }

    /// <summary>
    /// フェーズが更新された際に呼ばれるメソッド。
    /// フェーズに応じて敵の生成設定を変更する。
    /// </summary>
    /// <param name="newPhase">新しいフェーズ</param>
    public void UpdatePhase(PhaseManager.Phase newPhase)
    {
        currentPhase = newPhase;

        // フェーズに応じて敵の出現間隔や数を調整する
        switch (currentPhase)
        {
            case PhaseManager.Phase.Phase1:
                spawnInterval = 0.2f;
                enemiesPerPhase = 5;
                spawnPoint = spawnPoints[0];
                enemyPrefab = enemyPrefabs[0];
                break;
            case PhaseManager.Phase.Phase2:
                spawnInterval = 0.2f;
                enemiesPerPhase = 5;
                spawnPoint = spawnPoints[1];
                enemyPrefab = enemyPrefabs[1];
                break;
            case PhaseManager.Phase.Phase3:
                spawnInterval = 0.2f;
                enemiesPerPhase = 10;
                spawnPoint = spawnPoints[0];
                enemyPrefab = enemyPrefabs[0];
                break;
            case PhaseManager.Phase.Phase4:
                spawnInterval = 0.2f;
                enemiesPerPhase = 10;
                spawnPoint = spawnPoints[1];
                enemyPrefab = enemyPrefabs[1];
                break;
            case PhaseManager.Phase.Phase5:
                spawnInterval = 0.2f;
                enemiesPerPhase = 10;
                break;
        }

        // 新しいフェーズに応じて敵の生成を開始する
        StartCoroutine(SpawnEnemies());
    }

    /// <summary>
    /// 敵をフェーズごとにスポーンさせるコルーチン。
    /// </summary>
    private IEnumerator SpawnEnemies()
    {
        // 現在のフェーズに応じた敵の数を決定
        int enemiesToSpawn = enemiesPerPhase;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // 敵をスポーンポイントにスポーンさせる
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            // 指定された間隔で次の敵をスポーンさせる
            yield return new WaitForSeconds(spawnInterval);
        }

        // フェーズ終了後の処理があればここに追加
    }
}


//using UnityEngine;

//public class EnemyGenerator : MonoBehaviour
//{
//    //敵プレハブ
//    [SerializeField] GameObject enemyPrefab;
//    //プレイヤー位置
//    [SerializeField] Transform player;

//    //敵生成時間間隔
//    [SerializeField] private  float interval;
//    //経過時間
//    private float time = 0f;

//    private void Start()
//    {
//        player = GameObject.Find("Player").GetComponent<Transform>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //時間計測
//        time += Time.deltaTime;

//        //経過時間が生成時間になったとき(生成時間より大きくなったとき)
//        if (time > interval)
//        {
//            //enemyをインスタンス化する(生成する)
//            GameObject enemy = Instantiate(enemyPrefab);
//            //生成した敵の座標を決定する
//            enemy.transform.position = 
//                new Vector3(player.transform.position.x -15f,
//                            1,
//                            Random.Range(-2f,-10f));
//            //経過時間を初期化して再度時間計測を始める
//            time = 0f;
//        }
//    }
//}