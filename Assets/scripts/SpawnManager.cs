using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    public int enemys = 0;
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
                enemys = 4 + i * 2 ,
                spawninterval = 1,
            });
        }
        return result;
    }
}
