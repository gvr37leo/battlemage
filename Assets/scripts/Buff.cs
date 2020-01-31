using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public string name;
    public int stacks;
    public float tickratehz = 1;
    public float starttimesec;
    public float duration;
    public event Action onStart;
    public event Action onTick;
    public event Action onEnd;
    public  Ability calltick;

    public Buff(float duration,string name) {
        this.name = name;
        this.duration = duration;
        starttimesec = Time.time;
        calltick = new Ability {
            cb = () => {
                onTick();
            },
            cooldownsec = 1000 / tickratehz,
        };
    }

    public void FrameStart() {
        calltick.Tapfire();
    }

    public void triggerOnStart()
    {
        onStart();
    }

    public void triggerOnEnd() {
        onEnd();
    }

}

public class BuffManager
{
    Dictionary<string, Buff> buffmap = new Dictionary<string, Buff>();

    public void Add(Buff buff)
    {
        if (buffmap.ContainsKey(buff.name) == false)
        {
            buff.stacks = 1;
            buff.starttimesec = Time.time;
            buffmap[buff.name] = buff;
            buff.triggerOnStart();
        }
        else
        {
            buff.stacks++;
            buff.starttimesec = Time.time;
        }
    }


    public void update()
    {
        foreach (var pair in buffmap)
        {
            var buff = pair.Value;
            if (to(buff.starttimesec,Time.time) >= buff.duration)
            {
                buffmap.Remove(buff.name);
                buff.triggerOnEnd();
            }
            else
            {
                buff.calltick.Tapfire();
            }
        }
    }

    public float to(float a, float b)
    {
        return b - a;
    }
}
