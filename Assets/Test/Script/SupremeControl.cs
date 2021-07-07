using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SupremeControl : MonoBehaviour
{
    //**
    //该脚本用作每一关最高控制
    //实现功能，退出，调节音量，获得帮助等
    //**

    public static SupremeControl instance;//单例模式
    
    

    //以下是私有变量，策划无需关心
    private int finishNum = 0;
    public bool[] finish;//全局最高控制数组
    private GameObject[] hardware;//硬件

    // Start is called before the first frame update
    void Start()
    {
        instance = this;//单例模式 
        hardware = GameObject.FindGameObjectsWithTag("Hardware");//获取所有硬件
        this.finish = new bool[hardware.Length];//确认总数组长度
        for (int i = 0; i <hardware.Length; i++)
        {
            finish[i] = false;
            hardware[i].gameObject.name = i + "号硬件";

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 

    public void Music(bool m)
    {
        //提示音乐播放函数
        if (m)
        {
            //播放成功音乐
            Debug.Log("播放成功音乐");
        }
        else
        {
            //播放失败音乐
            Debug.Log("播放失败音乐");
        }
    }

    public void Tips()
    {
        //提示
        for (int i = 0; i < hardware.Length; i++)
        {
            if (finish[i])
                finishNum++;
        }
        if (finishNum == hardware.Length)
        {
            Debug.Log("本关卡已完成");
            //跳转下一关或主界面
        }
        else
        {
            Debug.Log("还有" + (hardware.Length - finishNum) + "个硬件未能成功归位");
        }
    }

    public void Finish(GameObject gameObject)
    {
        int i=0;
        for (; i < hardware.Length; i++)
        {
            if (hardware[i] == gameObject)
            {
                break;
            }
        }
        finish[i] = true;
    }
}
