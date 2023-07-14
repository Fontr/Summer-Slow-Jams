using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsChecking : MonoBehaviour
{
    private HPEvents hpEvents;
    void Start()
    {
        hpEvents = GetComponent<HPEvents>();
    }

    void Update()
    {
        //проверка событий нажатием кнопок
        if (Input.GetKey(KeyCode.Keypad0)) { hpEvents.HPLoss();}
        if (Input.GetKey(KeyCode.Keypad1)) { hpEvents.HPRecovery();}
    }
}
