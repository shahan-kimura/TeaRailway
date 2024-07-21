using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyRoutine : MonoBehaviour
{
    [SerializeField] GameObject lockOnCircle;   // ロックオンサークルのオブジェクト
    [SerializeField] string TagName;            //当たり判定となるタグ
    bool isLockOn = false;                      //ロックオン判定用bool

    //レーザーでも使った追尾用フィールド変数群
    Vector3 acceleration;
    Vector3 velocity;
    Vector3 position;
    Transform target; // 今回はPlayerを追尾する

    float lifeTime = 0;
    [SerializeField] float amplitude = 2f;      // 振幅
    [SerializeField] float frequency = 1f;      // 周波数
    [SerializeField] float sinValue_x = 0f;      // Sin関数の値
    [SerializeField] float sinValue_y = 0f;      // Sin関数の値
    [SerializeField] float sinValue_z = 0f;      // Sin関数の値
    [SerializeField] float offsetPosX = -10f;          // X軸方向のオフセット
    [SerializeField] float offsetPosY = 5f;          // Y軸方向のオフセット
    [SerializeField] float offsetPosZ = 0f;          // Z軸方向のオフセット

    float cycleDelta;                            // 周期のずれ

    [SerializeField][Tooltip("到着時間")] float period = 1f;
    [SerializeField][Tooltip("到着時差")] float deltaPeriod = 0.5f;



    void Start()
    {
        // ロックオンサークルを子オブジェクトから検索して非表示にする
        lockOnCircle = transform.Find("LockOnCircle").gameObject;
        lockOnCircle.SetActive(false);

        //追尾用：初期位置と速度の設定
        position = transform.position;
        velocity = new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(-6.0f, 6.0f));
        period += Random.Range(-deltaPeriod, deltaPeriod);

        // 追尾対象の取得（今回はPlayerを想定）
        target = GameObject.Find("Player").GetComponent<Transform>();

        cycleDelta = Random.Range(-0.5f, 0.5f);
    }

    private void Update()
    {
        //運動方程式：t秒間に進む距離(diff) = (初速度(v) * t) ＋ (1/2 *加速度(a) * t^2)
        //変形すると
        //運動方程式：加速度(a) = 2*(diff - (v * t)) / t^2 
        //なので、「速度vの物体がt秒後にdiff進むための加速度a」が算出できる
        //GameObjectのvは取得できるし、tも取得できる
        //なら、Objectがperiod秒後に到着（diffが0）するために必要なaが算出できるよね！

        // 時間経過によるSin関数の値の更新
        lifeTime += Time.deltaTime;
        sinValue_x = amplitude * Mathf.Sin((lifeTime * frequency) + cycleDelta); // lifeTimeを使って全体の時間でSin関数を計算する
        sinValue_y = amplitude * Mathf.Sin((lifeTime * frequency) + cycleDelta); // lifeTimeを使って全体の時間でSin関数を計算する
        sinValue_z = amplitude *2f * Mathf.Sin((lifeTime *0.5f * frequency) + cycleDelta); // lifeTimeを使って全体の時間でSin関数を計算する

        acceleration = Vector3.zero; // 初期加速度は0

        // 追尾対象との距離を計算し、その距離に対する加速度を求める
        Vector3 diff = target.position - position;
        acceleration.y = 0f;
        acceleration.z = 0f;
        acceleration += (diff - velocity * period) * 2f / (period * period);

        // 残りの到着時間の更新
        period -= Time.deltaTime;

        // 到着時間が一定値以下になったら再設定する→ずっと追従する
        if (period < 0.2f) 
        {
            period += Time.deltaTime;
        }

        velocity += acceleration * Time.deltaTime;      // 現在速度→加速度*時間
        position += velocity * Time.deltaTime ;          // 現在位置→速度*時間

        //positionのx軸だけ更新、y,zは上記acceleration.y等で0指定
        //sinValueでplayer平行した動きを、deltaPosで敵prefab毎に目標地点のズレを生成
        transform.position = new Vector3(position.x + sinValue_x + offsetPosX,
                                         position.y + sinValue_y + offsetPosY, 
                                         position.z + sinValue_z + offsetPosZ );
    }



    // ロックオンを開始する
    public void LockOnCheck()
    {
        if (isLockOn)
        {
            lockOnCircle.SetActive(true);    // ロックオンサークルを表示する
        }
        else
        {
            lockOnCircle.SetActive(false);    // ロックオンサークルを非表示する
        }
    }

    // ロックオンを停止する
    public void ActivateLockOn()
    {
        isLockOn = true;
        LockOnCheck();      //boolの更新に合わせてセットでLockOnCheckをかける
    }

    private void OnTriggerEnter(Collider other)
    {
        //当たり判定のタグネームと一致していれば処理の呼び出し
        if (other.CompareTag(TagName))
        {
            isLockOn = false;
            LockOnCheck();
        }
    }
}
