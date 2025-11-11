using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ElevatorCell
{
    public Vector2 matrixPosition;
    public bool occupied = false;
    public Vector2 inGamePosition;

    public bool isEmpty() { return occupied; }
    public void changeState() { occupied = !occupied; }
    




}
