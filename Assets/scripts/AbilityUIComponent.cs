using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUIComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public Ability ability;
    public Image cooldownMask;

    // Update is called once per frame
    void Update()
    {
        cooldownMask.fillAmount = ability.cooldownPercentComplete();

    }
}
