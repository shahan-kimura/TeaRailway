using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�ւ̎�U��ƓG���[�U�[�̔��ˊ֌W�̃N���X
/// </summary>
public class CannonHead : MonoBehaviour
{
    [SerializeField] Transform target;                      // ����͓G�p�Ȃ̂�Player��Start��Get

    [SerializeField, Header("�G���[�U�[Prefab")] 
    GameObject EnemyLaserPrefab;                            //�G���[�U�[��Prefab

    private float laserTimer = 0;
    [SerializeField, Header("�ˌ��C���^�[�o��")]
    private float attackInterval = 5f;
    [SerializeField, Header("�ˌ��C���^�[�o���̌̍�")]
    float fireDeltaPeriod = 5f;

    private void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        attackInterval += Random.Range(0, fireDeltaPeriod);     //�ˌ��C���^�[�o���Ɍ̗����ǉ�
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(target.transform);

        laserTimer += Time.deltaTime;
        if(laserTimer > attackInterval)
        {
            laserTimer = 0;
            EnemyLaserAttack();

        }
    }

    private void EnemyLaserAttack()
    {
        // �^�[�Q�b�g��null�ł��邩�ǂ������m�F����
        if (target == null)
        {
            Debug.LogWarning("Trying to fire laser at a null target.");
            return;
        }

        GameObject enemylaser = Instantiate(EnemyLaserPrefab, transform.position, transform.rotation);
        LaserFire enemylaserScript = enemylaser.GetComponent<LaserFire>();
        enemylaserScript.SetTarget(target.transform);
    }
}
