using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed = 1f;
    public float JumpSpeed = 1f;

    void Move()
    {
        float xmove = Input.GetAxis("Horizontal");
        float zmove = 0f;

        if (Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.UpArrow))
        {
            zmove = 1f;
        }

        if (Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.DownArrow))
        {
            zmove = -1f;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }



        Vector3 temppos = transform.position;
        temppos.x += xmove * MoveSpeed * Time.deltaTime;
        temppos.z += zmove * MoveSpeed * Time.deltaTime;

        transform.position = temppos;
    }
    public float ShotDelaySec = 3f;             // �÷��̾� ź �߻縦 ���� ���� ����
    protected float m_CurrentSec = 0;


    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time >= m_CurrentSec)
            {
                m_CurrentSec = Time.time + ShotDelaySec;

                Fire();
            }
        }

    }

    //public GameObject bullet;
    //public Transform Gun;
    public float BulletPower = 800f;
    public List<GameObject> BulletList = new List<GameObject>();
    private int BulletMaxCount = 50;            // �ִ� �Ѿ��� ������ ����
    private int BulletIndex = 0;                // ���� �߻�� �Ѿ� ������ ����
    public GameObject Bullet = null;
    public Transform Gun = null;

    void Fire()
    {
        //GameObject copyobj = GameObject.Instantiate(bullet);   // ������ ��� ����
        //copyobj.SetActive(true);
        //copyobj.transform.position = Gun.position;
        //Rigidbody body = copyobj.GetComponent<Rigidbody>();
        //body.useGravity = false;
        //body.AddForce(Gun.forward * BulletPower);
        if (BulletList[BulletIndex].gameObject.activeSelf)      // �����ؾ��� ź�� ���ư��� �ִ� ���ϰ�� ����
        {
            return;
        }

        BulletList[BulletIndex].transform.position = Gun.position;
        BulletList[BulletIndex].transform.rotation = Bullet.transform.rotation;
        BulletList[BulletIndex].gameObject.SetActive(true);
        // �Ѿ��� ��ġ�� ������ �����ϰ� Ȱ��ȭ


        Rigidbody body = BulletList[BulletIndex].GetComponent<Rigidbody>();
        body.velocity = new Vector3(0, 0, 0);
        body.angularVelocity = new Vector3(0, 0, 0);
        body.useGravity = false;
        body.AddForce(Gun.forward * BulletPower);
        // rigidbody�� ���� �ʱ�ȭ�ϰ� �ӵ� ����


        if (BulletIndex >= BulletMaxCount - 1)
        {
            BulletIndex = 0;
        }
        // ź�� Ǯ �������� �� ���� ���������� �ʱ�ȭ
        else
        {
            BulletIndex++;
        }
        // ź ��� Ƚ���� �ʱ�ȭ

    }
    private bool jumptype = false;
    void Jump()
    {
        float ymove = 1f;
        Vector3 jumppos = transform.position;
        while(true)
        {
            jumppos.y += ymove * JumpSpeed * Time.deltaTime;
            transform.position = jumppos;
            if (jumppos.y > 2)
            {
                jumptype = true;
                break;
            }
        }
        if (jumptype)
        {
            while(jumppos.y > 0)
            {
                jumppos.y -= ymove * JumpSpeed * Time.deltaTime;
                transform.position = jumppos;
                if (jumppos.y <= 0)
                {
                    break;
                }
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < BulletMaxCount; ++i)  // �̸� ź�� ������ ����
        {
            GameObject b = Instantiate<GameObject>(Bullet);

            b.gameObject.SetActive(false);

            BulletList.Add(b);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
    }
}
