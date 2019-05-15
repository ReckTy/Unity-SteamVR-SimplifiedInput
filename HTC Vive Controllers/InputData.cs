using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class InputData
{
    #region Variables

    #region SteamVR_Actions <You might want to edit these>

    // Vector2 set to [Trackpad/USE-AS-TRACKPAD/Position] 
    private readonly SteamVR_Action_Vector2 m_TouchPosition = SteamVR_Actions._default.TouchpadTouch;

    // Boolean set to [Trackpad/USE-AS-BUTTON/Click]
    private readonly SteamVR_Action_Boolean m_TouchpadState = SteamVR_Actions._default.TouchpadDown;

    // Boolean set to [Trigger/USE-AS-BUTTON/Click]
    private readonly SteamVR_Action_Boolean m_TriggerState = SteamVR_Actions._default.GrabPinch;

    // Boolean set to [Grip/USE-AS-BUTTON/Click]
    private readonly SteamVR_Action_Boolean m_GrabState = SteamVR_Actions._default.GrabGrip;

    // Boolean set to [Menu/USE-AS-BUTTON/Click]
    private readonly SteamVR_Action_Boolean m_MenuState = SteamVR_Actions._default.Menu;

    #endregion

    /// <summary>
    /// Has to be set to either 'SteamVR_Input_Sources.LeftHand' or 'SteamVR_Input_Sources.RightHand'
    /// </summary>
    public SteamVR_Input_Sources InputSource = SteamVR_Input_Sources.Any;

    #region Touchpad

    #region Private

    private bool touchpad_Touched = false;
    private bool touchpad_Touch_Start = false;
    private bool touchpad_Touch_End = false;
    private int touchpad_Touch_ID = 0;
    private Vector2 touchpad_Touch_Position = new Vector2();

    private bool touchpad_Pressed = false;
    private bool touchpad_Pressed_Start = false;
    private bool touchpad_Pressed_End = false;
    private int touchpad_Pressed_ID = 0;

    #endregion

    #region Public

    public bool Touchpad_Touched { get => touchpad_Touched; }
    public bool Touchpad_Touch_Start { get => touchpad_Touch_Start; }
    public bool Touchpad_Touch_End { get => touchpad_Touch_End; }
    public int Touchpad_Touch_ID { get => touchpad_Touch_ID; }
    public Vector2 Touchpad_Touch_Position { get => touchpad_Touch_Position; }

    public bool Touchpad_Pressed { get => touchpad_Pressed; }
    public bool Touchpad_Pressed_Start { get => touchpad_Pressed_Start; }
    public bool Touchpad_Pressed_End { get => touchpad_Pressed_End; }
    public int Touchpad_Pressed_ID { get => touchpad_Pressed_ID; }

    #endregion

    #endregion

    #region Trigger

    #region Private

    private bool trigger_Pressed = false;
    private bool trigger_Pressed_Start = false;
    private bool trigger_Pressed_End = false;
    private int trigger_Pressed_ID = 0;

    #endregion

    #region Public

    public bool Trigger_Pressed { get => trigger_Pressed; }
    public bool Trigger_Pressed_Start { get => trigger_Pressed_Start; }
    public bool Trigger_Pressed_End { get => trigger_Pressed_End; }
    public int Trigger_Pressed_Pressed_ID { get => trigger_Pressed_ID; }

    #endregion

    #endregion

    #region Grip

    #region Private

    private bool grab_Pressed = false;
    private bool grab_Pressed_Start = false;
    private bool grab_Pressed_End = false;
    private int grab_Pressed_ID = 0;

    #endregion

    #region Public

    public bool Grab_Pressed { get => grab_Pressed; }
    public bool Grab_Pressed_Start { get => grab_Pressed_Start; }
    public bool Grab_Pressed_End { get => grab_Pressed_End; }
    public int Grab_Pressed_ID { get => grab_Pressed_ID; }

    #endregion

    #endregion

    #region Menu

    #region Private

    private bool menu_Pressed = false;
    private bool menu_Pressed_Start = false;
    private bool menu_Pressed_End = false;
    private int menu_Pressed_ID = 0;

    #endregion

    #region Public

    public bool Menu_Pressed { get => menu_Pressed; }
    public bool Menu_Pressed_Start { get => menu_Pressed_Start; }
    public bool Menu_Pressed_End { get => menu_Pressed_End; }
    public int Menu_Pressed_ID { get => menu_Pressed_ID; }

    #endregion

    #endregion

    #region Misc

    private bool last_Touchpad_Touched = false;

    #endregion

    #endregion

    /// <summary>
    /// Make sure 'InputSource' is set correctly before using this
    /// </summary>
    public void RefreshInput()
    {
        #region Touchpad

        // Check if 'InputSouce' is set
        if (!(InputSource == SteamVR_Input_Sources.LeftHand || InputSource == SteamVR_Input_Sources.RightHand)) return;

        // Reset necessary
        touchpad_Touch_Start = false;
        touchpad_Touch_End = false;

        // Touchpad_Touched
        touchpad_Touched = (touchpad_Touch_Position != Vector2.zero);

        // Touchpad_Touch_Start/End (EXPERIMENTAL)
        if (touchpad_Touched && !last_Touchpad_Touched)
        {
            last_Touchpad_Touched = true;

            // Set Touchpad_Touch_Start
            touchpad_Touch_Start = true;
        }
        else if (!touchpad_Touched && last_Touchpad_Touched)
        {
            // Set Touchpad_Touch_End
            touchpad_Touch_End = true;

            last_Touchpad_Touched = false;
        }

        // Touchpad_Touch_ID
        if (touchpad_Touch_Start)
            touchpad_Touch_ID++;

        // Touchpad_Touch_Position
        touchpad_Touch_Position = m_TouchPosition.GetAxis(InputSource);

        // Touchpad_Pressed
        touchpad_Pressed = m_TouchpadState[InputSource].state;

        // Touchpad_Pressed_Start
        touchpad_Pressed_Start = m_TouchpadState[InputSource].stateUp;

        // Touchpad_Pressed_End
        touchpad_Pressed_End = m_TouchpadState[InputSource].stateDown;

        // Touchpad_Pressed_ID
        if (touchpad_Pressed_Start)
            touchpad_Pressed_ID++;

        #endregion

        #region Trigger

        // Trigger_Pressed
        trigger_Pressed = m_TriggerState[InputSource].state;

        // Trigger_Pressed_Start
        trigger_Pressed_Start = m_TriggerState[InputSource].stateDown;

        // Trigger_Presed_End
        trigger_Pressed_End = m_TriggerState[InputSource].stateUp;

        // Trigger_Pressed_ID
        if (trigger_Pressed_Start)
            trigger_Pressed_ID++;

        #endregion

        #region Grab

        // Grab_Pressed
        grab_Pressed = m_GrabState.state;

        // Grab_Pressed_Start
        grab_Pressed_Start = m_GrabState.stateDown;

        // Grab_Pressed_End
        grab_Pressed_End = m_GrabState.stateUp;

        // Grab_Pressed_ID
        if (grab_Pressed_Start)
            grab_Pressed_ID++;

        #endregion

        #region Menu

        // Menu_Pressed
        menu_Pressed = m_MenuState[InputSource].state;

        // Menu_Pressed_Start
        menu_Pressed_Start = m_MenuState[InputSource].stateDown;

        // Menu_Pressed_End
        menu_Pressed_End = m_MenuState[InputSource].stateUp;

        // Menu_Pressed_ID
        if (menu_Pressed_Start)
            menu_Pressed_ID++;

        #endregion
    }

}
