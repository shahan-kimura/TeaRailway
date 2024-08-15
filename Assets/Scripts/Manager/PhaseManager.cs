using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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

    [SerializeField] public Phase CurrentPhase { get; private set; } // 現在のフェーズを管理するプロパティ
    [SerializeField] private EnemyGenerator enemyGenerator; // 敵の生成を管理する EnemyGenerator の参照

    public CinemachineVirtualCamera vCamSide; //サイドビューカメラ
    [SerializeField] private CinemachineVirtualCamera vCamTop;  //トップビュー
    [SerializeField] private CinemachineVirtualCamera vCamBack; //バックビュー

    private int currentEnemy = 0;


    private void Start()
    {
        // 初期フェーズを設定
        SetPhase(CurrentPhase);
    }

    /// <summary>
    /// フェーズを設定し、EnemyGenerator に新しいフェーズを通知するメソッド。
    /// </summary>
    /// <param name="newPhase">新しいフェーズ</param>
    public void SetPhase(Phase newPhase)
    {
        CurrentPhase = newPhase;
        // カメラの設定
        UpdateCamera(CurrentPhase);

        // EnemyGenerator に新しいフェーズを通知
        if (enemyGenerator != null)
        {
            enemyGenerator.UpdatePhase(CurrentPhase);
        }
        else
        {
            Debug.LogWarning("EnemyGenerator is not assigned in PhaseManager.");
        }

        // 追加のフェーズ変更処理があればここに追加
    }
    public void NextPhase()
    {
        CurrentPhase++;
        // カメラの設定
        UpdateCamera(CurrentPhase);

        Debug.Log("nextphase");

        // EnemyGenerator に新しいフェーズを通知
        if (enemyGenerator != null)
        {
            enemyGenerator.UpdatePhase(CurrentPhase);
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
        return CurrentPhase;
    }

    /// <summary>
    /// フェーズに応じてカメラを更新するメソッド。
    /// </summary>
    /// <param name="phase">現在のフェーズ</param>
    private void UpdateCamera(Phase phase)
    {
        switch (phase)
        {
            case Phase.Phase1:
                SetCamera(vCamSide);
                break;
            case Phase.Phase2:
                SetCamera(vCamTop);
                break;
            case Phase.Phase3:
            case Phase.Phase4:
                SetCamera(vCamBack);
                break;
            case Phase.Phase5:
                SetCamera(vCamSide);
                break;
        }
    }

    // 指定されたカメラをアクティブにするメソッド
    private void SetCamera(CinemachineVirtualCamera activeCamera)
    {
        // すべてのカメラの優先度を0に設定
        vCamSide.Priority = 0;
        vCamTop.Priority = 0;
        vCamBack.Priority = 0;

        // 指定されたカメラの優先度を10に設定
        activeCamera.Priority = 10;
    }

    public void PhaseEnemyCountSet(int phaseEnemy)
    {
        currentEnemy = phaseEnemy;
    } 
    public void EnemyDestroyCount()
    {
        currentEnemy--;

        if (currentEnemy <= 0)
        {
            NextPhase();
        }

    }
}
