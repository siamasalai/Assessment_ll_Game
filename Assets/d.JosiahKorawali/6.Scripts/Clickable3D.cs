using UnityEngine;
using UnityEngine.Events;

public class Clickable3D : MonoBehaviour
{
    public UnityEvent OnClick;

    // This runs when the mouse clicks the object's collider
    void OnMouseDown()
    {
        OnClick.Invoke();
    }
}