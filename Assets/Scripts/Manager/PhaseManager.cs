using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームのフェーズを管理するクラス。
/// フェーズが変わることで、敵の生成やその他のゲーム要素が更新される。
/// </summary>
public class PhaseManager : MonoBehaviour
{
    public enum Phase
    {
        Phase1,
        Phase2,
        Phase3,
        Phase4,
        Phase5,
    }

    [SerializeField] private Phase currentPhase; // 現在のフェーズを管理する変数
    [SerializeField] private EnemyGenerator enemyGenerator; // 敵の生成を管理する EnemyGenerator の参照

    private void Start()
    {
        // 初期フェーズを設定
        SetPhase(currentPhase);
    }

    /// <summary>
    /// フェーズを設定し、EnemyGenerator に新しいフェーズを通知するメソッド。
    /// </summary>
    /// <param name="newPhase">新しいフェーズ</param>
    public void SetPhase(Phase newPhase)
    {
        currentPhase = newPhase;

        // EnemyGenerator に新しいフェーズを通知
        if (enemyGenerator != null)
        {
            enemyGenerator.UpdatePhase(currentPhase);
        }
        else
        {
            Debug.LogWarning("EnemyGenerator is not assigned in PhaseManager.");
        }

        // 追加のフェーズ変更処理があればここに追加
    }
    public void NextPhase()
    {
        currentPhase++;;

        Debug.Log("nextphase");

        // EnemyGenerator に新しいフェーズを通知
        if (enemyGenerator != null)
        {
            enemyGenerator.UpdatePhase(currentPhase);
        }
        else
        {
            Debug.LogWarning("EnemyGenerator is not assigned in PhaseManager.");
        }

        // 追加のフェーズ変更処理があればここに追加
    }

    /// <summary>
    /// 現在のフェーズを取得するメソッド。
    /// </summary>
    /// <returns>現在のフェーズ</returns>
    public Phase GetCurrentPhase()
    {
        return currentPhase;
    }
}
