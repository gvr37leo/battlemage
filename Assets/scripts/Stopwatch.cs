using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

class StopWatch {

    float starttimestamp = Time.time;
    float pausetimestamp = Time.time;
    float pausetime = 0;
    bool paused = true;

    public float get() {
        var currentamountpaused = 0f;
        if(paused){
            currentamountpaused = Time.time - pausetimestamp;
        }
        return Time.time - starttimestamp - (pausetime + currentamountpaused);
    }

    public void start() {
        paused = false;
        starttimestamp = Time.time;
        pausetime = 0;
    }

    public void resume(){
        if(paused){
            paused = false;
            pausetime += Time.time - pausetimestamp;
        }
    }

    public void pause() {
        if (paused == false) {
            paused = true;
            pausetimestamp = Time.time;
        }
    }

    public void reset() {
        paused = true;
        starttimestamp = Time.time;
        pausetimestamp = Time.time;
        pausetime = 0;
    }
}
