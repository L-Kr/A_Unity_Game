using System.Collections;
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
}

public class CD_Manager
{
    public CD_Manager(float cd_q, float cd_e, float cd_r)
    {
        Q = cd_q;
        E = cd_e;
        R = cd_r;
    }
    public float Q { get; private set; }
    public float E { get; private set; }
    public float R { get; private set; }

    public float cdq = 0;
    public float cde = 0;
    public float cdr = 0;
}
