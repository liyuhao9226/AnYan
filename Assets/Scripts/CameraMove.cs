using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed;//移动速度
    public float move_x;//X方向移动距离
    public float move_y;//Y方向移动距离
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)//判断是否为滑动
        {
            CameraFreeMove();
        }
    }

    public void CameraFreeMove()
    {
        speed = 0.2F;
        move_x = Input.GetAxis("Mouse X") * speed;//获取X轴
        move_y = Input.GetAxis("Mouse Y") * speed;//获取Y轴
        if(gameObject.transform.position.x >= -30 && gameObject.transform.position.x <= 30 &&
            gameObject.transform.position.y >= -30 && gameObject.transform.position.y <= 30)//规定活动范围
        {
            gameObject.transform.Translate(-move_x, -move_y, -10);//镜头移动
        }
        else//恢复位置
        {
            if (gameObject.transform.position.x < -30)//X轴恢复
            {
                gameObject.transform.position = new Vector3(-4, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            else if (gameObject.transform.position.x > 30)
            {
                gameObject.transform.position = new Vector3(4, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            if (gameObject.transform.position.y < -30)//Y轴恢复
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, -4, gameObject.transform.position.z);
            }
            else if (gameObject.transform.position.y > 30)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 4, gameObject.transform.position.z);
            }
        }
    }
}
