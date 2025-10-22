using System;
using UnityEngine;
using UnityEngine.UI;

public class TipsManager : MonoBehaviour
{
    public static Action<Sprite> displayTipEvent;
    public static Action disableTipEvent;


    private Animator anim;
    private int activeTips;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        displayTipEvent += displayTip;
        disableTipEvent += disableTip;
    }

    private void OnDisable()
    {
        displayTipEvent -= displayTip;
        disableTipEvent -= disableTip;
    }

    private void displayTip(Sprite image)
    {
        anim.SetInteger("state", ++activeTips);
    }

    private void disableTip()
    {
        anim.SetInteger("state", --activeTips);

        // ���� �������� ��������� ��� - ��������� ��������
        if (activeTips <= 0)
        {
            activeTips = 0; // ������ �� ������������� ��������
        }
    }
}