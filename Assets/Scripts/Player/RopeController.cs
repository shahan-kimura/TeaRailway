using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    RectTransform rectTransform; // UI要素のRectTransform
    Vector3 startPosition;       // スワイプ開始時の位置
    Vector3 lastMousePosition;   // 前フレームでのマウスの位置

    // SmokeControllerを持つGameObjectへの参照を保持するためのシリアライズフィールド
    [SerializeField]
    private GameObject smokeObject;
    // SmokeControllerコンポーネントへの参照
    private SmokeController smokeController;

    // 警笛中か判定する変数
    private bool isSmoking = false;
    //ドラッグ中かどうかを判定するフラグ
    private bool isDragging = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // RectTransformを取得
        startPosition = rectTransform.anchoredPosition3D; // 初期位置を保存（Vector3型）
        lastMousePosition = Input.mousePosition; // 前フレームのマウス位置を保存

        // smokeObjectがnullでない場合、そのオブジェクトからSmokeControllerコンポーネントを取得
        if (smokeObject != null)
        {
            smokeController = smokeObject.GetComponent<SmokeController>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // UI要素内のクリックのみドラッグを開始
            if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition, null)) // 修正: マウス位置がUI内にあるか判定
            {
                isDragging = true; // 修正: ドラッグ状態を管理するフラグを設定
                // マウスをクリックした場合はスワイプ開始
                startPosition = rectTransform.anchoredPosition3D;
                lastMousePosition = Input.mousePosition;
            }
        }

        if (isDragging && Input.GetMouseButton(0)) // ドラッグ状態のみUIを移動
        {
            // ドラッグ中にUI要素を移動する
            Vector3 delta = Input.mousePosition - lastMousePosition;
            rectTransform.anchoredPosition3D += new Vector3(0, delta.y, 0);
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false; // 修正: ドラッグ終了時にフラグをリセット
            // マウスを離した場合は初期位置に戻る
            rectTransform.anchoredPosition3D = startPosition;
        }


        //警笛が一定以下に下がっていたら動作
        WhistleAction();

    }

    private void WhistleAction()
    {
        if (rectTransform.anchoredPosition3D.y < -400)
        {
            smokeController.SmokeUp();
            Debug.Log(rectTransform.anchoredPosition3D);
            if (!isSmoking)
            {
                isSmoking = true;
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            isSmoking = false;
            smokeController.SmokeDown();
        }
    }
}
