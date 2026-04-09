using UnityEngine.Events;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private float inputX;
    private float inputY;
    public UnityEvent<Vector2> onInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }


    // Update is called once per frame
       // Update is called once per frame
    void Update()
    {
        inputY = Input.GetAxis("Vertical");
        inputX = Input.GetAxis("Horizontal");
        Vector2 vec2 = new Vector2(inputX, inputY).normalized;
        onInput.Invoke(vec2);
        //Debug.Log(inputX + "," + inputY);
    }
}
