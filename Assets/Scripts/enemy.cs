using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
    public float health;//当前血量
    public float health_max;//最高血量
    public float hit;//命中
    public float evade;//闪避
    public int damage;//攻击力
    public float real_hit;//实际命中

    public bool ishit;//攻击是否命中

    public GameManager GameManager;
    public UIManager UIManager;
    public GameObject EnemyCell;//对应单元格
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        health = health_max;
        CloseEnemy();//初始化
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()//棋子攻击
    {
        SetCell();
        if (GameManager.selected.GetComponentInChildren<player>().actionpoint > 0 &&
            EnemyCell.GetComponentInChildren<cell>().attackcell.activeInHierarchy)//判断是否可攻击且enemy在攻击范围内
        {
            GameManager.selected.GetComponentInChildren<player>().Attack(transform.parent.gameObject);
            if(GameManager.selected.GetComponentInChildren<player>().ishit == true)
            {
                health = health - GameManager.selected.GetComponentInChildren<player>().damage;//敌人血量变化
            }
            else 
            {
                Debug.Log("Miss");
            }
            if (health <= 0)//判断血量是否为零
            {
                transform.parent.gameObject.SetActive(false);//将敌人设置为无活性
            }
            else
            {
                Attack(GameManager.selected);
                if(ishit == true)
                {
                    GameManager.selected.GetComponentInChildren<player>().health = 
                        GameManager.selected.GetComponentInChildren<player>().health - damage;//玩家血量变化
                }
                else
                {
                    Debug.Log("Respond Miss");
                }
            }
            UIManager.player_bloodUI.GetComponentInChildren<Slider>().value = 
                GameManager.selected.GetComponentInChildren<player>().health /
                  GameManager.selected.GetComponentInChildren<player>().health_max;//玩家血量显示
            GameManager.CloseRange();
        }
        UIWithEnemy();//敌人血量显示
    }

    public void Attack(GameObject _x)//攻击
    {
        int rdchar = Random.Range(0, 99);//随机数
        real_hit = hit / (hit + _x.GetComponentInChildren<player>().evade);//实际命中
        Debug.Log(transform.parent.gameObject);
        Debug.Log(real_hit);
        if (rdchar <= real_hit * 100)//判断是否攻击命中
        {
            ishit = true;
        }
        else
        {
            ishit = false;
        }
    }

    public void SetCell()//将对应的单元格设置为不可移动
    {
        foreach (var cell in GameManager.cells)//扫描所有单元格
        {
            if (transform.parent.gameObject.transform.position.x == cell.transform.position.x &&
               transform.parent.gameObject.transform.position.y == cell.transform.position.y)
            {
                cell.GetComponentInChildren<cell>().playernumber = 1;
                EnemyCell = cell;
                hit = hit + cell.GetComponentInChildren<cell>().hit_up;//命中加成
                evade = evade + cell.GetComponentInChildren<cell>().evade_up;//闪避加成
            }
        }
    }

    public void CloseEnemy()//初始化
    {
        hit = 0.8f;
        evade = 0.1f;
    }

    public void UIWithEnemy()//血量条与人物绑定
    {
        UIManager.enemy_bloodUI.SetActive(true);
        UIManager.enemy_bloodUI.GetComponentInChildren<Slider>().value = health / health_max;
        UIManager.enemy_bloodUI.GetComponentInChildren<Text>().text = transform.parent.gameObject.name;
    }
}
