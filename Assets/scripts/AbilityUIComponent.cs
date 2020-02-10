using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityUIComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public Ability ability;
    public Image cooldownMask;
    public TMP_Text textMesh;

    private void Start() {
        textMesh.text = ability.name;
    }
    // Update is called once per frame
    void Update()
    {
        cooldownMask.fillAmount = ability.cooldownPercentComplete();

    }
}
