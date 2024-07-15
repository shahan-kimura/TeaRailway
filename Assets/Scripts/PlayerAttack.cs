using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject laserPrefab;    //���[�U�[prefab�̃A�^�b�`���K�v
    [SerializeField] GameObject laserSpawner;   //���[�U�[���˒n�_�̎q�I�u�W�F�N�g
    [SerializeField] string enemyTag = "Enemy"; // �G�̃^�O

    [SerializeField] private List<GameObject> lockedTargets = new List<GameObject>();

    [SerializeField] private bool isLockingOn = false;               //���b�N�I�������p������p��bool
    [SerializeField] float lockOnInterval = 0.2f;   //���b�N�I���Ԋu
    [SerializeField] float searchRadius = 10f; // �T�[�`���a


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

        FireAtTarget(20);
    }

    private void FireAtTarget(int bulletsCount)
    {
        Debug.Log("FireAtTarget");
        // �G�̐����擾
        int enemyCount = lockedTargets.Count;

        // �G�����Ȃ��ꍇ�̓����_���Ȉʒu�ɔ��˂���(Mathf.CeilToInt�̃G���[�h�~�̂��ߐ���s)
        if (enemyCount == 0)
        {
            FireMultipleLasersAtRandomPosition(bulletsCount); // �������̔��˂��s��
            return;
        }

        // �e�ۂ�G�̐��ɉ����ĕ������Ĕ���
        int bulletsPerEnemy = Mathf.CeilToInt(bulletsCount / enemyCount);
        int totalBulletsFired = 0;

        foreach (GameObject target in lockedTargets)
        {
            // �e�G�ɑ΂��� `bulletsPerEnemy` ���̒e�𔭎�
            for (int i = 0; i < bulletsPerEnemy; i++)
            {
                Debug.Log("FireLaserAtTarget");
                FireLaserAtTarget(target);
                totalBulletsFired++;

                // ���ː��𒴂����ꍇ�͔��˂��I������
                if (totalBulletsFired >= bulletsCount)
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
        // �^�[�Q�b�g��null�ł��邩�ǂ������m�F����
        if (target == null)
        {
            Debug.LogWarning("Trying to fire laser at a null target.");
            return;
        }

        GameObject laser = Instantiate(laserPrefab, laserSpawner.transform.position, laserSpawner.transform.rotation);
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



    // 7.15 Pysics.OverlapSphere��p�������̃R���C�_�[���̓G��S��LockOn����X�N���v�g
    public IEnumerator StartLockOn()
    {
        isLockingOn = true;             // ���b�N�I���t���O��true
        lockedTargets.Clear();          // ���b�N�I�����X�g���N���A


        int enemyLayerMask = LayerMask.GetMask("Enemy"); // �G�̃��C���[���擾
        // ���݂̃I�u�W�F�N�g�̈ʒu�𒆐S�ɁA���asearchRadius�̋��̂�`���B���͈͓̔��ɂ���enemyLaerCollider���擾�B
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, searchRadius, enemyLayerMask);
        Debug.Log("StartLockOn");

        // �擾�������ׂĂ�Collider�ɑ΂��ă��[�v�����s�B
        foreach (var hitCollider in hitColliders)
        {
            // Collider���G�̃^�O�������Ă��邩�A�����ł�lockedTargets���X�g�Ɋ܂܂�Ă��Ȃ��ꍇ�B
            if (hitCollider.CompareTag(enemyTag) && !lockedTargets.Contains(hitCollider.gameObject))
            {
                // lockedTargets���X�g�ɓG��ǉ��B
                lockedTargets.Add(hitCollider.gameObject);

                //hitCollider ���^�[�Q�b�g�̃R���|�[�l���g���Ăяo��
                //�^�[�Q�b�g�T�[�N���̕\�����߂����s
                hitCollider.gameObject.GetComponent<EnemyRoutine>().ActivateLockOn();
            }

            yield return new WaitForSeconds(lockOnInterval);    //���b�N�I���Ԋu�ҋ@

            // isLockingOn��false�ɂȂ����烋�[�v�𔲂��A���˂���
            if (!isLockingOn)
            {
                Debug.Log("shot!");
                FireAtTarget((lockedTargets.Count) * 3);
                yield break;
            }
        }

        // ���ׂẴ��b�N�I�������������ꍇ�AisLockingOn��false�ɂȂ�܂őҋ@
        while (isLockingOn)
        {
            yield return null;
        }

        // isLockingOn��false�ɂȂ����u�Ԃɔ���
        Debug.Log("shot!");
        FireAtTarget((lockedTargets.Count) * 3);
    }

    //7.15 isLockingOn��~�����˗p
    public void StopLockOn()
    {
        isLockingOn = false;
    }

    // �M�Y�����g�p���ăT�[�`�͈͂����o��
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }


}