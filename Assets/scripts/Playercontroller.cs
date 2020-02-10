using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//check ability system
//implement finisher
//create 2 more enemies - ranged - big
//wave spawning
//surrounding behaviour
//combo counter
//ability ui
//health bar


public class Playercontroller : MonoBehaviour
{

    public float speed = 5;
    public Ability parry;
    public Ability blink;
    public Ability regen;
    public Ability melee;
    public Ability ranged;
    public Ability finisher;
    public List<Ability> abilitys;
    public LayerMask enemymask;
    public float energy = 100;
    public int combopoints = 0;
    public BuffManager buffs = new BuffManager();
    public GameObject missilePrefab;

    public void init()
    {
        parry = new Ability()
        {
            name = "parry",
            cooldownsec = 1,
            cb = () => {
                buffs.Add(new Buff(1, "parry"));
                buffs.Add(new Buff(0.5f, "perfectparry"));
            }
        };

        var blinkenergycost = 10;
        blink = new Ability()
        {
            name = "blink",
            cooldownsec = 0.2f,
            cb = () => {
                var range = 5;
                transform.position += transform.forward * range;
                energy -= blinkenergycost;
            },
        };
        blink.rules.Add(new Rule() { 
            message = "out of energy",
            cb = () =>
            {
                return energy >= blinkenergycost;
            }
        });

        regen = new Ability()
        {
            name = "regen",
            cooldownsec = 1,
            cb = () => {
                energy = 100;
            },
        };
        melee = new Ability()
        {
            name = "melee",
            cooldownsec = 1,
            cb = () => {
                var center = transform.position + transform.forward * 1;
                var radius = 1;

                var hits = Physics.BoxCastAll(center, new Vector3(10, 10, 10), Vector3.up, Quaternion.identity, 100, enemymask, QueryTriggerInteraction.Collide);
                hits = Physics.SphereCastAll(center, radius, transform.forward, 1, enemymask, QueryTriggerInteraction.Collide);
                if (hits.Length > 0)
                {
                    var hit = hits.First();
                    var enemy = hit.transform.gameObject.GetComponent<Enemycontroller>();
                    enemy.TakeDamage(50);
                }
                else
                {
                    //miss
                }
            },
        };
        ranged = new Ability()
        {
            name = "ranged",
            cooldownsec = 1,
            cb = () => {
                var missile = Instantiate(missilePrefab,gameObject.transform.position + gameObject.transform.forward * 1,Quaternion.LookRotation(gameObject.transform.forward,Vector3.up));

                //var range = 8;
                //var hits = Physics.RaycastAll(transform.position, transform.position, range, enemymask.value);
                //foreach (var hit in hits)
                //{
                //    var enemy = hit.transform.gameObject.GetComponent<Enemycontroller>();
                //    enemy.TakeDamage(50);
                //}
            },
        };
        finisher = new Ability()
        {
            name = "finisher",
        };

        abilitys = new List<Ability>() {parry,blink,regen,melee,ranged,finisher };
    }

    void Start()
    {
        //UnityEditor.EditorWindow.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));

     


    }

    void Update()
    {
        var input = getInput();
        transform.position += input * speed * Time.deltaTime;
        buffs.update();
        if(input.magnitude > 0) {
            transform.LookAt(transform.position + input);
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            blink.Tapfire();
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            regen.Tapfire();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            //melee.Tapfire();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            parry.Tapfire();
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            ranged.Tapfire();
        }
    }


    Vector3 getInput() {
        var result = new Vector3();
        if (Input.GetKey(KeyCode.W)) {
            result.z++;
        }
        if (Input.GetKey(KeyCode.S)) {
            result.z--;
        }
        if (Input.GetKey(KeyCode.A)) {
            result.x--;
        }
        if (Input.GetKey(KeyCode.D)) {
            result.x++;
        }
        return result;
    }

    private void OnDrawGizmos() {
        //Gizmos.DrawSphere(transform.position + transform.forward * 1, 1);
    }
}
