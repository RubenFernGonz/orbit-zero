using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ElevatorCell
{
    public Vector2 position;
    public bool occupied = false;

    public bool isEmpty() { return occupied; }
    public void changeState() { occupied = !occupied; }
    




}
