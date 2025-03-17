using UnityEngine;
using UnityEngine.EventSystems;
public class ToolTipDetail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string TitleText;
    public string DetailsText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
public void OnPointerEnter(PointerEventData eventData)
    {
        ToolTipManager.TpInstance.Show(TitleText,DetailsText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipManager.TpInstance.Hide();
    }
}
