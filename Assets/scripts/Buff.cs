using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public string name;
    public float tickratehz = 1;
    public float starttime;
    public float duration;
    public event Action onStart;
    public event Action onTick;
    public event Action onEnd;
    private Ability calltick;

    public Buff(float duration,string name) {
        this.duration = duration;
        starttime = Time.time;
        calltick = new Ability {
            cb = () => {
                onTick();
            },
            cooldownms = 1000 / tickratehz,
        };
    }

    public void FrameStart() {
        calltick.Tapfire();
    }
}
