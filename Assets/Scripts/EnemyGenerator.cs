using UnityEngine;
 
public class EnemyGenerator : MonoBehaviour
{
    //敵プレハブ
    [SerializeField] GameObject enemyPrefab;
    //プレイヤー位置
    [SerializeField] Transform player;

    //敵生成時間間隔
    [SerializeField] private  float interval;
    //経過時間
    private float time = 0f;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //時間計測
        time += Time.deltaTime;

        //経過時間が生成時間になったとき(生成時間より大きくなったとき)
        if (time > interval)
        {
            //enemyをインスタンス化する(生成する)
            GameObject enemy = Instantiate(enemyPrefab);
            //生成した敵の座標を決定する
            enemy.transform.position = 
                new Vector3(player.transform.position.x -15f,
                            1,
                            Random.Range(-2f,-10f));
            //経過時間を初期化して再度時間計測を始める
            time = 0f;
        }
    }
}