using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
    class Repeater
    {
        const float threshold = 0.5f;
        const float rate = 0.25f;
        float _next;
        bool _hold;
        string _axis;

        public Repeater(string axisName)
        {
            _axis = axisName;
        }

        // This is just a regular class
        // trigger this Update manually
        public int Update()
        {
            int retValue = 0;
            int value = Mathf.RoundToInt(Input.GetAxisRaw(_axis));
            // when there is user input on _axis
            if (value != 0)
            {
                // when sufficient time has passed to
                // allow another input event
                // When button is first pressed, event is allowed immediately
                if (Time.time > _next)
                {
                    retValue = value;
                    _next = Time.time + (_hold ? rate : threshold);
                    _hold = true;
                }
                // Basically triggers movement when:
                // the button is first pressed
                // first 0.5 passed
                // then every 0.25 passed
            }
            // when no user input on _axis
            // reset
            else
            {
                _hold = false;
                _next = 0;
            }
            return retValue;
        }
    }
    
    Repeater _hor = new Repeater("Horizontal");
    Repeater _ver = new Repeater("Vertical");

    public static event EventHandler<InfoEventArgs<Point>> moveEvent;
    public static event EventHandler<InfoEventArgs<int>> fireEvent;
    string[] _buttons = new string[]{"Fire1", "Fire2", "Fire3"};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    int x = _hor.Update();
        int y = _ver.Update();
        if (x != 0 || y != 0)
        {
            if (moveEvent != null)
                moveEvent(this, new InfoEventArgs<Point>(new Point(x, y)));
        }
        for (int i = 0; i < _buttons.Length; ++i)
        {
            if (Input.GetButtonUp(_buttons[i]))
            {
                if (fireEvent != null)
                    fireEvent(this, new InfoEventArgs<int>(i));
            }
        }
    }
}
