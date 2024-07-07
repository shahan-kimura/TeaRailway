using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject laserPrefab;    //���[�U�[prefab�̃A�^�b�`���K�v
    [SerializeField] GameObject laserSpawner;   //���[�U�[���˒n�_�̎q�I�u�W�F�N�g
    [SerializeField] float searchRadius = 10f; // �T�[�`���a
    [SerializeField] string enemyTag = "Enemy"; // �G�̃^�O

    private List<GameObject> lockedTargets = new List<GameObject>();

 
    // ��ԋ߂��G���T�[�`���Ēe�𔭎˂���֐�
    public void FindClosestEnemyAndFire()
    {
        GameObject closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            FireLaserAtTarget(closestEnemy);
        }
        else
        {
            FireLaserAtRandomPosition(); // �^�[�Q�b�g���Ȃ��ꍇ�̓����_���ʒu�ɔ���
        }
    }

    // �X�e�[�W���̂��ׂĂ̓G���T�[�`���Ēe�𔭎˂���֐�
    public void FindAllEnemiesAndFire()
    {
        lockedTargets.Clear();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (GameObject enemy in enemies)
        {
            lockedTargets.Add(enemy);
        }

        // �G�̐����擾
        int enemyCount = lockedTargets.Count;

        // �G�����Ȃ��ꍇ�̓����_���Ȉʒu�ɔ��˂���
        if (enemyCount == 0)
        {
            FireMultipleLasersAtRandomPosition(20); // 20�����˂���
            return;
        }

        // 20���̒e�ۂ�G�̐��ɉ����ĕ������Ĕ���
        int bulletsPerEnemy = Mathf.CeilToInt(20f / enemyCount);
        int totalBulletsFired = 0;

        foreach (GameObject target in lockedTargets)
        {
            // �e�G�ɑ΂��� `bulletsPerEnemy` ���̒e�𔭎�
            for (int i = 0; i < bulletsPerEnemy; i++)
            {

                 FireLaserAtTarget(target);
                 totalBulletsFired++;

                // 20���𒴂����ꍇ�͔��˂��I������
                if (totalBulletsFired >= 20)
                    return;
            }
        }
    }

    // ��ԋ߂��G��������֐�
    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject closestEnemy = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = (enemy.transform.position - currentPosition).sqrMagnitude;
            if (distanceToEnemy < minDistance)
            {
                closestEnemy = enemy;
                minDistance = distanceToEnemy;
            }
        }

        return closestEnemy;
    }

    // �^�[�Q�b�g�Ɍ����Ēe�𔭎˂���֐�
    void FireLaserAtTarget(GameObject target)
    {
        GameObject laser = Instantiate(laserPrefab,laserSpawner.transform.position, laserSpawner.transform.rotation);
        LaserFire laserScript = laser.GetComponent<LaserFire>();
        laserScript.SetTarget(target.transform);
    }
    // �����_���Ȉʒu�Ƀ��[�U�[�𔭎˂���֐�
    void FireLaserAtRandomPosition()
    {
        GameObject laser = Instantiate(laserPrefab, laserSpawner.transform.position, laserSpawner.transform.rotation);
        LaserFire laserScript = laser.GetComponent<LaserFire>();
        laserScript.SetTarget(null); // �^�[�Q�b�g��null�ɐݒ肷�邱�ƂŃ����_���Ȉʒu�Ɍ�����
    }

    // �����_���Ȉʒu�ɕ����̃��[�U�[�𔭎˂���֐�
    void FireMultipleLasersAtRandomPosition(int numLasers)
    {
        for (int i = 0; i < numLasers; i++)
        {
            FireLaserAtRandomPosition();
        }
    }
}
