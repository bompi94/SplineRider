using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnHoverDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    GameObject textBoxPanel;

    [SerializeField]
    float timeToShowTextbox;

    [SerializeField]
    string description;

    [SerializeField]
    bool up;

    bool over;
    bool shouldCount = true;

    float counter;

    float myWidth;
    float myHeight;
    RectTransform myRectTransform;

    float tbWidth;
    float tbHeight;
    RectTransform tbRectTransform;

    Text tbText; 

    private void Awake()
    {
        tbText = textBoxPanel.GetComponentInChildren<Text>(); 

        myRectTransform = GetComponent<RectTransform>();
        myWidth = myRectTransform.rect.width;
        myHeight = myRectTransform.rect.height;

        tbRectTransform = textBoxPanel.GetComponent<RectTransform>();
        tbWidth = tbRectTransform.rect.width;
        tbHeight = tbRectTransform.rect.height; 
    }

    private void Update()
    {
        if (over && shouldCount)
        {
            counter += Time.deltaTime;
            if (counter >= timeToShowTextbox)
            {
                shouldCount = false;
                ShowAndPlaceTextBox(); 
            }
        }
    }

    void ShowAndPlaceTextBox()
    {
        textBoxPanel.SetActive(true);

        Vector3 corner;
        Vector3 diagonalOffset;
        if (up)
        {
            corner = myRectTransform.localPosition + new Vector3(myWidth / 2, myHeight / 2, 0);
            diagonalOffset = new Vector3(tbWidth / 2, tbHeight / 2, 0);
        }
        else
        {
            corner = myRectTransform.localPosition + new Vector3(myWidth / 2, -myHeight / 2, 0);
            diagonalOffset = new Vector3(tbWidth / 2, -tbHeight / 2, 0);
        }

        tbRectTransform.localPosition = corner + diagonalOffset;
        tbText.text = description; 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        over = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        over = false;
        shouldCount = true; 
        counter = 0;
        textBoxPanel.SetActive(false);
    }
}
