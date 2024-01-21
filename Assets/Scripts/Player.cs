using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float hp = 5;
    public float MAX_HP = 5;
    public float attack;

    public bool noDamage;

    public static Player Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }


    public float speed;
    private Rigidbody rb;
    public AudioSource itemSound;
    public bool isJump;
    public VariableJoystick variableJoystick;

    public int score;

    public int leftItem;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI leftText;
    public GameObject endImage;

    public GameObject reGamePoint;

    public Animator anime;

    void Start()
    {
        //같은 Inspector 내에서 Rigidbody를 가져오겠다는 뜻
        rb = GetComponent<Rigidbody>();
        leftItem = 9;
        anime = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // other 충돌된 물체
        if (other.tag == "Item")
        {
            other.gameObject.SetActive(false);
            itemSound.Play();
            score = score + 10;
        }

        if (other.tag == "JumpItem")
        {
            other.gameObject.SetActive(false);
            itemSound.Play();
            score = score + 20;
        }

        if (other.tag == "EnemyMonster")
        {
            if (!noDamage)
            {
                hp = hp - other.GetComponent<EnemyMonster>().damage;
                StartCoroutine("OnDamage");
            }
        }

        leftItem--;


        if (score >= 120)
        {
            endImage.SetActive(true);
        }

        scoreText.text = score + "/120";
        leftText.text = "Left Item : " + leftItem;

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            isJump = true;
            anime.SetBool("jumping", false);
        }

        if (other.gameObject.tag == "ReGame")
        {
            OnRegame();
        }
    }

    IEnumerator OnDamage()
    {
        noDamage = true;
        yield return new WaitForSeconds(1f);
        noDamage = false;
    }

    public void OnRegame()
    {
        this.transform.position = reGamePoint.transform.position;
    }

    //물리 사용을 위한 지속 호출
    private void FixedUpdate()
    {
        // // 수평 축의 입력을 받음 (LEFT,RIGHT)
        // float moveH = Input.GetAxis("Horizontal");

        // // 수직 축의 입력을 받음 (UP,DOWN)
        // float moveV = Input.GetAxis("Vertical");

        // // 플레이어 이동
        // Vector3 movement = new Vector3(moveH, 0, moveV);

        // // 플레이어 힘을 가함
        // rb.AddForce(movement * speed);


        // rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        transform.LookAt(transform.position + direction);
        transform.position += direction * speed * Time.deltaTime;
        anime.SetBool("isRun", direction != Vector3.zero);
        FreezeRotation();


        // if (Input.GetKey(KeyCode.Space) && isJump)
        // {
        //     rb.AddForce(Vector3.up * 3f, ForceMode.Impulse);
        //     isJump = false;
        // }
    }

    void FreezeRotation()
    {
        rb.angularVelocity = Vector3.zero;
    }

    public void OnClickJump()
    {
        if (isJump)
        {
            rb.AddForce(Vector3.up * 3f, ForceMode.Impulse);
            anime.SetTrigger("doJump");
            anime.SetBool("jumping", true);
            isJump = false;
        }
    }
}
