using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoutine : MonoBehaviour
{
    [SerializeField] GameObject lockOnCircle;   // ロックオンサークルのオブジェクト
    [SerializeField] string TagName;            //当たり判定となるタグ
    bool isLockOn = false;                      //ロックオン判定用bool


    void Start()
    {
        // ロックオンサークルを子オブジェクトから検索する（例として子オブジェクトの名前を "LockOnCircle" とする）
        lockOnCircle = transform.Find("LockOnCircle").gameObject;

        // 初期状態では非表示にする
        lockOnCircle.SetActive(false);
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
