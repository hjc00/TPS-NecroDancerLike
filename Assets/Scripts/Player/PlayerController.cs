using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Transform musszleTrans;
    public GameObject bullet;

    private float preTime = 0;  //用来标记上一次按键的时间
    private float currentTime = 0;   //用来记录时间
    private bool firstPress;  //是否是第一次按键

    private Animator anim;

    private SkillsController SkillsController;
    private Rigidbody rb;

    private void Start()
    {
        anim = GetComponent<Animator>();


        SkillsController = GetComponent<SkillsController>();
        rb = GetComponent<Rigidbody>();

        firstPress = true;
    }

    private void FixedUpdate()
    {
        Lookat();
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        Move();
        Attack();
        ReleaseSkill();
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckTime())
            {
                Lookat();
                Instantiate(bullet, musszleTrans.position, musszleTrans.rotation);
            }
            else
            {
                //节奏不对
                UIController.Instance.ShowWaring();
            }
        }
    }

    void ReleaseSkill()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (CheckTime())
            {
                SkillsController.RealseSkills(1);
            }
            else
            {
                //节奏不对
                UIController.Instance.ShowWaring();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (CheckTime())
            {
                SkillsController.RealseSkills(2);
            }
            else
            {
                //节奏不对
                UIController.Instance.ShowWaring();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (CheckTime())
            {
                SkillsController.RealseSkills(3);
            }
            else
            {
                //节奏不对
                UIController.Instance.ShowWaring();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (CheckTime())
            {
                SkillsController.RealseSkills(4);
            }
            else
            {
                //节奏不对
                UIController.Instance.ShowWaring();
            }
        }
    }


    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (CheckTime())
            {
                Vector3 target = this.transform.position + new Vector3(0, 0, 1);
                if (CheckCanMove(target - this.transform.position))
                {
                    StartCoroutine(SmoothMove(this.transform.position, target));
                }
            }
            else
            {
                //节奏不对
                UIController.Instance.ShowWaring();
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (CheckTime())
            {
                Vector3 target = this.transform.position + new Vector3(0, 0, -1);
                if (CheckCanMove(target - this.transform.position))
                {
                    StartCoroutine(SmoothMove(this.transform.position, target));
                }
            }
            else
            {
                UIController.Instance.ShowWaring();

            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (CheckTime())
            {
                Vector3 target = this.transform.position + new Vector3(-1, 0, 0);
                if (CheckCanMove(target - this.transform.position))
                {
                    StartCoroutine(SmoothMove(this.transform.position, target));
                }
            }
            else
            {
                UIController.Instance.ShowWaring();

            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (CheckTime())
            {
                Vector3 target = this.transform.position + new Vector3(1, 0, 0);
                if (CheckCanMove(target - this.transform.position))
                {
                    StartCoroutine(SmoothMove(this.transform.position, target));
                }
            }
            else
            {
                UIController.Instance.ShowWaring();

            }
        }
    }   //控制移动

    void Lookat()  //控制朝向
    {

        Ray lookatRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(lookatRay, out hitInfo, 100, 1 << LayerMask.NameToLayer("Ground")))
        {
            Vector3 dir = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);

            if (Vector3.Distance(transform.position, dir) < 1)
            {
                return;
            }

            transform.LookAt(dir);
        }

    }

    bool CheckTime()   //判断该次按键是否合法，若合法则移动
    {
        if (firstPress)
        {
            firstPress = false;
            preTime = currentTime;  //记录当前按键的时间
            return true;
        }
        else
        {
            float timeInterval = currentTime - preTime;     //表示这次按键与上一次按键的时间间隔

            if (timeInterval <= MusicController.Instance.minCanMoveTime ||
               timeInterval >= MusicController.Instance.maxCanMoveTime)  //适合的时机
            {
                preTime = currentTime;
                // Debug.Log("miss beat:" + timeInterval);
                return false;
            }
            else
            {
                //Debug.Log(timeInterval);
                preTime = currentTime;
                return true;
            }
        }
    }

    bool CheckCanMove(Vector3 dir)   //判断是否能够进行移动
    {
        Ray ray = new Ray(this.transform.position, dir);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 1.5f))
        {
            if (hitInfo.transform.tag == "Ostabcle")
                return false;
            else return true;
        }
        else return true;
    }

    IEnumerator SmoothMove(Vector3 start, Vector3 target)
    {

        anim.SetTrigger("move");
        // while (Vector3.Distance(this.transform.position, target) > 0.001f)
        while (this.transform.position != target)
        {
            Lookat();
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
            yield return null;
        }

    }

    public void ResetCheckTimer()   //重置计时，用于某些简化操作
    {
        firstPress = false;
        currentTime = 0;
    }
}
