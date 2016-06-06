using UnityEngine;
using UnityEngine.UI;

public class PointsIndicator : MonoBehaviour {

    private static RectTransform pointsBarRect;

    void Start()
    {
        pointsBarRect = transform.Find("PointsBar").GetComponent<RectTransform>();
    }

    public static void SetPoints(int _cur, int _max)
    {
        float _value = (float)_cur / _max;

        pointsBarRect.GetComponent<Image>().color = new Color(0.0f, 0.58f, 0.0f, 1.0f);
        pointsBarRect.localScale = new Vector3(_value, pointsBarRect.localScale.y, pointsBarRect.localScale.z);
    }
}
