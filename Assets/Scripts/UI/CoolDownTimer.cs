using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownTimer : MonoBehaviour
{

    [SerializeField]
    private float coolDownTime;

    [HideInInspector]
    public float timerValue;

    [HideInInspector]
    public Image image;

    [HideInInspector]
    public bool startTimer;

    public TextMeshProUGUI countDown;

    Button thisButton;

    ActivateAttack activateAttack;

    public HotBarKeys spellIsReady;

    void Start()
    {
        image = this.GetComponent<Image>();
        
        activateAttack = transform.parent.GetComponent<ActivateAttack>();

        thisButton = transform.parent.GetComponent<Button>();

        spellIsReady = this.GetComponentInParent<HotBarKeys>();

        startTimer = false;

        countDown.enabled = false;

        timerValue = 0;
        image.fillAmount = 0;
    }

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        if (startTimer)
        {

            activateAttack.CheckForHotbarSlot();

            timerValue -= Time.deltaTime / coolDownTime;

            spellIsReady.isReady = false;

            thisButton.enabled = false;

            activateAttack.enabled = false;

            countDown.enabled = true;

             // Calculate remaining time as an integer
            int remainingTime = Mathf.CeilToInt(coolDownTime * timerValue);
            countDown.text = remainingTime.ToString();

            if (timerValue <= 0)
            {
                Debug.Log("Spell ready to use");

                if (spellIsReady != null)
                {
                    countDown.enabled = false;

                    spellIsReady.isReady = true;

                    this.image.enabled = false;

                    thisButton.enabled = true;

                    activateAttack.enabled = true;

                    startTimer = false;
                }

            }

            image.fillAmount = timerValue;
        }

        //Debug.Log(timerValue);
    }
}
