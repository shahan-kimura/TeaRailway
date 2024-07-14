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

    private PlayerAttack playerAttack;          //攻撃用ScriptのplayerAttackを宣言

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントを事前に参照
        rigidbody = GetComponent<Rigidbody>();

        // PlayerAttack スクリプトを持つコンポーネントを取得
        playerAttack = GetComponent<PlayerAttack>();



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

        //クリック時サーチレーザー
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("on click");
            StartCoroutine(playerAttack.StartLockOn());
        }
        //クリック時サーチ終了
        if (Input.GetKeyUp(KeyCode.F))
        {
            playerAttack.StopLockOn();
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
}

