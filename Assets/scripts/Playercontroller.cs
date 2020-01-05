using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{

    public float speed = 5;
    public Ability parry;
    public Ability blink;
    public Ability regen;
    public Ability melee;
    public Ability ranged;
    public Ability finisher;
    public LayerMask enemymask;
    public float energy = 100;
    public int combopoints = 0;
    public List<Buff> buffs = new List<Buff>();


    void Start()
    {
        UnityEditor.EditorWindow.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));


        parry = new Ability() {
            cooldownms = 1000,
            cb = () => {
                buffs.Add(new Buff(1, "parry"));
                buffs.Add(new Buff(0.5f, "perfectparry"));
            }
        };
        blink = new Ability() {
            cooldownms = 1000,
            cb = () => {
                var range = 5;
                transform.position += transform.forward * range;
            },
        };
        regen = new Ability() {
            cooldownms = 1000,
            cb = () => {
                energy = 100;
            },
        };
        melee = new Ability() {
            cooldownms = 1000,
            cb = () => {
                var center = transform.position + transform.forward * 1;
                var radius = 1;
                var hits = Physics.BoxCastAll(center, new Vector3(1,1,1), Vector3.up, Quaternion.identity, 100, enemymask.value);
                if(hits.Length > 0) {
                    var hit = hits.First();
                    var enemy = hit.transform.gameObject.GetComponent<Enemycontroller>();
                    enemy.TakeDamage(50);
                } else {
                    //miss
                }
            },
        };
        ranged = new Ability() {
            cooldownms = 1000,
            cb = () => {
                var range = 8;
                var hits = Physics.RaycastAll(transform.position, transform.position, range, enemymask.value);
                foreach(var hit in hits) {
                    var enemy = hit.transform.gameObject.GetComponent<Enemycontroller>();
                    enemy.TakeDamage(50);
                }
            },
        };
        finisher = new Ability() {

        };

    }

    void Update()
    {
        var input = getInput();
        transform.position += input * speed * Time.deltaTime;
        if(input.magnitude > 0) {
            transform.LookAt(transform.position + input);
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            blink.Activate();
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            regen.Activate();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            melee.Activate();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            parry.Activate();
        }
        if (Input.GetKeyDown(KeyCode.G)) {
            parry.Activate();
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
