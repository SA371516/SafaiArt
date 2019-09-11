using Assets.Script.Game;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsManager : BaseState
{
    Text Title;
    GameObject[] Planes;
    GameObject[] Buttons;
    string[] TitleText =
    {
        "Instructions",
        "遊び方"
    };
    RectTransform rect;
    int _length;
    public override void InstanceManager()
    {
        Planes = GameObject.FindGameObjectsWithTag("Planes");
        Buttons = GameObject.FindGameObjectsWithTag("Button");
        Title = GameObject.Find("Title_Text").GetComponent<Text>();
        _length = 0;
        foreach (var G in Planes)
        {
            G.SetActive(false);
        }
        Title.text = TitleText[_length];
    }

    public override void Manager()
    {
        if (_length == 0)
        {
            Buttons[0].SetActive(false);
            Buttons[1].SetActive(true);
        }
        else if (Planes.Length - 1 == _length)
        {
            Buttons[0].SetActive(true);
            Buttons[1].SetActive(false);
        }
        else
        {
            Buttons[0].SetActive(true);
            Buttons[1].SetActive(true);
        }
        Planes[_length].SetActive(true);
    }

    public override void PanelChange(int _change)
    {
        Planes[_length].SetActive(false);
        _length+=_change;
        //範囲外判定
        if (_length >= TitleText.Length)
        {
            _length = TitleText.Length - 1;
        }
        else if (_length < 0)
        {
            _length = 0;
        }
        Title.text = TitleText[_length];
    }

}
