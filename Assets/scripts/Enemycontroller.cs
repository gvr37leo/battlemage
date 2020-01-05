using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemycontroller : MonoBehaviour
{

    public int health = 100;
    public bool isStunned = false;
    public List<Buff> buffs = new List<Buff>();
    public GameObject target;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        StartCoroutine(UpdateNavMeshAgent());
    }

    IEnumerator UpdateNavMeshAgent() {
        while (true) {
            agent.SetDestination(target.gameObject.transform.position);
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage) {
        health -= 50;
        buffs.Add(new Buff(2, "stunned"));//reset buff
        if(health <= 0) {
            Destroy(gameObject);
        }
    }
}
