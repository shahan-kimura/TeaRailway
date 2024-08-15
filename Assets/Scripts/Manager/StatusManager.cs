using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    [SerializeField] GameObject MainObject; //このスクリプトをアタッチするオブジェクト
    [SerializeField] int hp  =1;             //hp現在値
    [SerializeField] int maxHP =1;          //いずれMaxhp利用する際に使用

    //hpのプロパティ化用宣言
    public int HP { get; private set; }
    public int MaxHP { get; private set; }

    [SerializeField] GameObject destroyEffect;  //撃破エフェクト

    [SerializeField] string TagName;            //当たり判定となるタグ

    private void Start()
    {
        //プロパティの数値初期化
        HP = hp;
        MaxHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        //hpが0以下なら、撃破エフェクトを生成してMainを破壊
        if (HP <= 0)
        {
            DestroyThisObject();
        }
    }

 

    private void OnTriggerEnter(Collider other)
    {
        //onTriggerEnterを弾丸と相互利用する際には
        //少なくともどちらか片方にRigidbodyを適用させること（無いとバグります）

        //当たり判定のタグネームと一致していればダメージ処理呼び出し
        if (other.CompareTag(TagName))
        {
            Debug.Log("Hit");
            Damage();

        }
    }
    private void Damage()
    {
        HP--;
    }

    private void DestroyThisObject()
    {
        HP = 0;
        var effect = Instantiate(destroyEffect);
        effect.transform.position = transform.position;
        DestroyTagCheck();
        Destroy(effect, 5);
        Destroy(MainObject);
    }

    private void DestroyTagCheck()
    {
        switch (gameObject.tag)
        {
            case "Enemy":
                //敵撃破判定をPhaseManagerへ通知
                PhaseManager phaseManager = FindObjectOfType<PhaseManager>();
                phaseManager.EnemyDestroyCount();
                break;

            //破壊されたタグがplayerならUIからGameOverを呼び出し
            case "Player":
                UIManager uiManager = FindObjectOfType<UIManager>();
                uiManager.GameOver();
                break;
            default:
                break;
        }

    }

}
