using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MessageAcknowledgeButton : MonoBehaviour
{

    [SerializeField] private UnityEngine.UI.Image acknowlegeButtonImage;
    [SerializeField] private TextMeshProUGUI buttonText;

    [SerializeField] private Sprite acknowledgeEnabledImage;
    [SerializeField] private Sprite acknowledgeSelectedImage;
    [SerializeField] private Sprite acknowledgeSubmitImage;




    public void OnSelect()
    {
        acknowlegeButtonImage.sprite = acknowledgeSelectedImage;
        SetTextColor(GameController.Instance.uiController.textColorSelected);
        
    }

    public void OnDeselect()
    {
        acknowlegeButtonImage.sprite = acknowledgeEnabledImage;
        SetTextColor(GameController.Instance.uiController.textColorEnabled);
    }

    public void OnSubmit()
    {
        acknowlegeButtonImage.sprite = acknowledgeSubmitImage;
        SetTextColor(GameController.Instance.uiController.textColorPressed);
    }

    private void SetTextColor(Color newColor)
    {
        buttonText.color = newColor;
    }

}