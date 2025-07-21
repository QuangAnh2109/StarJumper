using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownManager : MonoBehaviour
{
    [SerializeField] private Image backGround;
    [SerializeField] private int levelToUnlock;
    [SerializeField] private TextMeshProUGUI text;
    private float cooldownTimer;
    private float currentTimer;
    private bool isUnlock;

    private void Start()
    {
        if (text != null)
        {
            text.SetText(levelToUnlock.ToString());
        }
        else
        {
            Debug.LogWarning("text is null in SkillCooldownManager Start!");
        }
    }

    private void Update()
    {
        if (GameManager.instance != null && GameManager.instance.level >= levelToUnlock && !isUnlock)
        {
            isUnlock = true;
            if (backGround != null)
            {
                backGround.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("backGround is null in SkillCooldownManager!");
            }
            if (text != null)
            {
                text.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("text is null in SkillCooldownManager!");
            }
        }
        else if (GameManager.instance == null)
        {
            Debug.LogWarning("GameManager.instance is null in SkillCooldownManager!");
        }
    }

    public void SkillCooldown(float timer)
    {
        if (backGround == null)
        {
            Debug.LogWarning("backGround is null in SkillCooldown!");
            return;
        }
        backGround.gameObject.SetActive(true);
        cooldownTimer = timer;
        backGround.fillAmount = 1;
        currentTimer = timer;
        StartCoroutine(CheckCooldown());
    }

    IEnumerator CheckCooldown()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        currentTimer -= Time.deltaTime;
        if (backGround != null)
        {
            backGround.fillAmount = currentTimer / cooldownTimer;
        }
        if (currentTimer > 0)
        {
            StartCoroutine(CheckCooldown());
        }
        else
        {
            if (backGround != null)
            {
                backGround.gameObject.SetActive(false);
            }
        }
    }
}