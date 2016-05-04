using UnityEngine;
using System.Collections;

public class PointsIndicator : MonoBehaviour {

    private static RectTransform pointsBarRect;

    void Start()
    {
        pointsBarRect = transform.Find("PointsBar").GetComponent<RectTransform>();

        if (pointsBarRect == null)
        {
            Debug.LogError("POINTS INDICATOR: No PointsBar object!");
        }
    }

    public static void SetPoints(int _cur, int _max)
    {
        float _value = (float)_cur / _max;

        pointsBarRect.localScale = new Vector3(_value, pointsBarRect.localScale.y, pointsBarRect.localScale.z);
    }
}
