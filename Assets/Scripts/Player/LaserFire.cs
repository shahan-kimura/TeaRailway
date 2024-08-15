using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFire : MonoBehaviour
{
    Vector3 acceleration;
    Vector3 velocity;
    Vector3 position;
    Transform target; // レーザー対象
    Transform beforTarget;


    [SerializeField][Tooltip("着弾時間")] float period = 1f;                  
    [SerializeField][Tooltip("着弾時差")] float deltaPeriod = 0.5f;
    [SerializeField][Tooltip("x軸初速")] float x_initial_v = 10f; // X軸方向の初速度
    [SerializeField][Tooltip("y軸初速")] float y_initial_v = 10f; // Y軸方向の初速度
    [SerializeField][Tooltip("z軸初速")] float z_initial_v = 10f; // Z軸方向の初速度
    Vector3 randomPos; // ターゲットが存在しない時の空撃ち用position

    [SerializeField][Tooltip("着弾パーティクル")] GameObject explosionPrefab;
    GameObject explosionInstance; // 爆発エフェクトのインスタンス

    bool exploded = false;  // 爆発エフェクトが再生されたかどうかのフラグ

    Player player;                  //player

    bool previousIsDecoy = false; // 前回のisDecoy状態

    // レーザーのターゲットを設定する関数
    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    void Start()
    {
        position = transform.position;
        randomPos = new Vector3(Camera.main.transform.position.x + 20f,
                                Random.Range(0f, 10f), Random.Range(0f, 5f));
        // レーザーの初期速度をランダムに設定、地面めり込み禁止でy軸のみ0以上に
        velocity = new Vector3(Random.Range(-x_initial_v, x_initial_v),
                                Random.Range(0, y_initial_v),
                                Random.Range(-z_initial_v, z_initial_v));

        period += Random.Range(-deltaPeriod, deltaPeriod);

        //デコイ判定のためにpublic bool isDecoyを観測するため
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        DecoyCheck();
        HomingSequence();
    }

    private void HomingSequence()
    {
        acceleration = Vector3.zero; // 初期加速度は0

        // ターゲットが存在しない場合にはrandomPosを利用
        Vector3 diff;
        if (target != null)
        {
            diff = target.position - position;
        }
        else
        {
            diff = randomPos - position;
        }

        //運動方程式：t秒間に進む距離(diff) = (初速度(v) * t) ＋ (1/2 *加速度(a) * t^2)
        //変形すると
        //運動方程式：加速度(a) = 2*(diff - (v * t)) / t^2 
        //なので、「速度vの物体がt秒後にdiff進むための加速度a」が算出できる
        //GameObjectのvは取得できるし、tも取得できる
        //なら、レーザーがperiod秒後に着弾（diffが0）するために必要なaが算出できるよね！

        acceleration += (diff - velocity * period) * 2f / (period * period);

        period -= Time.deltaTime;
        if (period < 0f && !exploded)
        {
            TriggerExplosion(); // 着弾時に爆発エフェクトを再生
            Destroy(gameObject);
            return;
        }

        velocity += acceleration * Time.deltaTime; // 現在速度→加速度*時間
        position += velocity * Time.deltaTime; // 現在位置→速度*時間
        transform.position = position;
    }

    // 爆発エフェクトを再生する関数
    void TriggerExplosion()
    {
        // 爆発エフェクトをインスタンス化して、位置を設定
        explosionInstance = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        exploded = true; // 爆発が再生されたことをフラグで管理

        // 爆発エフェクトが再生した後に、自動的にこのGameObjectを破棄
        Destroy(explosionInstance, explosionPrefab.GetComponent<ParticleSystem>().main.duration);
    }

    void DecoyCheck()
    {
        // 現在のisDecoy状態が前回の状態と異なる場合に処理を実行する
        // 状態が変わった瞬間だけ処理を行いたいので、現在の状態と前回の状態を比較
        if (player.isDecoy != previousIsDecoy)
        {
            // プレイヤーがデコイの状態である（isDecoyがtrue）場合
            if (player.isDecoy)
            {
                // "Decoy"タグを持つオブジェクトを探し、そのTransformを取得
                Transform currentDecoy = GameObject.FindWithTag("Decoy").GetComponent<Transform>();

                // デコイが存在する場合に実行
                if (currentDecoy != null)
                {
                    // 現在のターゲットを保存し、新しいターゲットをデコイに設定
                    beforTarget = target;
                    target = currentDecoy;
                    Debug.Log(target.gameObject.name); // デバッグ用: 現在のターゲットの名前を表示
                }
            }
            // プレイヤーがデコイの状態でない場合（isDecoyがfalse）で、前回のターゲットが存在する場合
            else if (beforTarget != null)
            {
                // ターゲットを元のターゲットに戻し、前回のターゲットをクリア
                target = beforTarget;
                beforTarget = null;
                Debug.Log(target.gameObject.name); // デバッグ用: 現在のターゲットの名前を表示
            }

            // 現在の状態を前回の状態として記録する
            previousIsDecoy = player.isDecoy;
        }
    }
}
