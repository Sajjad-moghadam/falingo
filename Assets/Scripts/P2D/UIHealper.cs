using UnityEngine;
using System.Collections;

public class UIHealper  {

    public static void SetAnchorOnObject(RectTransform rectTransform,RectTransform parentRect)
    {
        Vector2 newAnchorsMin = new Vector2(rectTransform.anchorMin.x + rectTransform.offsetMin.x / parentRect.rect.width,
                                           rectTransform.anchorMin.y + rectTransform.offsetMin.y / parentRect.rect.height);
        Vector2 newAnchorsMax = new Vector2(rectTransform.anchorMax.x + rectTransform.offsetMax.x / parentRect.rect.width,
                                            rectTransform.anchorMax.y + rectTransform.offsetMax.y / parentRect.rect.height);

        rectTransform.anchorMin = newAnchorsMin;
        rectTransform.anchorMax = newAnchorsMax;
        rectTransform.offsetMin = rectTransform.offsetMax = new Vector2(0, 0);
    }

}