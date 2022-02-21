using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    const int FOG_NUMBER_X = 24;//X轴迷雾单元格数量
    const int FOG_NUMBER_Y = 24;//Y轴迷雾单元格数量

    public int turn = 0;//当前回合数
    public GameObject selected;//当前游戏物体

    public GameObject[] cells;//所有cell
    public GameObject[] enemies;//所有enemy
    public GameObject[] players;//所有player
    public GameObject[] fogs;//所有fog

    public int[,] fog_number;//迷雾的亮度判定
    public GameObject next;//下一步单元格
    public List<GameObject> SELECTEDLIST;//初步删选结果
    public List<GameObject> OPEN;//子节点储存
    public List<GameObject> CLOSE;//自动寻路路径

    public UIManager UIManager;
    // Start is called before the first frame update
    void Start()
    {
        cells = GameObject.FindGameObjectsWithTag("Cell");
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        players = GameObject.FindGameObjectsWithTag("player");
        fogs = GameObject.FindGameObjectsWithTag("fog");
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        fog_number = new int[FOG_NUMBER_X,FOG_NUMBER_Y];
        OPEN = new List<GameObject>();
        CLOSE = new List<GameObject>();
        SELECTEDLIST = new List<GameObject>();
        selected = null;
        Turn();//开始回合计数
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMoveRange()//显示移动范围
    {
        ShowPlayer();//判断玩家所在单元格
        ShowEnemy();//判断敌人所在单元格
        int waste_all = 0;
        int range = selected.GetComponentInChildren<player>().actionpoint;//移动距离
        FirstSelect(range);//初步删选
        for(int i = 0;i < SELECTEDLIST.Count;i++)
        {
            FindPath(SELECTEDLIST[i].transform.position.x, SELECTEDLIST[i].transform.position.y);//寻找路径
            foreach (var path in CLOSE)//计算行动点消耗总计
            {
                waste_all = waste_all + path.GetComponentInChildren<cell>().move_waste;
            }
            if (waste_all < selected.GetComponentInChildren<player>().actionpoint + 1 &&
                CLOSE.Exists(cell => cell == SELECTEDLIST[i]))//判断是否可达且目的地在路径内
            {
                SELECTEDLIST[i].GetComponentInChildren<cell>().movecell.SetActive(true);
            }
            CLOSE.Clear();
            waste_all = 0;
        }
        SELECTEDLIST.Clear();
    }

    public void ShowAttackRange()//显示攻击范围
    {
        ShowPlayer();//判断玩家所在单元格
        int range = selected.GetComponentInChildren<player>().attackrange;//攻击距离
        if(selected.GetComponentInChildren<player>().actionpoint > 0)
        {
            foreach (var cell in cells)//扫描cell
            {
                if ((Mathf.Abs(cell.transform.position.x - selected.transform.position.x) +
                   Mathf.Abs(cell.transform.position.y - selected.transform.position.y) <= range) &&
                   cell.GetComponentInChildren<cell>().playernumber == 0)//判断距离和单元格是否有玩家存在
                {
                    cell.GetComponentInChildren<cell>().attackcell.SetActive(true);
                }
            }
        }
    }

    public void CloseRange()//关闭移动范围
    {
        foreach (var cell in cells)//扫描cell
        {
            cell.GetComponentInChildren<cell>().CloseCell();
        }
    }

    public void CloseRole()//重置人物属性
    {
        foreach(var player in players)
        {
            player.GetComponentInChildren<player>().Closeplayer();
        }
        foreach(var enemy in enemies)
        {
            enemy.GetComponentInChildren<enemy>().CloseEnemy();
        }
    }

    public void ShowEnemy()//敌人显示
    {
        foreach(var enemy in enemies)//扫描enemy
        {
            enemy.GetComponentInChildren<enemy>().SetCell();
        }
    }

    public void ShowPlayer()//玩家显示
    {
        foreach (var player in players)//player
        {
            player.GetComponentInChildren<player>().SetCell();
        }
    }

    public void Turn()//回合重置
    {
        turn ++;//回合数增加
        selected = null;
        UIManager.CloseUIManager();
        CloseRange();//关闭范围
        CloseRole();
        SetFog();
    }

    public void FindChildren(GameObject _x)//寻找子节点
    {
        for(int i = 0;i < cells.Length; i++)//所有单元格
        {
            if ((cells[i].transform.position - _x.transform.position).sqrMagnitude == 1)
            {
                OPEN.Add(cells[i]);
            }
        }
    }

    public void FindPath(float _x,float _y)//自动寻路
    {
        int temp = 0;//当前步数
        int jiazhi = (int)(selected.transform.position - new Vector3(_x,_y,0)).sqrMagnitude;//价值
        bool update;//记录最新价值
        FindChildren(selected);
        while (OPEN != null)
        {
            update = false;
            CLOSE.Add(gameObject);//使下一个不为空
            for (int i = 0; i < OPEN.Count; i++)
            {
                if ((OPEN[i].transform.position - new Vector3(_x, _y, 0)).sqrMagnitude < jiazhi)//判断价值
                {
                    jiazhi = (int)(OPEN[i].transform.position - new Vector3(_x, _y, 0)).sqrMagnitude;//更新价值
                    update = true;
                    CLOSE.RemoveAt(temp);
                    CLOSE.Add(OPEN[i]);//更新链表中temp的值
                    next = OPEN[i];//下一步的单元格
                }
            }
            if(!update)//判断是否无法进行下一步
            {
                if (CLOSE.Count > 2)//判断是否返回到起点
                {
                    next = CLOSE[temp - 2];//更改next
                    jiazhi = (int)(next.transform.position - new Vector3(_x, _y, 0)).sqrMagnitude;//价值还原
                    OPEN.Clear();
                    FindChildren(next);
                    OPEN.Remove(CLOSE[temp - 1]);//删除错误路径
                    CLOSE.RemoveAt(temp);
                    CLOSE.RemoveAt(temp - 1);//更改CLOSE表
                    temp--;//更改步数
                }
                else
                {
                    CLOSE.Remove(gameObject);
                    OPEN.Clear();
                    break;//退出，保证不进入死循环
                }
            }
            else
            {
                temp++;//步数增加
                if (jiazhi == 0)
                {
                    OPEN.Clear();
                    break;
                }//跳出循环
                else
                {
                    OPEN.Clear();
                    FindChildren(next);
                }
            }
        }
    }

    public void FirstSelect(int _range) //初步删选
    {
        for(int i = 0; i < cells.Length; i++)
        {
            if ((Mathf.Abs(cells[i].transform.position.x - selected.transform.position.x) +
               Mathf.Abs(cells[i].transform.position.y - selected.transform.position.y)) <= _range &&
               cells[i].GetComponentInChildren<cell>().playernumber == 0)//判断距离和单元格是否有玩家存在
            {
                SELECTEDLIST.Add(cells[i]);
            }
        }
    }

    public void UpdateFog()//更新迷雾透明度
    {
        foreach(var fog in fogs)
        {
            if (fog_number[(int)fog.transform.position.x + 4, (int)fog.transform.position.y + 5] == 0)
            {
                fog.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 255);
            }
            else if(fog_number[(int)fog.transform.position.x + 4, (int)fog.transform.position.y + 5] == 1)
            {
                fog.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            }
        }
    }

    public void SetFog()//迷雾判定
    {
        for(int i = 0; i < FOG_NUMBER_X; i++)
        {
            for(int j = 0;j < FOG_NUMBER_Y; j++)
            {
                fog_number[i, j] = 0;
            }
        }
        foreach(var player in players)
        {
            player.GetComponentInChildren<player>().BaseFog();
        }
        UpdateFog();
    }

    public void Detect()//探测功能
    {
        selected.GetComponentInChildren<player>().isdetect = true;
    }
}
