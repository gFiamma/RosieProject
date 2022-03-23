using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProvaComandi : MonoBehaviour            //Codice per testare i comandi di ogni singolo tasto del controller (bisogna attivare lo script assegnato al Player)
{
    //variabile che gestisce gli input
    private InputManager inputManager;
    //nell'awake assegnamo l'inputMaster allo script
    private void Awake()
    {
        inputManager = new InputManager();
    }
    //quando viene abilitato avvia l'inputManager
    private void OnEnable()
    {
        inputManager.Enable();
    }
    //quando viene disabilitato disattiva l'inputManager
    private void OnDisable()
    {
        inputManager.Disable();
    }

    void Update()
    {
        //A======================================================================================================================
        if (inputManager.Input.Cross.ReadValue<float>() >= 0.2f)
        {
            if (inputManager.Input.Cross.IsPressed())
            {
                Debug.Log("A");
                Gamepad.current.SetMotorSpeeds(1f, 1f);
            }
        }else if (inputManager.Input.Cross.ReadValue<float>() <= 0.1f)
        {
            if (inputManager.Input.Cross.WasReleasedThisFrame())
            {
                Debug.Log("A Relased");
                Gamepad.current.SetMotorSpeeds(0f, 0f);
            }
        }
        //B======================================================================================================================
        if (inputManager.Input.Circle.ReadValue<float>() >= 0.2f)
        {
            if (inputManager.Input.Circle.IsPressed())
            {
                Debug.Log("B");
                Gamepad.current.SetMotorSpeeds(1f, 1f);
            }
        }
        else if (inputManager.Input.Circle.ReadValue<float>() <= 0.1f)
        {
            if (inputManager.Input.Circle.WasReleasedThisFrame())
            {
                Debug.Log("B Relased");
                Gamepad.current.SetMotorSpeeds(0f, 0f);
            }
        }
        //X======================================================================================================================
        if (inputManager.Input.Square.ReadValue<float>() >= 0.2f)
        {
            if (inputManager.Input.Square.IsPressed())
            {
                Debug.Log("X");
                Gamepad.current.SetMotorSpeeds(1f, 1f);
            } 
        }
        else if (inputManager.Input.Square.ReadValue<float>() <= 0.1f)
        {
            if (inputManager.Input.Square.WasReleasedThisFrame())
            {
                Debug.Log("X Relased");
                Gamepad.current.SetMotorSpeeds(0f, 0f);
            }
        }
        //Y======================================================================================================================
        if (inputManager.Input.Triangle.ReadValue<float>() >= 0.2f)
        {
            if (inputManager.Input.Triangle.IsPressed())
            {
                Debug.Log("Y");
                Gamepad.current.SetMotorSpeeds(1f, 1f);
            }
        }
        else if (inputManager.Input.Triangle.ReadValue<float>() <= 0.1f)
        {
            if (inputManager.Input.Triangle.WasReleasedThisFrame())
            {
                Debug.Log("Y Relased");
                Gamepad.current.SetMotorSpeeds(0f, 0f);
            }
        }
        //Start======================================================================================================================
        if (inputManager.Input.Options.ReadValue<float>() >= 0.2f)
        {
            if (inputManager.Input.Options.IsPressed())
            {
                Debug.Log("Start");
                Gamepad.current.SetMotorSpeeds(1f, 1f);
            }
        }
        else if (inputManager.Input.Options.ReadValue<float>() <= 0.1f)
        {
            if (inputManager.Input.Options.WasReleasedThisFrame())
            {
                Debug.Log("Start Relased");
                Gamepad.current.SetMotorSpeeds(0f, 0f);
            }
        }
        //Select======================================================================================================================
        if (inputManager.Input.TouchpadPress.ReadValue<float>() >= 0.2f)
        {
            if (inputManager.Input.TouchpadPress.IsPressed())
            {
                Debug.Log("Select");
                Gamepad.current.SetMotorSpeeds(1f, 1f);
            }
        }
        else if (inputManager.Input.TouchpadPress.ReadValue<float>() <= 0.1f)
        {
            if (inputManager.Input.TouchpadPress.WasReleasedThisFrame())
            {
                Debug.Log("Select Relased");
                Gamepad.current.SetMotorSpeeds(0f, 0f);
            }
        }
        //RB======================================================================================================================
        if (inputManager.Input.R1.ReadValue<float>() >= 0.2f)
        {
            if (inputManager.Input.R1.IsPressed())
            {
                Debug.Log("RB");
                Gamepad.current.SetMotorSpeeds(1f, 1f);
            }
        }
        else if (inputManager.Input.R1.ReadValue<float>() <= 0.1f)
        {
            if (inputManager.Input.R1.WasReleasedThisFrame())
            {
                Debug.Log("RB Relased");
                Gamepad.current.SetMotorSpeeds(0f, 0f);
            }
        }
        //LB======================================================================================================================
        if (inputManager.Input.L1.ReadValue<float>() >= 0.2f)
        {
            if (inputManager.Input.L1.IsPressed())
            {
                Debug.Log("LB");
                Gamepad.current.SetMotorSpeeds(1f, 1f);
            }
        }
        else if (inputManager.Input.L1.ReadValue<float>() <= 0.1f)
        {
            if (inputManager.Input.L1.WasReleasedThisFrame())
            {
                Debug.Log("LB Relased");
                Gamepad.current.SetMotorSpeeds(0f, 0f);
            }
        }
        //LT======================================================================================================================
        if (inputManager.Input.L2.ReadValue<float>() >= 0.2f)
        {
            if (inputManager.Input.L2.IsPressed())
            {
                Debug.Log("LT");
                Gamepad.current.SetMotorSpeeds(inputManager.Input.L2.ReadValue<float>(), inputManager.Input.L2.ReadValue<float>());
            }
        }else if (inputManager.Input.L2.ReadValue<float>() <= 0.1f)
        {
            if (inputManager.Input.L2.WasReleasedThisFrame())
            {
                Debug.Log("LT Relased");
                Gamepad.current.SetMotorSpeeds(0f, 0f);
            }
        }
        //RT======================================================================================================================
        if (inputManager.Input.R2.ReadValue<float>() >= 0.2f)
        {
            if (inputManager.Input.R2.IsPressed())
            {
                Debug.Log("RT");
                Gamepad.current.SetMotorSpeeds(inputManager.Input.R2.ReadValue<float>(), inputManager.Input.R2.ReadValue<float>());
            }
        }
        else if (inputManager.Input.R2.ReadValue<float>() <= 0.1f)
        {
            if (inputManager.Input.R2.WasReleasedThisFrame())
            {
                Debug.Log("RT Relased");
                Gamepad.current.SetMotorSpeeds(0f, 0f);
            }
        }
        //LStickPressed======================================================================================================================

        if (inputManager.Input.L3.ReadValue<float>() >= 0.2f)
        {
            if (inputManager.Input.L3.IsPressed())
            {
                Debug.Log("analogico sinistra premuto");
                Gamepad.current.SetMotorSpeeds(1f, 1f);
            }
        }
        else if (inputManager.Input.L3.ReadValue<float>() <= 0.1f)
        {
            if (inputManager.Input.L3.WasReleasedThisFrame())
            {
                Debug.Log("analogico sinistra rilasciato");
                Gamepad.current.SetMotorSpeeds(0f, 0f);
            }
        }
        //RStickPressed======================================================================================================================
        if (inputManager.Input.R3.ReadValue<float>() >= 0.2f)
        {
            if (inputManager.Input.R3.IsPressed())
            {
                Debug.Log("analogico destra premuto");
                Gamepad.current.SetMotorSpeeds(1f, 1f);
            }
        }
        else if (inputManager.Input.R3.ReadValue<float>() <= 0.1f)
        {
            if (inputManager.Input.R3.WasReleasedThisFrame())
            {
                Debug.Log("analogico destra rilasciato");
                Gamepad.current.SetMotorSpeeds(0f, 0f);
            }
        }
        //LStickLeftRight======================================================================================================================
        var movement = inputManager.Input.MovementInput.ReadValue<Vector2>().x;

        if (inputManager.Input.MovementInput.ReadValue<Vector2>().x != 0f)
        {
            if (movement > 0 || movement < 0)
            {
                Debug.Log("analogicoSX destra e sinistra");
            }
        }
        else if (inputManager.Input.MovementInput.ReadValue<Vector2>().x == 0f) 
        {
            if (movement == 0)
            {
                Debug.Log("analogicoSX fermo");
            }
        }
        //LStickUpDown======================================================================================================================
        var movement2 = inputManager.Input.MovementInput.ReadValue<Vector2>().y;
        if (movement2 > 0 || movement2 < 0)
        {
            Debug.Log("analogico sopra e sotto");
        }
        //ArrowLeftRight======================================================================================================================
        var xArrow = inputManager.Input.Arrow.ReadValue<Vector2>().x;
        if (xArrow > 0 || xArrow < 0)
        {
            Debug.Log("Frecce destra e sinistra");
        }
        //ArrowUpDown======================================================================================================================
        var yArrow = inputManager.Input.Arrow.ReadValue<Vector2>().y;
        if (yArrow > 0 || yArrow < 0)
        {
            Debug.Log("Frecce sopra e sotto");
        }
        //RStickLeftRight======================================================================================================================
        var mouseX = inputManager.Input.VisualMovement.ReadValue<Vector2>().x;
        if (mouseX > 0 || mouseX < 0)
        {
            Debug.Log("analogicoDX destra e sinistra");
        }
        //RStickUpDown======================================================================================================================
        var mouseY = inputManager.Input.VisualMovement.ReadValue<Vector2>().y;
        if (mouseY > 0 || mouseY < 0)
        {
            Debug.Log("analogicoDX sopra e sotto");
        }
    }
}
