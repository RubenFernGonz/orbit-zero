using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ElevatorGrid : MonoBehaviour
{
    [SerializeField]
    Transform elevatorObject;
    public int width = 5;
    public int height = 5;
    public float cellSize = 1f;
    Vector3 planeSize;
    public ElevatorCell[,] grid;
    // Start is called before the first frame update
    void Start()
    {



        if (elevatorObject != null)
        {

            width = Mathf.RoundToInt(elevatorObject.transform.localScale.x * 10f);

            height = Mathf.RoundToInt(elevatorObject.transform.localScale.z * 10f);

        }
        else
        {
            Debug.Log("No has añadido tu superficie del elevador!");
        }

        Debug.Log(width+"Height :"+height);
        CreateGrid();
    }

    // Iniciar el Grid en base al tamaño de la superficie de un plano, cada casilla sera un ElevatorCell (mirar clase)
    void CreateGrid()
    {
    
    grid = new ElevatorCell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = new ElevatorCell();
                grid[x, y].position = new Vector2(x, y);
            }
        }
        Debug.Log(grid[1,2]);
    }
    void OnDrawGizmos()
{
    // Crear el grid en editor si es null
    if (grid == null)
        CreateGrid();

    Gizmos.color = Color.green;

    for (int x = 0; x < width; x++)
    {
        for (int y = 0; y < height; y++)
        {
                Vector3 cellWorldPos = new Vector3(x * cellSize, 0, y * cellSize);
                Debug.Log(cellWorldPos);
            Gizmos.DrawWireCube(cellWorldPos + Vector3.one * cellSize / 2f, Vector3.one * cellSize);
        }
    }
}
    // Update is called once per frame
    void Update()
    {
        
    }
}
