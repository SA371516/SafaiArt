using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClearText : MonoBehaviour
{
    public Text text;
    bool ok;
    float time;

    public float Interval;
    Color color;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        text.text = "";
        color = text.color;
        color.a = 0;
        text.color = color;
    }
    private void Update()
    {
        //シーンジャンプする時間
        if (ok)
        {
            time += Time.deltaTime;
        }
        if (time >= 5)
        {
            time = 0;
            SceneJump jump = GameObject.Find("GameManeger").GetComponent<SceneJump>();
            jump.Jump("Clear");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            ok = true;
            GameObject.FindWithTag("Player").GetComponent<PlayerMove>().Clear = ok;
            text.text = "Clear!!";
            StartCoroutine(StartVI());
        }
    }
    IEnumerator StartVI()
    {
        float time = 0;
        //見えるように
        while (time <= Interval)
        {
            int a = 1;
            time += Time.deltaTime * a;
            color.a = time / Interval;
            text.color = color;
            yield return new WaitForSeconds(1f / 60f);
        }
        time = Interval;
        //見えなくなるように
        while (time >= 0)
        {
            int a = -1;
            time += Time.deltaTime * a;
            color.a = time / Interval;
            text.color = color;
            yield return new WaitForSeconds(1f / 60f);
        }
        StartCoroutine(StartVI());
    }

}
