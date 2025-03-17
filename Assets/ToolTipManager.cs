using TMPro;
using UnityEngine;

public class ToolTipManager : MonoBehaviour
{
    public Canvas parentCanvas;
    public Transform ToolTipTransform;
    public static ToolTipManager TpInstance;
    public CanvasGroup ToolTipCanvasGroup;
    public TMP_Text Title, Detail;

    bool isShowing;
    void Start()
    {
        TpInstance = this;
        isShowing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShowing)
        {
            if(ToolTipCanvasGroup.alpha < 1)
            {
                ToolTipCanvasGroup.alpha += Time.deltaTime * 3;
            }
        }
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, Input.mousePosition, parentCanvas.worldCamera, out movePos);

        ToolTipTransform.position = parentCanvas.transform.TransformPoint(movePos);
    }

    public void Show(string TitleText, string DetailText)
    {
        ToolTipCanvasGroup.alpha = 0;
        Title.text = TitleText;
        Detail.text = DetailText;
        ToolTipTransform.gameObject.SetActive(true);
        isShowing = true;

    }
    public void Hide()
    {
        ToolTipTransform.gameObject.SetActive(false);
        isShowing = false;
    }
}
