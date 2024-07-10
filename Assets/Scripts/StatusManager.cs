using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    [SerializeField] GameObject MainObject; //このスクリプトをアタッチするオブジェクト
    [SerializeField] int HP =1;             //HP現在値
    [SerializeField] int MaxHP =1;          //いずれMaxHP利用する際に使用

    [SerializeField] GameObject destroyEffect;  //撃破エフェクト

    [SerializeField] string TagName;            //当たり判定となるタグ

    // Update is called once per frame
    void Update()
    {
        //HPが0以下なら、撃破エフェクトを生成してMainを破壊
        if (HP <= 0)
        {
            HP = 0;
            var effect = Instantiate(destroyEffect);
            effect.transform.position = transform.position;
            Destroy(effect,5);
            Destroy(MainObject);
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

}
