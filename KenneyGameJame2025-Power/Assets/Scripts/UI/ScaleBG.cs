using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ScaleBG : MonoBehaviour
{
    public RectTransform targetContent;
    public Vector2 padding = new Vector2(20f, 20f);

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        Vector2 size = targetContent.sizeDelta;

        rectTransform.sizeDelta = size + padding;
        rectTransform.position = targetContent.position;
    }
}
