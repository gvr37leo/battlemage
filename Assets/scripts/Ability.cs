using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Rule {
    public string message;
    public Func<bool> cb;

    public Rule(){

    }
}

public class Ability
{
    // ammo:number = 1
    // maxammo:number = 1
    // ammorechargerate:number = 1000
    // casttime:number = 2000
    // channelduration:number = 3000


    public string name;
    public float cooldownsec = 5;
    public float lastfire = 0;
    public List<Rule> rules = new List<Rule>{
       
        //cast while moving rule
        //must have target rule
        //must have valid target rule
        //resource rule
        //ammo rule
        //line of sight rule
    };
    StopWatch stopwatch = new StopWatch();
    public float ventingtime = 0;
    public event Action onCastFinished;
    public int shots = 0;
    public bool firing = false;
    public Action cb;

    public Ability() {
        rules.Add(createCooldownRule());
    }

    //positive if you need to wait 0 or negative if you can call it
    public float timeTillNextPossibleActivation(){
        return (lastfire + cooldownsec) - Time.time;
    }

    public float cooldownPercentComplete()
    {
        return timeTillNextPossibleActivation() / cooldownsec;
    }

    public bool canActivate() {
        return rules.Any(r => r.cb());
    }

    public void Activate() {
        cb();
    }

    public void fire() {//activate
        if (firing == false) {
            Startfire();
        } else {
            Holdfire();
        }
    }

    public void Releasefire() {
        firing = false;
    }

    public void Tapfire() {
        Startfire();
        Releasefire();
    }

    public void Startfire() {
        if (rules.All(r => r.cb())) {
            firing = true;
            ventingtime = 0;
            this.stopwatch.start();
            ventingtime -= this.cooldownsec;
            shots = 1;
            lastfire = Time.time;
            cb();
        }
    }

    public void Holdfire() {
        this.ventingtime += this.stopwatch.get();
        this.stopwatch.start();
        shots = (int)Math.Ceiling(ventingtime / cooldownsec);
        ventingtime -= this.shots * cooldownsec;
        lastfire = Time.time;
        if (shots > 0) {
            cb();
        }
    }

    public Rule createCooldownRule() {
        return new Rule() {
            message = "not ready yet",
            cb = () => {
                return (lastfire + cooldownsec) < Time.time;
            }
        };
    }
}

