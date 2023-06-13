using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bird : MonoBehaviour
{
    private bool isClick = false;
    
    public Transform rightPos;
    public Transform leftPos;
    public Transform BirdPos;
    public float maxDis = 2; // 弹弓最大距离
    [HideInInspector]
    public SpringJoint2D sp;
    protected Rigidbody2D rg;

    public LineRenderer right;
    public LineRenderer left;

    public GameObject Boom;

    private bool NextTime = false; // 下一只判定

    private TestMyTrail myTrail; // 拖尾
    [HideInInspector]
    public bool canMove = false;
    [HideInInspector]
    public bool isReleased = false;

    public float Amooth = 3; // 相机移动平滑值

    public AudioClip Select, birdFly, birdCollision;
    private bool isCollision = false; // 飞行时触发

    public bool isFly = false;

    public Sprite hurtPic;
    public Sprite flyPic;
    public Sprite skillPic;
    private SpriteRenderer render;







    void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestMyTrail>();
        render = GetComponent<SpriteRenderer>();
    }

    
    // Start is called before the first frame update
    
    void Start()
    {
        rg.isKinematic = true;

    }


    private void OnMouseDown()
    {
        if (canMove)
        {
            AudioPlay(Select);

            // rg.isKinematic = true;
            isClick = true;

        }
    }

    private void OnMouseUp()
    {

        if (canMove)
        {

            isClick = false;
            rg.isKinematic = false;
            Invoke("Fly", 0.1f);

            // 划线禁用
            right.enabled = false;
            left.enabled = false;

            canMove = false;
        }

    }
    

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        BirdFollow(); // 鼠标按下时小鸟跟随

        BirdDead(); // 发射后死亡

        CameraFollow(); // 相机跟随小鸟

        if (isFly)
        {
            if (Input.GetMouseButtonDown(0))
            {
                showSkill();
            }
        }
    }

    // 鼠标按下小鸟跟随
    void BirdFollow()
    {
        if (isClick)
        { 
            
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);
            // transform.position = Input.mousePosition;

            if (Vector3.Distance(transform.position, rightPos.position) > maxDis)
            { // 位置限定
                Vector3 pos = (transform.position - rightPos.position).normalized; // 单位化向量
                pos *= maxDis;
                transform.position = pos + rightPos.position;
            }
            Line();
        }
    }

    // 相机跟随小鸟
    void CameraFollow()
    {
        float posX = transform.position.x;
        Vector3 afterPos = new Vector3(Mathf.Clamp(posX,0,20), Camera.main.transform.position.y, Camera.main.transform.position.z);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, afterPos, Amooth);
    }

    // 小鸟发射后死亡
    void BirdDead()
    {
        // Debug.Log("x_speed:" + Mathf.Abs(rg.velocity.x) + "  y_speed:" + Mathf.Abs(rg.velocity.y));
        // Debug.Log(Mathf.Abs(transform.position.y));
        if (NextTime && Mathf.Abs(rg.velocity.x) < 0.01 && Mathf.Abs(rg.velocity.y) < 0.01)
        {
            // 速度死亡
            Next();
            NextTime = false;
        }
        else if (Mathf.Abs(transform.position.y) > 10)
        {
            // 位置死亡
            Next();
            NextTime = false;
        }
    }

    void Fly() // 弹射
    {
        isReleased = true;
        isFly = true;
        isCollision = true;
        render.sprite = flyPic;
        AudioPlay(birdFly); // 飞行时音乐

        myTrail.StartTrails(); //飞出时开启拖尾

        sp.enabled = false;
        NextTime = true;
        // Invoke("Next", 5);
    }

    void Line() // 划线
    {
        // 划线启用
        right.enabled = true;
        left.enabled = true;

        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, BirdPos.position);

        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, BirdPos.position);
    }

    void Next() // 下一只小鸟飞出
    {
        GameManager._instance.Birds.Remove(this);
        Destroy(gameObject);
        Instantiate(Boom, transform.position, Quaternion.identity);
        GameManager._instance.NextBird();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (isCollision)
        {
            AudioPlay(birdCollision); // 小鸟碰撞音乐
            isCollision = false; // 只响一次
            render.sprite = hurtPic;
        }
        isFly = false;
        myTrail.ClearTrails(); // 发生碰撞时关闭拖尾
    }


    // 播放音乐
    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    // 小鸟技能
    public virtual void showSkill()
    {
        isFly = false;
        render.sprite = skillPic;
    }
}
