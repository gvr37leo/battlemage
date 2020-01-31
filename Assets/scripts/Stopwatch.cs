using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

class StopWatch {

    float starttimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    float pausetimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    float pausetime = 0;
    bool paused = true;

    public float get() {
        var currentamountpaused = 0f;
        if(paused){
            currentamountpaused = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - pausetimestamp;
        }
        return DateTimeOffset.UtcNow.ToUnixTimeSeconds() - starttimestamp - (pausetime + currentamountpaused);
    }

    public void start() {
        paused = false;
        starttimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        pausetime = 0;
    }

    public void resume(){
        if(paused){
            paused = false;
            pausetime += DateTimeOffset.UtcNow.ToUnixTimeSeconds() - pausetimestamp;
        }
    }

    public void pause() {
        if (paused == false) {
            paused = true;
            pausetimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }

    public void reset() {
        paused = true;
        starttimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        pausetimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        pausetime = 0;
    }
}
