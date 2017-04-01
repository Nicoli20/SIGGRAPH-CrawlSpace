using UnityEngine;
using UnityEngine.UI;

// Creates and manages the grid of hexes.
public class HexGrid : MonoBehaviour
{
    public int width = 6, height = 6; // Grid size
    public HexCell cellPrefab; // Each hex is made of this.
    public Text cellLabelPrefab; // Labels the coordinates for each hex.
    public Color defaultColor = Color.white; // The default color for a hex.
    public Color touchedColor = Color.magenta; // The color a hex changes to when clicked.

    private Canvas gridCanvas; // The canvas object used to display UI elements upon the grid.
    private HexCell[] cells; // The array of hexes that make up the grid.
    private HexMesh hexMesh; // The mesh of the grid. Texture, collision, etc.

    // Called during game initialization to set the canvas, mesh, and cells.
    private void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();

        cells = new HexCell[height * width];
        // Create new cells for the entire grid.
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    // Called after game initialization to form the triangles that make up each hexcell.
    private void Start()
    {
        hexMesh.Triangulate(cells);
    }

    // Creates a hexcell.
    private void CreateCell(int x, int z, int i)
    {
        // Coordinates are set based on hex metrics.
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        // Instantiate, position, and color the cell.
        HexCell cell = cells[i] = Instantiate(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        cell.color = defaultColor;

        // Set the cell's neighbors.
        if (x > 0) // All cells expect the first of the row.
        {
            cell.SetNeighbor(HexDirection.W, cells[i - 1]);
        }
        if (z > 0) // Cells not in the first row.
        {
            if ((z & 1) == 0) // Even rows.
            {
                cell.SetNeighbor(HexDirection.SE, cells[i - width]);
                if (x > 0) // All cells expect the first of the row.
                {
                    cell.SetNeighbor(HexDirection.SW, cells[i - width - 1]);
                }
            }
            else // Odd rows.
            {
                cell.SetNeighbor(HexDirection.SW, cells[i - width]);
                if (x < width - 1) // All cells expect the last of the row.
                {
                    cell.SetNeighbor(HexDirection.SE, cells[i - width + 1]);
                }
            }
        }      

        // Instantiate, position, and write the cell's label.
        Text label = Instantiate(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition =
            new Vector2(position.x, position.z);
        label.text = cell.coordinates.ToStringOnSeparateLines();
    }

    // Recolor the chosen cell, then re-triangulate all cells.
    public void ColorCell(Vector3 position, Color color)
    {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
        HexCell cell = cells[index];
        cell.color = color;
        hexMesh.Triangulate(cells);
    }
}
