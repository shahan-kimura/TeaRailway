using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    [SerializeField] GameObject MainObject; //���̃X�N���v�g���A�^�b�`����I�u�W�F�N�g
    [SerializeField] int hp  =1;             //hp���ݒl
    [SerializeField] int maxHP =1;          //������Maxhp���p����ۂɎg�p

    //hp�̃v���p�e�B���p�錾
    public int HP { get; private set; }
    public int MaxHP { get; private set; }

    [SerializeField] GameObject destroyEffect;  //���j�G�t�F�N�g

    [SerializeField] string TagName;            //�����蔻��ƂȂ�^�O

    private void Start()
    {
        //�v���p�e�B�̐��l������
        HP = hp;
        MaxHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        //hp��0�ȉ��Ȃ�A���j�G�t�F�N�g�𐶐�����Main��j��
        if (HP <= 0)
        {
            DestroyThisObject();
        }
    }

 

    private void OnTriggerEnter(Collider other)
    {
        //onTriggerEnter��e�ۂƑ��ݗ��p����ۂɂ�
        //���Ȃ��Ƃ��ǂ��炩�Е���Rigidbody��K�p�����邱�Ɓi�����ƃo�O��܂��j

        //�����蔻��̃^�O�l�[���ƈ�v���Ă���΃_���[�W�����Ăяo��
        if (other.CompareTag(TagName))
        {
            Debug.Log("Hit");
            Damage();

        }
    }
    private void Damage()
    {
        HP--;
    }

    private void DestroyThisObject()
    {
        HP = 0;
        var effect = Instantiate(destroyEffect);
        effect.transform.position = transform.position;
        DestroyTagCheck();
        Destroy(effect, 5);
        Destroy(MainObject);
    }

    private void DestroyTagCheck()
    {
        switch (gameObject.tag)
        {
            case "Enemy":
                //�G���j�����PhaseManager�֒ʒm
                PhaseManager phaseManager = FindObjectOfType<PhaseManager>();
                phaseManager.EnemyDestroyCount();
                break;

            //�j�󂳂ꂽ�^�O��player�Ȃ�UI����GameOver���Ăяo��
            case "Player":
                UIManager uiManager = FindObjectOfType<UIManager>();
                uiManager.GameOver();
                break;
            default:
                break;
        }

    }

}
