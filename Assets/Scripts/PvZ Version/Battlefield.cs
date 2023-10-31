using UnityEngine;

public class Battlefield : MonoBehaviour
{
    public int rows = 5;
    public int columns = 9;
    public float cellSize = 1.0f;
    public GameObject gridCellPrefab;

    private GameObject[,] grid;

    private bool isSelectingLocation = false;
    private GameObject plantToPlace;

    void Start()
    {
        InitializeGrid();
    }

    void Update()
    {
        if (isSelectingLocation)
        {
            CheckForGridClick();
        }
    }

    void InitializeGrid()
    {
        grid = new GameObject[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3 position = new Vector3(j * cellSize, i * cellSize, 0);
                GameObject cell = Instantiate(gridCellPrefab, position, Quaternion.identity, this.transform);
                grid[i, j] = cell;
            }
        }
    }

    public void PrepareToPlacePlant(GameObject plantPrefab)
    {
        isSelectingLocation = true;
        plantToPlace = plantPrefab;
    }

    void CheckForGridClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                int row = Mathf.FloorToInt(hit.point.y / cellSize);
                int column = Mathf.FloorToInt(hit.point.x / cellSize);

                AddPlant(plantToPlace, row, column);
                isSelectingLocation = false;
            }
        }
    }

    public void AddPlant(GameObject plant, int row, int column)
    {
        if (grid[row, column] == null)
        {
            GameObject newPlant = Instantiate(plant, new Vector3(column * cellSize, row * cellSize, 0), Quaternion.identity, this.transform);
            grid[row, column] = newPlant;
        }
        else
        {
            Debug.LogError("Cell already occupied!");
        }
    }
}