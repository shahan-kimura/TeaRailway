using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFire : MonoBehaviour
{
    Vector3 acceleration;
    Vector3 velocity;
    Vector3 position;
    Transform target; // レーザー対象

    [SerializeField] float period = 1f;
    Vector3 randomPos; // ターゲットが存在しない時の空撃ち用position

    // レーザーのターゲットを設定する関数
    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    void Start()
    {
        position = transform.position;
        randomPos = new Vector3(Camera.main.transform.position.x + 10f,
                                Random.Range(0f, 20f), Random.Range(0f, 20f));
        velocity = new Vector3(Random.Range(-5.0f, -2.5f), Random.Range(-6.0f, 6.0f), Random.Range(-6.0f, 6.0f));

        period += Random.Range(-0.2f, 0.2f);
    }

    void Update()
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
        if (period < 0f)
        {
            Destroy(gameObject);
            return;
        }

        velocity += acceleration * Time.deltaTime; // 現在速度→加速度*時間
        position += velocity * Time.deltaTime; // 現在位置→速度*時間
        transform.position = position;
    }
}
