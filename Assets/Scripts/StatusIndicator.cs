using UnityEngine;
using UnityEngine.UI;

public class StatusIndicator : MonoBehaviour {

    [SerializeField]
    private RectTransform helthBarRect;
    
    public void SetHealth(int _cur, int _max)
    {
        float _value = (float) _cur / _max;

        helthBarRect.localScale = new Vector3(_value, helthBarRect.localScale.y, helthBarRect.localScale.z);
    }
}
