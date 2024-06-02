using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    RectTransform rectTransform; // UI要素のRectTransform
    Vector3 startPosition;       // スワイプ開始時の位置
    Vector3 lastMousePosition;   // 前フレームでのマウスの位置

    void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // RectTransformを取得
        startPosition = rectTransform.anchoredPosition3D; // 初期位置を保存（Vector3型）
        lastMousePosition = Input.mousePosition; // 前フレームのマウス位置を保存
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // マウスをクリックした場合はスワイプ開始
            startPosition = rectTransform.anchoredPosition3D;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            // ドラッグ中にUI要素を移動する
            Vector3 delta = Input.mousePosition - lastMousePosition;
            rectTransform.anchoredPosition3D += new Vector3(0, delta.y, 0);
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // マウスを離した場合は初期位置に戻る
            rectTransform.anchoredPosition3D = startPosition;
        }


        //
        if(rectTransform.anchoredPosition3D.y < -400) 
        {
            Debug.Log(rectTransform.anchoredPosition3D);
        }
        
    }
}
