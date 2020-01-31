using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class MissileController : MonoBehaviour
{

    public float speed = 10;
    public float spawntime;
    public float lifetime = 10;
    // Start is called before the first frame update
    void Start()
    {
        spawntime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Utilitys.to(spawntime, Time.time) >= lifetime)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
        }
    }
}
