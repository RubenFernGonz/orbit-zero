using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ElevatorGrid : MonoBehaviour
{
    [SerializeField]
    GameObject PlanePrefab;
    [SerializeField]
    Transform elevatorObject;
    [SerializeField]
    public int desiredWidth = 5, desiredHeight = 5;
     public int width;
    public int height;
    public float cellWidth;
    public float cellHeight;
    public ElevatorCell[,] grid;
    // Start is called before the first frame update
    void Start()
    {



        if (elevatorObject != null)
        {

            cellWidth = Mathf.RoundToInt(elevatorObject.transform.localScale.x * 10f/desiredWidth);

            cellHeight = Mathf.RoundToInt(elevatorObject.transform.localScale.z * 10f / desiredHeight);

            
        }
        else
        {
            Debug.Log("No has añadido tu superficie del elevador!");
        }

        Debug.Log(cellWidth+"Height :"+cellHeight);
        CreateGrid();
    }

    // Iniciar el Grid en base al tamaño de la superficie de un plano, cada casilla sera un ElevatorCell (mirar clase)
    void CreateGrid()
    {
    
    grid = new ElevatorCell[width, height];
    float startX = -width / 2 + cellWidth / 2;
    float startZ = -height / 2 + cellHeight / 2;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                GameObject childPlane = Instantiate(PlanePrefab, transform);
                
                float posX = startX + x * cellWidth;
                float posZ = startZ + y * cellHeight;

                grid[x, y] = new ElevatorCell();
                grid[x, y].matrixPosition = new Vector2(x, y);
                grid[x, y].inGamePosition = new Vector2(posX, posZ);

                childPlane.transform.localScale = new Vector3(cellWidth / 10f, 1, cellHeight / 10f);
                childPlane.transform.position = new Vector3(posX, childPlane.transform.position.y+5, posZ);

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
                Vector3 cellWorldPos = new Vector3(x * cellWidth, 0, y * cellHeight);
                Debug.Log(cellWorldPos);
            Gizmos.DrawWireCube(cellWorldPos + Vector3.one * cellWidth / 2f, Vector3.one * cellHeight);
        }
    }
}
    // Update is called once per frame
    void Update()
    {
        
    }
}
