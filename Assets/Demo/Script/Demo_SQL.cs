﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_SQL {

    private static Demo_SQL instance;
    private Demo_SQL(){}
    private static object instance_lock = new object(); //锁同步
    
    public static Demo_SQL Instance()
    {
        if (instance == null)   //若为空
        {
            lock (instance_lock)    //加锁保证线程安全
            {
                if (instance == null)   //若为空则实例化
                    instance = new Demo_SQL();
            }
        }
        return instance;
    }

    public string Username { set; get; }
    public string Heroname { set; get; }
}

public class CD_Manager
{
    public CD_Manager(float cd_q, float cd_e, float cd_r)
    {
        Q = cd_q;
        E = cd_e;
        R = cd_r;
    }

    /*技能总CD*/
    public float Q { get; private set; }
    public float E { get; private set; }
    public float R { get; private set; }

    /*技能剩余cd*/
    public float cdq = 0;
    public float cde = 0;
    public float cdr = 0;

    /*技能蓝耗,public读，private写*/
    private float consume_q = 50f; //默认值
    private float consume_e = 75f;
    private float consume_r = 100f;
    public float Consume_q
    {
        get
        {
            return consume_q;
        }
    }
    public float Consume_e
    {
        get
        {
            return consume_e;
        }
    }
    public float Consume_r
    {
        get
        {
            return consume_r;
        }
    }

    public void Set_consume(float q,float e,float r)
    {
        consume_q = q;
        consume_e = e;
        consume_r = r;
    }
}
