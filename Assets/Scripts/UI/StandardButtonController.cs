using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StandardButtonController : MonoBehaviour
{
    [Header("Button Objects and Components")]
    [SerializeField] private GameObject buttonObject;
    [SerializeField] private Image buttonImage;
    [SerializeField] private Sprite enabledImage, selectedImage, submitedImage, disabledImage;
    [SerializeField] private TextMeshProUGUI buttonText;

    [Header("Scaling Factors")]
    [SerializeField] private Vector3 selectScale = new Vector3(1.1f, 1.1f, 0f);
    [SerializeField] private Vector3 submitScale = new Vector3(.95f, .95f, 0f);
    private Vector3 originalScale = new Vector3(1f, 1f, 1f);

    [Header("Action Effects")]
    [SerializeField] private float submitDelay = .15f;
    [SerializeField] private ButtonAction buttonAction;
    [SerializeField] private MainMenuController mainMenuController;
    [SerializeField] private MapController mapController;

    [Header("Movement Factors")]
    [SerializeField] private float moveDistance = 10f;
    [SerializeField] private float moveSpeed = 2f;
    private Vector3 originalPosition;


    private void Start()
    {
        //originalScale = transform.localScale;

        originalPosition = transform.localPosition;
    }

    public void OnSelectMove()
    {
        StopAllCoroutines();
        StartCoroutine(MoveButton(originalPosition + new Vector3(0, moveDistance, 0)));
    }

    public void OnDeselectMove()
    {
        StopAllCoroutines();
        StartCoroutine(MoveButton(originalPosition));
    }

    public void OnSubmitMove()
    {
        StopAllCoroutines();
        StartCoroutine(MoveButton(originalPosition - new Vector3(0, moveDistance, 0), true));

        if (mapController != null || mainMenuController != null && buttonAction != null)
        {
            StartCoroutine(DelayedSubmitCoroutine());
        }
    }

    public void OnSelect()
    {
        buttonImage.sprite = selectedImage;
        SetTextColor(GameController.Instance.uiController.textColorSelected);

        transform.localScale = selectScale;
    }

    public void OnDeselect()
    {
        buttonImage.sprite = enabledImage;
        SetTextColor(GameController.Instance.uiController.textColorEnabled);

        transform.localScale = originalScale;
    }

    public void OnSubmit()
    {
        buttonImage.sprite = submitedImage;
        SetTextColor(GameController.Instance.uiController.textColorPressed);

        transform.localScale = submitScale;

        if (mainMenuController != null || mapController != null && buttonAction != null)
        {
            StartCoroutine(DelayedSubmitCoroutine());
        }
        else
        {
            Debug.LogError($"{nameof(OnSubmit)} in {nameof(StandardButtonController)}: {nameof(mainMenuController)} & or {nameof(buttonAction)} not assigned in Inspector");
        }
    }

    public void OnButtonDisable()
    {
        buttonImage.sprite = disabledImage;
        SetTextColor(GameController.Instance.uiController.textColorDisabled);

        transform.localScale = originalScale;
    }

    private void SetTextColor(Color newColor)
    {
        if (buttonText != null)
        {
            buttonText.color = newColor;
        }
    }

    private IEnumerator DelayedSubmitCoroutine()
    {
        yield return new WaitForSeconds(submitDelay);

        if (mainMenuController != null)
        {
            mainMenuController.ExecuteButtonAction(buttonAction.buttonActionName);
        }
        else if (mapController != null)
        {
            mapController.ExecuteButtonAction(buttonAction.buttonActionName);
        }
        else
        {
            Debug.LogError($"{nameof(DelayedSubmitCoroutine)} in {nameof(StandardButtonController)}: {nameof(MapController)} & or {nameof(buttonAction)} not assigned in Inspector");
        }
    }

    private IEnumerator MoveButton(Vector3 targetPosition, bool onSubmit = false)
    {
        while (Vector3.Distance(transform.localPosition, targetPosition) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
            if (onSubmit)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, submitScale, moveSpeed * Time.deltaTime);
            }
            yield return null;
        }

        if (onSubmit)
        {
            transform.localScale = submitScale; // Ensure scale is set to submitScale after movement
        }
    }
}

