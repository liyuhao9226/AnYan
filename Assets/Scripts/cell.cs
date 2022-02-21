using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cell : MonoBehaviour
{
    //public int cell_type;//单元格种类:平原、高地、特殊设施（地窖/据点：敌方据点/我方据点）、障碍物

    //public bool movable;//可移动
    //public bool attackable;// 可攻击
    //public bool activable;//可激活
    //public bool dectable;//可侦测

    public float hit_up;//命中加成
    public float evade_up;//闪避加成
    public int move_waste;//移动行动点消耗
    public int attack_waste;//攻击行动点消耗
    private float x_difference;//X轴差值
    private float y_difference;//Y轴差值
    //public int activate_waste;//激活行动点消耗
    //public int detect_waste;//侦测行动点消耗

    public GameObject movecell;//移动单元
    public GameObject attackcell;//攻击单元

    public int playernumber;//位于单元格的玩家数量

    public GameManager GameManager;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        CloseCell();//初始化
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()//棋子移动
    {
        if(GameManager.selected.GetComponentInChildren<player>().isdetect == true)//判断是否探测
        {
            CellDetect();
            GameManager.selected.GetComponentInChildren<player>().isdetect = false;
            GameManager.selected.GetComponentInChildren<player>().actionpoint--;
        }
        else//进入移动
        {
            CellMove();
        }
    }

    public void CloseCell()//初始化cell
    {
        playernumber = 0;
        movecell.SetActive(false);
        attackcell.SetActive(false);
    }

    public void CellMove()//玩家移动
    {
        if (GameManager.selected != null && movecell.activeInHierarchy)
        {
            if (GameManager.selected.GetComponentInChildren<player>().actionpoint >= 1)//判断该单元格是否可移动
            {
                GameManager.selected.GetComponentInChildren<player>().Move(transform.parent.gameObject.transform.position.x,
                                                                           transform.parent.gameObject.transform.position.y);//移动
            }
            GameManager.CloseRange();
        }
    }

    public void CellDetect()//玩家探测
    {
        x_difference = transform.parent.gameObject.transform.position.x - GameManager.selected.transform.position.x;
        y_difference = transform.parent.gameObject.transform.position.y - GameManager.selected.transform.position.y;
        GameManager.SetFog();
        if (x_difference > 0 && Mathf.Abs(x_difference) > Mathf.Abs(y_difference))//朝向向左
        {
            Set1((int)GameManager.selected.transform.position.x + 4, (int)GameManager.selected.transform.position.y + 5 + 1);
            Set1((int)GameManager.selected.transform.position.x + 4, (int)GameManager.selected.transform.position.y + 5);
            Set1((int)GameManager.selected.transform.position.x + 4, (int)GameManager.selected.transform.position.y + 5 - 1);
            Set1((int)GameManager.selected.transform.position.x + 4 + 1, (int)GameManager.selected.transform.position.y + 5 + 1);
            Set1((int)GameManager.selected.transform.position.x + 4 + 1, (int)GameManager.selected.transform.position.y + 5);
            Set1((int)GameManager.selected.transform.position.x + 4 + 1, (int)GameManager.selected.transform.position.y + 5 - 1);
            Set1((int)GameManager.selected.transform.position.x + 4 + 2, (int)GameManager.selected.transform.position.y + 5 + 2);
            Set1((int)GameManager.selected.transform.position.x + 4 + 2, (int)GameManager.selected.transform.position.y + 5 + 1);
            Set1((int)GameManager.selected.transform.position.x + 4 + 2, (int)GameManager.selected.transform.position.y + 5);
            Set1((int)GameManager.selected.transform.position.x + 4 + 2, (int)GameManager.selected.transform.position.y + 5 - 1);
            Set1((int)GameManager.selected.transform.position.x + 4 + 2, (int)GameManager.selected.transform.position.y + 5 - 2);
            Set1((int)GameManager.selected.transform.position.x + 4 + 3, (int)GameManager.selected.transform.position.y + 5 + 3);
            Set1((int)GameManager.selected.transform.position.x + 4 + 3, (int)GameManager.selected.transform.position.y + 5 + 2);
            Set1((int)GameManager.selected.transform.position.x + 4 + 3, (int)GameManager.selected.transform.position.y + 5 + 1);
            Set1((int)GameManager.selected.transform.position.x + 4 + 3, (int)GameManager.selected.transform.position.y + 5);
            Set1((int)GameManager.selected.transform.position.x + 4 + 3, (int)GameManager.selected.transform.position.y + 5 - 1);
            Set1((int)GameManager.selected.transform.position.x + 4 + 3, (int)GameManager.selected.transform.position.y + 5 - 2);
            Set1((int)GameManager.selected.transform.position.x + 4 + 3, (int)GameManager.selected.transform.position.y + 5 - 3);
            Set1((int)GameManager.selected.transform.position.x + 4 + 4, (int)GameManager.selected.transform.position.y + 5 + 2);
            Set1((int)GameManager.selected.transform.position.x + 4 + 4, (int)GameManager.selected.transform.position.y + 5 + 1);
            Set1((int)GameManager.selected.transform.position.x + 4 + 4, (int)GameManager.selected.transform.position.y + 5);
            Set1((int)GameManager.selected.transform.position.x + 4 + 4, (int)GameManager.selected.transform.position.y + 5 - 1);
            Set1((int)GameManager.selected.transform.position.x + 4 + 4, (int)GameManager.selected.transform.position.y + 5 - 2);
            Set1((int)GameManager.selected.transform.position.x + 4 + 5, (int)GameManager.selected.transform.position.y + 5 + 1);
            Set1((int)GameManager.selected.transform.position.x + 4 + 5, (int)GameManager.selected.transform.position.y + 5);
            Set1((int)GameManager.selected.transform.position.x + 4 + 5, (int)GameManager.selected.transform.position.y + 5 - 1);
            Set1((int)GameManager.selected.transform.position.x + 4 + 6, (int)GameManager.selected.transform.position.y + 5);
        }
        else if(x_difference < 0 && Mathf.Abs(x_difference) > Mathf.Abs(y_difference))//朝向向右
        {
            Set1((int)GameManager.selected.transform.position.x + 4, (int)GameManager.selected.transform.position.y + 5 + 1);
            Set1((int)GameManager.selected.transform.position.x + 4, (int)GameManager.selected.transform.position.y + 5);
            Set1((int)GameManager.selected.transform.position.x + 4, (int)GameManager.selected.transform.position.y + 5 - 1);
            Set1((int)GameManager.selected.transform.position.x + 4 - 1, (int)GameManager.selected.transform.position.y + 5 + 1);
            Set1((int)GameManager.selected.transform.position.x + 4 - 1, (int)GameManager.selected.transform.position.y + 5);
            Set1((int)GameManager.selected.transform.position.x + 4 - 1, (int)GameManager.selected.transform.position.y + 5 - 1);
            Set1((int)GameManager.selected.transform.position.x + 4 - 2, (int)GameManager.selected.transform.position.y + 5 + 2);
            Set1((int)GameManager.selected.transform.position.x + 4 - 2, (int)GameManager.selected.transform.position.y + 5 + 1);
            Set1((int)GameManager.selected.transform.position.x + 4 - 2, (int)GameManager.selected.transform.position.y + 5);
            Set1((int)GameManager.selected.transform.position.x + 4 - 2, (int)GameManager.selected.transform.position.y + 5 - 1);
            Set1((int)GameManager.selected.transform.position.x + 4 - 2, (int)GameManager.selected.transform.position.y + 5 - 2);
            Set1((int)GameManager.selected.transform.position.x + 4 - 3, (int)GameManager.selected.transform.position.y + 5 + 3);
            Set1((int)GameManager.selected.transform.position.x + 4 - 3, (int)GameManager.selected.transform.position.y + 5 + 2);
            Set1((int)GameManager.selected.transform.position.x + 4 - 3, (int)GameManager.selected.transform.position.y + 5 + 1);
            Set1((int)GameManager.selected.transform.position.x + 4 - 3, (int)GameManager.selected.transform.position.y + 5);
            Set1((int)GameManager.selected.transform.position.x + 4 - 3, (int)GameManager.selected.transform.position.y + 5 - 1);
            Set1((int)GameManager.selected.transform.position.x + 4 - 3, (int)GameManager.selected.transform.position.y + 5 - 2);
            Set1((int)GameManager.selected.transform.position.x + 4 - 3, (int)GameManager.selected.transform.position.y + 5 - 3);
            Set1((int)GameManager.selected.transform.position.x + 4 - 4, (int)GameManager.selected.transform.position.y + 5 + 2);
            Set1((int)GameManager.selected.transform.position.x + 4 - 4, (int)GameManager.selected.transform.position.y + 5 + 1);
            Set1((int)GameManager.selected.transform.position.x + 4 - 4, (int)GameManager.selected.transform.position.y + 5);
            Set1((int)GameManager.selected.transform.position.x + 4 - 4, (int)GameManager.selected.transform.position.y + 5 - 1);
            Set1((int)GameManager.selected.transform.position.x + 4 - 4, (int)GameManager.selected.transform.position.y + 5 - 2);
            Set1((int)GameManager.selected.transform.position.x + 4 - 5, (int)GameManager.selected.transform.position.y + 5 + 1);
            Set1((int)GameManager.selected.transform.position.x + 4 - 5, (int)GameManager.selected.transform.position.y + 5);
            Set1((int)GameManager.selected.transform.position.x + 4 - 5, (int)GameManager.selected.transform.position.y + 5 - 1);
            Set1((int)GameManager.selected.transform.position.x + 4 - 6, (int)GameManager.selected.transform.position.y + 5);
        }
        else if(y_difference < 0 && Mathf.Abs(y_difference) > Mathf.Abs(x_difference))//朝向向下
        {
            Set1((int)GameManager.selected.transform.position.x + 4 + 1, (int)GameManager.selected.transform.position.y + 5);
            Set1((int)GameManager.selected.transform.position.x + 4, (int)GameManager.selected.transform.position.y + 5);
            Set1((int)GameManager.selected.transform.position.x + 4 - 1, (int)GameManager.selected.transform.position.y + 5);
            Set1((int)GameManager.selected.transform.position.x + 4 + 1, (int)GameManager.selected.transform.position.y + 5 - 1);
            Set1((int)GameManager.selected.transform.position.x + 4, (int)GameManager.selected.transform.position.y + 5 - 1);
            Set1((int)GameManager.selected.transform.position.x + 4 - 1, (int)GameManager.selected.transform.position.y + 5 - 1);
            Set1((int)GameManager.selected.transform.position.x + 4 + 2, (int)GameManager.selected.transform.position.y + 5 - 2);
            Set1((int)GameManager.selected.transform.position.x + 4 + 1, (int)GameManager.selected.transform.position.y + 5 - 2);
            Set1((int)GameManager.selected.transform.position.x + 4, (int)GameManager.selected.transform.position.y + 5 - 2);
            Set1((int)GameManager.selected.transform.position.x + 4 - 1, (int)GameManager.selected.transform.position.y + 5 - 2);
            Set1((int)GameManager.selected.transform.position.x + 4 - 2, (int)GameManager.selected.transform.position.y + 5 - 2);
            Set1((int)GameManager.selected.transform.position.x + 4 + 3, (int)GameManager.selected.transform.position.y + 5 - 3);
            Set1((int)GameManager.selected.transform.position.x + 4 + 2, (int)GameManager.selected.transform.position.y + 5 - 3);
            Set1((int)GameManager.selected.transform.position.x + 4 + 1, (int)GameManager.selected.transform.position.y + 5 - 3);
            Set1((int)GameManager.selected.transform.position.x + 4, (int)GameManager.selected.transform.position.y + 5 - 3);
            Set1((int)GameManager.selected.transform.position.x + 4 - 1, (int)GameManager.selected.transform.position.y + 5 - 3);
            Set1((int)GameManager.selected.transform.position.x + 4 - 2, (int)GameManager.selected.transform.position.y + 5 - 3);
            Set1((int)GameManager.selected.transform.position.x + 4 - 3, (int)GameManager.selected.transform.position.y + 5 - 3);
            Set1((int)GameManager.selected.transform.position.x + 4 + 2, (int)GameManager.selected.transform.position.y + 5 - 4);
            Set1((int)GameManager.selected.transform.position.x + 4 + 1, (int)GameManager.selected.transform.position.y + 5 - 4);
            Set1((int)GameManager.selected.transform.position.x + 4, (int)GameManager.selected.transform.position.y + 5 - 4);
            Set1((int)GameManager.selected.transform.position.x + 4 - 1, (int)GameManager.selected.transform.position.y + 5 - 4);
            Set1((int)GameManager.selected.transform.position.x + 4 - 2, (int)GameManager.selected.transform.position.y + 5 - 4);
            Set1((int)GameManager.selected.transform.position.x + 4 + 1, (int)GameManager.selected.transform.position.y + 5 - 5);
            Set1((int)GameManager.selected.transform.position.x + 4, (int)GameManager.selected.transform.position.y + 5 - 5);
            Set1((int)GameManager.selected.transform.position.x + 4 - 1, (int)GameManager.selected.transform.position.y + 5 - 5);
            Set1((int)GameManager.selected.transform.position.x + 4, (int)GameManager.selected.transform.position.y + 5 - 6);
        }
        else if (y_difference > 0 && Mathf.Abs(y_difference) > Mathf.Abs(x_difference))//朝向向上
        {
            Set1((int)GameManager.selected.transform.position.x + 4 + 1, (int)GameManager.selected.transform.position.y + 5);
            Set1((int)GameManager.selected.transform.position.x + 4, (int)GameManager.selected.transform.position.y + 5);
            Set1((int)GameManager.selected.transform.position.x + 4 - 1, (int)GameManager.selected.transform.position.y + 5);
            Set1((int)GameManager.selected.transform.position.x + 4 + 1, (int)GameManager.selected.transform.position.y + 5 + 1);
            Set1((int)GameManager.selected.transform.position.x + 4, (int)GameManager.selected.transform.position.y + 5 + 1);
            Set1((int)GameManager.selected.transform.position.x + 4 - 1, (int)GameManager.selected.transform.position.y + 5 + 1);
            Set1((int)GameManager.selected.transform.position.x + 4 + 2, (int)GameManager.selected.transform.position.y + 5 + 2);
            Set1((int)GameManager.selected.transform.position.x + 4 + 1, (int)GameManager.selected.transform.position.y + 5 + 2);
            Set1((int)GameManager.selected.transform.position.x + 4, (int)GameManager.selected.transform.position.y + 5 + 2);
            Set1((int)GameManager.selected.transform.position.x + 4 - 1, (int)GameManager.selected.transform.position.y + 5 + 2);
            Set1((int)GameManager.selected.transform.position.x + 4 - 2, (int)GameManager.selected.transform.position.y + 5 + 2);
            Set1((int)GameManager.selected.transform.position.x + 4 + 3, (int)GameManager.selected.transform.position.y + 5 + 3);
            Set1((int)GameManager.selected.transform.position.x + 4 + 2, (int)GameManager.selected.transform.position.y + 5 + 3);
            Set1((int)GameManager.selected.transform.position.x + 4 + 1, (int)GameManager.selected.transform.position.y + 5 + 3);
            Set1((int)GameManager.selected.transform.position.x + 4, (int)GameManager.selected.transform.position.y + 5 + 3);
            Set1((int)GameManager.selected.transform.position.x + 4 - 1, (int)GameManager.selected.transform.position.y + 5 + 3);
            Set1((int)GameManager.selected.transform.position.x + 4 - 2, (int)GameManager.selected.transform.position.y + 5 + 3);
            Set1((int)GameManager.selected.transform.position.x + 4 - 3, (int)GameManager.selected.transform.position.y + 5 + 3);
            Set1((int)GameManager.selected.transform.position.x + 4 + 2, (int)GameManager.selected.transform.position.y + 5 + 4);
            Set1((int)GameManager.selected.transform.position.x + 4 + 1, (int)GameManager.selected.transform.position.y + 5 + 4);
            Set1((int)GameManager.selected.transform.position.x + 4, (int)GameManager.selected.transform.position.y + 5 + 4);
            Set1((int)GameManager.selected.transform.position.x + 4 - 1, (int)GameManager.selected.transform.position.y + 5 + 4);
            Set1((int)GameManager.selected.transform.position.x + 4 - 2, (int)GameManager.selected.transform.position.y + 5 + 4);
            Set1((int)GameManager.selected.transform.position.x + 4 + 1, (int)GameManager.selected.transform.position.y + 5 + 5);
            Set1((int)GameManager.selected.transform.position.x + 4, (int)GameManager.selected.transform.position.y + 5 + 5);
            Set1((int)GameManager.selected.transform.position.x + 4 - 1, (int)GameManager.selected.transform.position.y + 5 + 5);
            Set1((int)GameManager.selected.transform.position.x + 4, (int)GameManager.selected.transform.position.y + 5 + 6);
        }
        GameManager.UpdateFog();
    }

    public void Set1(int _x,int _y)//迷雾系统朝向区域置1
    {
        if (_x >= 0 && _y >= 0)
        {
            GameManager.fog_number[_x, _y] = 1;
        }
    }
}
