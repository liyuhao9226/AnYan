using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public float health;//当前血量
    public float health_max;//血量上限
    public int damage;//伤害
    public int actionpoint;//行动点
    public int actionpoint_max;//行动点上限
    public float hit;//命中
    public float evade;//闪避
    private float real_hit;//实际命中

    public int attackrange;//攻击范围

    public bool ishit;//是否攻击击中
    public bool isdetect;//是否探测

    private GameManager GameManager;
    private UIManager UIManager;
    private MusicManager MusicManager;
    private GameObject Camera;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        MusicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        Camera = GameObject.Find("Main Camera");
        Closeplayer();//初始化
        health = health_max;
        actionpoint = actionpoint_max;
        isdetect = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(float _x, float _y)//移动
    {
        int waste = 0;
        GameManager.FindPath(_x, _y);
        foreach (var cell in GameManager.CLOSE)
        {
            waste = waste + cell.GetComponentInChildren<cell>().move_waste;
        }
        actionpoint = actionpoint - waste;
        waste = 0;
        GameManager.CLOSE.Clear();
        transform.parent.gameObject.transform.position = new Vector3(_x, _y, 0);
        GameManager.CloseRange();//关闭范围
        GameManager.SetFog();//迷雾刷新
    }

    public void Attack(GameObject _x)//攻击
    {
        actionpoint = actionpoint - 5;
        int rdchar = Random.Range(0, 99);//随机数
        real_hit = hit / (hit + _x.GetComponentInChildren<enemy>().evade);//实际命中
        if (rdchar <= real_hit * 100)//判断是否攻击命中
        {
            ishit = true;
        }
        else
        {
            ishit = false;
        }
    }

    public void OnMouseDown()//选择物体
    {
        CameraFallow();
        GameManager.selected = transform.parent.gameObject;//GameManager选中player
        GameManager.CloseRange();
        UIManager.movable = true;
        UIManager.attackable = true;
        UIWithPlayer();//UI与玩家匹配
    }

    public void SetCell()//将对应的单元格设置为不可移动
    {
        foreach (var cell in GameManager.cells)//扫描所有单元格
        {
            if (transform.parent.gameObject.transform.position.x == cell.transform.position.x &&
               transform.parent.gameObject.transform.position.y == cell.transform.position.y)
            {
                cell.GetComponentInChildren<cell>().playernumber = 1;
                hit = hit + cell.GetComponentInChildren<cell>().hit_up;//命中加成
                evade = evade + cell.GetComponentInChildren<cell>().evade_up;//闪避加成
            }
        }
    }

    public void Closeplayer()//初始化player
    {
        actionpoint = actionpoint_max;
        hit = 0.7f;
        evade = 0.15f;
    }

    public void UIWithPlayer()//血量条与人物绑定
    {
        UIManager.player_bloodUI.SetActive(true);
        UIManager.moveUI.SetActive(true);
        UIManager.attackUI.SetActive(true);
        UIManager.detectUI.SetActive(true);
        UIManager.player_bloodUI.GetComponentInChildren<Slider>().value = health / health_max;
        UIManager.player_bloodUI.GetComponentInChildren<Text>().text = transform.parent.gameObject.name;
    }

    public void CameraFallow()//相机跟随
    {
        Vector3 pos = transform.parent.gameObject.transform.position;
        pos.z = -10;
        Camera.transform.position = pos;
    }

    public void BaseFog()//迷雾范围判定
    {
        foreach(var fog in GameManager.fogs)
        {
            if((fog.transform.position - transform.parent.gameObject.transform.position).sqrMagnitude <= 2)//将玩家周围一格设置为完全透明
            {
                GameManager.fog_number[(int)fog.transform.position.x + 4, (int)fog.transform.position.y + 5] = 1;
            }
        }
    }
}
