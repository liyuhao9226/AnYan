using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject player_bloodUI;//玩家血量UI
    public GameObject enemy_bloodUI;//敌人血量UI
    public GameObject moveUI;//玩家移动UI
    public GameObject attackUI;//玩家攻击UI
    public GameObject startUI;//开始界面UI
    public GameObject turnUI;//回合结束UI
    public GameObject detectUI;//侦测UI

    public bool startable;//保证只执行一次UI事件
    public bool movable;//保证只执行一次UI事件
    public bool attackable;//保证只执行一次UI事件
    public bool detectable;//保证只执行一次UI事件

    private GameManager GameManager;
    private MusicManager MusicManager;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        MusicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        player_bloodUI = GameObject.Find("player_bloodUI");
        enemy_bloodUI = GameObject.Find("enemy_bloodUI");
        moveUI = GameObject.Find("moveUI");
        attackUI = GameObject.Find("attackUI");
        startUI = GameObject.Find("StartUI");
        turnUI = GameObject.Find("TurnUI");
        detectUI = GameObject.Find("detectUI");
        startUI.SetActive(true);
        CloseUIManager();
    }

    // Update is called once per frame
    void Update()
    {
        Click();//点击UI事件
    }

    public void Click()//点击UI事件
    {
        startUI.GetComponentInChildren<Button>().onClick.AddListener(delegate ()
        {
            if(startable == true)
            {
                MusicManager.tem = Resources.Load<AudioClip>("music/tiger");
                MusicManager.PlayNext();//加载音乐tiger
                startUI.SetActive(false);//开始界面UI
                GameManager.SetFog();
            }
            startable = false;
        });
        moveUI.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            if (GameManager.selected.GetComponentInChildren<player>().actionpoint >= 1 &&
                movable == true)//点击移动UI
            {
                GameManager.ShowMoveRange();
                movable = false;
            }
        });
        attackUI.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            if(GameManager.selected.GetComponentInChildren<player>().actionpoint >= 0 &&
               attackable == true)//点击攻击UI
            {
                GameManager.ShowAttackRange();
                attackable = false;
            }
        });
        detectUI.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            if(GameManager.selected.GetComponentInChildren<player>().actionpoint >= 1 &&
                detectable == true)//点击探测UI
            {
                GameManager.Detect();
                detectable = false;
            }
        });
        turnUI.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            GameManager.Turn();//下一回合
        });
    }

    public void CloseUIManager()//UI初始化
    {
        startable = true;
        movable = true;
        attackable = true;
        detectable = true;
        player_bloodUI.SetActive(false);
        enemy_bloodUI.SetActive(false);
        moveUI.SetActive(false);
        attackUI.SetActive(false);
        detectUI.SetActive(false);
    }
}
