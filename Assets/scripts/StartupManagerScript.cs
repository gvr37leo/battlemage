using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupManagerScript : MonoBehaviour
{

    public AbilityUIComponent abilityUIComponent;
    
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Playercontroller>().init();
        FindObjectOfType<AbilityBarInitializer>().init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
