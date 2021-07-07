using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine;

public class GoldPopUp : MonoBehaviour
{
    private TMP_Text goldText;
    private float disappearTimer;
    private Color textColor;
    public AudioManager audioManagerPopup;

    public void Awake()
    {
        audioManagerPopup = GetComponent<AudioManager>();
        goldText = transform.GetComponent<TMP_Text>();
    }

    private class popUpTiers
    {
        public float goldTier;
        public float FontSize;
        public float DisappearTime;
        public Color FontColor;
        public string AudioName;

    }

    popUpTiers[] popUpData = new popUpTiers[]{
            new popUpTiers{goldTier= 0, FontSize= 45f, DisappearTime = 1f,
                FontColor = new Color(0.4528302f, 0.2902939f, 0.07048772f, 1), AudioName = null },

            new popUpTiers{goldTier= 100, FontSize= 50f, DisappearTime = 1f,
                FontColor = new Color(0.8396226f, 0.3766585f, 0.122775f, 1), AudioName = "Small" },

            new popUpTiers{goldTier= 300, FontSize= 100, DisappearTime = 4f,
                FontColor = new Color(0.8021093f, 0.9716981f, 0.9504475f,1), AudioName = "Medium" },

            new popUpTiers{goldTier= 1000, FontSize= 150f, DisappearTime = 4f,
                FontColor = new Color(1f, .8796226f, 0.6038275f, 1), AudioName = "Large" }
        };

    public void Setup(float goldAmount)
    {

        goldText.SetText(goldAmount.ToString("c2"));
        var popUpInformation = popUpData.FirstOrDefault(d => goldAmount <= d.goldTier);
        if (popUpInformation == null)
        {
            disappearTimer = 4f;
            goldText.faceColor = new Color(0f, 1, 0.9085407f, 1);
            textColor = goldText.faceColor;
            goldText.fontSize = 200f;
            audioManagerPopup.Play("SuperLarge");
            return;
        }
        disappearTimer = popUpInformation.DisappearTime;
        goldText.faceColor = popUpInformation.FontColor;
        textColor = goldText.faceColor;
        goldText.fontSize = popUpInformation.FontSize;
        if(popUpInformation.AudioName == null)
        {
            return;
        }
        audioManagerPopup.Play(popUpInformation.AudioName);

    }



    // Update is called once per frame
    void Update()
    {
        MoveUpwards();
        Disappear();
    }

    private void MoveUpwards()
    {
        var moveYSpeed = 20f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
    }

    void Disappear()
    {
        disappearTimer -= Time.deltaTime;
        if (disappearTimer >= 0)
        {
            return;
        }

        var dissapearSpeed = 3f;
        textColor.a -= dissapearSpeed * Time.deltaTime;
        goldText.color = textColor;
        if (textColor.a < 0)
        {
            Destroy(gameObject);
        }
    }
}
