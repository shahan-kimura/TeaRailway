using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFire : MonoBehaviour
{
    Vector3 acceleration;
    Vector3 velocity;
    Vector3 position;       
    Vector3 randomPos;      //�^�[�Q�b�g�����݂��Ȃ����̋󌂂��pposition
    Transform target;       //���[�U�[�Ώ�

    [SerializeField] float period =1f;

    GameObject seatchNearObject;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        seatchNearObject = FindClosestEnemy();

        if (seatchNearObject != null)
        {
            target = seatchNearObject.transform;
        }

        randomPos = new Vector3(Camera.main.transform.position.x + 10f,
                                Random.Range(0f,20f), 0);

        velocity = new Vector3(Random.Range(-5.0f, -2.5f), Random.Range(-6.0f, 6.0f), Random.Range(-6.0f, 6.0f)); 
    }





    void Update()
    {
        acceleration = Vector3.zero;        //���������x��0

        //Enemy���擾�ł��Ȃ������ۂ́ArandomPos�𗘗p����悤��
        if (seatchNearObject != null)
        {
            Vector3 diff = target.position - position;
            acceleration += (diff - velocity * period) * 2f / (period * period);
        }
        else
        {
            Vector3 diff = randomPos - position;
            acceleration += (diff - velocity * period) * 2f / (period * period);
        }

        //�^���������Ft�b�Ԃɐi�ދ���(diff) = (�����x(v) * t) �{ (1/2 *�����x(a) * t^2)
        //�ό`�����
        //�^���������F�����x(a) = 2*(diff - (v * t)) / t^2 
        //�Ȃ̂ŁA�u���xv�̕��̂�t�b���diff�i�ނ��߂̉����xa�v���Z�o�ł���
        //GameObject��v�͎擾�ł��邵�At���擾�ł���
        //�Ȃ�A���[�U�[��period�b��ɒ��e�idiff��0�j���邽�߂ɕK�v��a���Z�o�ł����ˁI


        period -= Time.deltaTime;
        if(period < 0f)
        {
            Destroy(gameObject);
            return;
        }

        velocity += acceleration * Time.deltaTime;      //���ݑ��x�������x*����
        position += velocity * Time.deltaTime;          //���݈ʒu�����x*����
        transform.position = position;
    }

    //�ł��߂�������Enemy���擾����֐�
    public GameObject FindClosestEnemy()
    {

        // enemy Tag������GameObject���i�[����z���錾�A"G"ame"O"bject"s"�̗���gos
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");

        //�ł��߂��ʒu�ɑ��݂���Enemy��Closest�Ɩ��t����
        GameObject closest = null;
        float distance = Mathf.Infinity;                //����������������ݒ�
        Vector3 position = transform.position;          //���g�̈ʒu���擾

        foreach (GameObject go in gos)                  //gos[go]�z��̍ŏ�����Ō�܂ł��Ago�Ƃ������ϐ���foreach���[�v
        {
            Vector3 diff = go.transform.position - position;    //gos[go]��player�Ƃ̍��i�x�N�g���j
            float curDistance = diff.sqrMagnitude;              //diff�x�N�g����sqrMagnitude�ŒP���ȋ����ɕϊ�

            if (curDistance < distance)                         //�����_��distance���߂���΁A�b��1�ʂ����ւ�
            {
                closest = go;
                distance = curDistance;
            }
        }

        //�ł��߂�����Enemy�����^�[��
        return closest;
    }


}
