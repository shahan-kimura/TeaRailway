using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFire : MonoBehaviour
{
    Vector3 acceleration;
    Vector3 velocity;
    Vector3 position;       
    Vector3 randomPos;      //ターゲットが存在しない時の空撃ち用position
    Transform target;       //レーザー対象

    [SerializeField] float period =1f;

    GameObject seatchNearObject;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        seatchNearObject = FindClosestEnemy();

        if (seatchNearObject != null)
        {
            target = seatchNearObject.transform;
        }

        randomPos = new Vector3(Camera.main.transform.position.x + 10f,
                                Random.Range(0f,20f), 0);

        velocity = new Vector3(Random.Range(-5.0f, -2.5f), Random.Range(-6.0f, 6.0f), Random.Range(-6.0f, 6.0f)); 
    }





    void Update()
    {
        acceleration = Vector3.zero;        //初期加速度は0

        //Enemyが取得できなかった際は、randomPosを利用するように
        if (seatchNearObject != null)
        {
            Vector3 diff = target.position - position;
            acceleration += (diff - velocity * period) * 2f / (period * period);
        }
        else
        {
            Vector3 diff = randomPos - position;
            acceleration += (diff - velocity * period) * 2f / (period * period);
        }

        //運動方程式：t秒間に進む距離(diff) = (初速度(v) * t) ＋ (1/2 *加速度(a) * t^2)
        //変形すると
        //運動方程式：加速度(a) = 2*(diff - (v * t)) / t^2 
        //なので、「速度vの物体がt秒後にdiff進むための加速度a」が算出できる
        //GameObjectのvは取得できるし、tも取得できる
        //なら、レーザーがperiod秒後に着弾（diffが0）するために必要なaが算出できるよね！


        period -= Time.deltaTime;
        if(period < 0f)
        {
            Destroy(gameObject);
            return;
        }

        velocity += acceleration * Time.deltaTime;      //現在速度→加速度*時間
        position += velocity * Time.deltaTime;          //現在位置→速度*時間
        transform.position = position;
    }

    //最も近い距離のEnemyを取得する関数
    public GameObject FindClosestEnemy()
    {

        // enemy TagをもつGameObjectを格納する配列を宣言、"G"ame"O"bject"s"の略でgos
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");

        //最も近い位置に存在するEnemyをClosestと名付ける
        GameObject closest = null;
        float distance = Mathf.Infinity;                //距離無限大を初期設定
        Vector3 position = transform.position;          //自身の位置を取得

        foreach (GameObject go in gos)                  //gos[go]配列の最初から最後までを、goという仮変数でforeachループ
        {
            Vector3 diff = go.transform.position - position;    //gos[go]とplayerとの差（ベクトル）
            float curDistance = diff.sqrMagnitude;              //diffベクトルをsqrMagnitudeで単純な距離に変換

            if (curDistance < distance)                         //現時点でdistanceより近ければ、暫定1位を入れ替え
            {
                closest = go;
                distance = curDistance;
            }
        }

        //最も近かったEnemyをリターン
        return closest;
    }


}
