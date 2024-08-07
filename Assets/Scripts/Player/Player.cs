using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ユーザー入力を受け取ってプレイヤーを操作します。
public class Player : MonoBehaviour
{
    // 移動速度を指定します。
    [SerializeField]
    [Tooltip("移動速度を指定します。")]
    private float speed = 2;
    // ジャンプ力を指定します。
    [SerializeField]
    [Tooltip("ジャンプ力を指定します。")]
    private Vector2 jumpPower = new(0, 6);
    // 地面との交差判定用のチェッカーを指定します。
    //[SerializeField]
    //[Tooltip("地面との交差判定用のチェッカーを指定します。")]
    //private LineCaster2D groundChecker = null;

    // コンポーネントを参照しておく変数
    new Rigidbody rigidbody;

    // フェイズシフト用仮置き
    [SerializeField] PhaseManager phaseManager;

    private PlayerAttack playerAttack;          //攻撃用ScriptのplayerAttackを宣言

    public bool isDecoy = false;   // デコイ放出中かの判定

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントを事前に参照
        rigidbody = GetComponent<Rigidbody>();

        // PlayerAttack スクリプトを持つコンポーネントを取得
        playerAttack = GetComponent<PlayerAttack>();

        //フェイズシフト用仮置き
        phaseManager = GameObject.FindObjectOfType<PhaseManager>();


    }

    // 毎フレームに一度実行される更新処理です。
    void Update()
    {

        // ジャンプ
        if (Input.GetButtonDown("Jump"))
        {
            rigidbody.AddForce(jumpPower, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            playerAttack.FindClosestEnemyAndFire();
        }

        if (Input.GetKeyDown(KeyCode.V))  // Vが押されたら
        {
            playerAttack.FindAllEnemiesAndFire();
        }

        // クリック時サーチレーザー
        if (Input.GetMouseButtonDown(0)) // マウスの左ボタンが押された瞬間
        {
            StartCoroutine(playerAttack.StartLockOn());
        }

        // クリック時サーチ終了
        if (Input.GetMouseButtonUp(0)) // マウスの左ボタンが離された瞬間
        {
            playerAttack.StopLockOn();
        }

        //デコイ状態反転
        if (Input.GetKeyDown(KeyCode.P))  // Pが押されたら
        {
            isDecoy = !isDecoy;
        }

        // 等速度運動
        var velocity = rigidbody.velocity;
        velocity.x = speed;
        rigidbody.velocity = velocity;
        //}
    }

    //ゴール時速度取得のため、プロパティ式で保存
    public float CurrentSpeed
    {
        get { return rigidbody.velocity.magnitude; }
    }


    public void IsDecoy()
    {
        isDecoy = !isDecoy;
    }
}

