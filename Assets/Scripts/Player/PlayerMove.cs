using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;
    //public float RotateSpeed = 2;//车子旋转速度
    //public float RotateGunSpeed=1;//枪口旋转速度
    public Rigidbody RD;
    private float shift=1;
    public float limitSpeed = 8;
    public float limitShitSpeed = 15;
    private Transform cameraGun;
    private float rotationY = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;//隐藏鼠标指针
        cameraGun = transform.Find("Gun/Camera");
    }
    private void FixedUpdate()
    {
        Move();
        PlayerRotate();
        ChangeDirection();
    }
    private void Move()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            shift = 1.5f;
        }
        else
        {
            shift = 1;
        }
        //没按shift加速时,速度不超过8码
        if((RD.velocity.x>=limitSpeed||RD.velocity.x<=-limitSpeed)&&shift==1)
        {
            RD.velocity = new Vector3(limitSpeed*Mathf.Abs(RD.velocity.x)/RD.velocity.x, RD.velocity.y, RD.velocity.z);
        }
        if ((RD.velocity.z >= limitSpeed||RD.velocity.z<=-limitSpeed) && shift == 1)
        {
            RD.velocity = new Vector3(RD.velocity.x, RD.velocity.y, limitSpeed*Mathf.Abs(RD.velocity.z)/RD.velocity.z);
        }
        //按住shift加速时速度不超过15码
        if ((RD.velocity.x >= limitShitSpeed || RD.velocity.x <= -limitShitSpeed) && shift == 1.5)
        {
            RD.velocity = new Vector3(limitShitSpeed * Mathf.Abs(RD.velocity.x) / RD.velocity.x, RD.velocity.y, RD.velocity.z);
        }
        if ((RD.velocity.z >= limitShitSpeed || RD.velocity.z <= -limitShitSpeed) && shift == 1.5)
        {
            RD.velocity = new Vector3(RD.velocity.x, RD.velocity.y, limitShitSpeed * Mathf.Abs(RD.velocity.z) / RD.velocity.z);
        }

        float speedx= Input.GetAxisRaw("Horizontal");
        float speedz = Input.GetAxisRaw("Vertical");
        RD.AddForce(transform.forward* moveSpeed*speedz*shift);
        RD.AddForce(transform.right* moveSpeed * speedx*shift);
        
    }
    private void PlayerRotate()
    {
        float xspeed = Input.GetAxis("Mouse X");
        float yspeed = Input.GetAxis("Mouse Y");
        Vector3 vector = new Vector3(-yspeed, 0, 0);
        transform.Rotate(new Vector3(0, xspeed * GameManager.instance.sensitive, 0), Space.World);//车子水平旋转
        rotationY += yspeed * GameManager.instance.sensitive;
        rotationY = Mathf.Clamp(rotationY, -20, 40);
        cameraGun.localEulerAngles = new Vector3(-rotationY, 0, 0);//枪口上下旋转,限制向下20度向上40度
    }
    private void ChangeDirection()
    {
        RaycastHit hit;
        int Rmask = LayerMask.GetMask("Environment");
        Vector3 Point_dir = transform.TransformDirection(Vector3.down);
        if (Physics.Raycast(transform.position, Point_dir, out hit, 50.0f, Rmask))
        {
            Quaternion NextRot = Quaternion.LookRotation(Vector3.Cross(hit.normal, Vector3.Cross(transform.forward, hit.normal)), hit.normal);
            transform.rotation = Quaternion.Lerp(transform.rotation, NextRot,0.2f);
        }
    }
   
}
