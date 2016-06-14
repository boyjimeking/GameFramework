﻿using UnityEngine;
using System.Collections;

//游戏的入口，不可销毁
public class GameManager : MonoBehaviour {

    public static GameManager Instance = null;

    [HideInInspector]
    public LuaManager luaMgr = null;
    [HideInInspector]
    public NetworkManager netMgr = null;
    [HideInInspector]
    public ResManager resMgr = null;
    [HideInInspector]
    public UIManager uiMgr = null;

    public static Entity MainPlayer = null;

    public uint HeroConfigID = 10;
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);   
        luaMgr = gameObject.AddComponent<LuaManager>();
        netMgr = gameObject.AddComponent<NetworkManager>();
        resMgr = gameObject.AddComponent<ResManager>();
        uiMgr = gameObject.AddComponent<UIManager>();
    }
	// Use this for initialization
	void Start () {

        luaMgr.InitStart();

        CSVManager.LoadAllCsv();
        MainPlayer = EntityManager.Instance.Get(HeroConfigID, 1);
        MainPlayer.Pos = new Vector3(40f, 0, 50f);
        MainPlayer.AttachCamera();

        //UIManager.Instance.CreatePanel("FightPanel");
	}
	
	// Update is called once per frame
	void Update ()
    {
        EntityManager.Instance.Update();
        EffectManager.Instance.Update();
        BulletManager.Instance.Update();

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            MainPlayer.Skill.CastSkill(0);
        }
	}

    void OnApplicationQuit()
    {
        CSVManager.ClearAllCsv();
        EntityManager.Instance.Clear();
        EffectManager.Instance.Clear();
        BulletManager.Instance.Clear();
        Log.Info("application quit.");
        Log.Stop();
    }

   

}