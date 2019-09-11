using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Ber : MonoBehaviour
{
    private RectTransform myRectTrans;
    public Enemy enemy_scr;
    public Slider En_HP_Ber;
    // Start is called before the first frame update
    void Start()
    {
        En_HP_Ber.maxValue = enemy_scr.GetSet_EMHP;

        myRectTrans = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

        En_HP_Ber.value = enemy_scr.GetSet_EMHP;
        myRectTrans.LookAt(GameObject.FindWithTag("Player").transform);
    }
}
