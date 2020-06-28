using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    [SerializeField] GameObject goComboImage = null;
    [SerializeField] Text txtCombo = null;
    Animator anim;

    int currentCombo = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
        txtCombo.gameObject.SetActive(false);
        goComboImage.SetActive(false);
    }
    private void Update()
    {
        comboEffect();
    }
    public void IncreaseCombo(int num=1)
    {
        currentCombo += num;
        txtCombo.text = string.Format("{0:#,##0}", currentCombo);   //문자열 정렬

        if(currentCombo>10)
        {
            txtCombo.gameObject.SetActive(true);
            goComboImage.SetActive(true);
        }
    }

    public void ResetCombo()
    {
        currentCombo = 0;
        txtCombo.text = "0";
        txtCombo.gameObject.SetActive(false);
        goComboImage.SetActive(false);
    }

    public int GetCombo()
    {
        return currentCombo;
    }

    public void comboEffect()
    {
        if (currentCombo % 10 == 9)
            anim.SetTrigger("combo");
    }
}
