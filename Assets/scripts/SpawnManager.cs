using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Wave
{
    public int normalEnemys = 0;
    public int rangedEnemys = 0;
    public int heavyEnemys = 0;
    public int spawninterval = 1;
}



public class SpawnManager : MonoBehaviour
{
    public List<Wave> waves = new List<Wave>();
    public Enemycontroller enemy;
    public Enemycontroller rangedEnemy;
    public Enemycontroller heavyEnemy;
    public int wavei = 0;
    public Ability spawn = new Ability();

    void Start()
    {
        waves = generateWaves(10);


        onWaveStart();
    }

    public void onWaveStart()
    {
        if(wavei < waves.Count)
        {
            spawnEnemys();
        }
    }

    public void onWaveEnd()
    {
        wavei++;
    }

    public void spawnEnemys()
    {
        var wave = waves[wavei];
        int deathcount = 0;
        var enemys = new List<Enemycontroller>();
        for (int i = 0; i < wave.normalEnemys; i++)
        {
            enemys.Add(Instantiate(enemy));
        }
        for (int i = 0; i < wave.rangedEnemys; i++)
        {
            enemys.Add(Instantiate(rangedEnemy));
        }
        for (int i = 0; i < wave.heavyEnemys; i++)
        {
            enemys.Add(Instantiate(heavyEnemy));
        }
        enemys.ForEach(e =>
        {
            e.onDeath += () =>
            {
                deathcount++;
                if (deathcount == enemys.Count)
                {

                    onWaveEnd();
                }
            };
        });
    }

    void Update()
    {
        
    }


    public List<Wave> generateWaves(int amount)
    {
        Random.InitState(0);
        Random.Range(5, 10);

        var result = new List<Wave>();
        for (int i = 0; i < amount; i++)
        {
            result.Add(new Wave() { 
                normalEnemys = 4 + i * 2 ,
                rangedEnemys = i,
                heavyEnemys = i,
                spawninterval = 1,
            });
        }
        return result;
    }
}
