using UnityEngine;
using UnityEngine.EventSystems;

// Allows for in-game editing of the hex grid.
public class HexMapEditor : MonoBehaviour
{
    public Color[] colors; // List of possible colors for a hex.
    public HexGrid hexGrid; // The hex grid to be edited.

    private Color activeColor; // The color that is used to paint a hex in-game.

    // Ran during game initialization, selecting the default color.
    void Awake()
    {
        SelectColor(0);
    }

    // Constantly checks for user input.
    void Update()
    {
        if (Input.GetMouseButton(0) &&
            !EventSystem.current.IsPointerOverGameObject())
        {
            HandleInput();
        }
    }

    // Raycasts upon whatever is clicked. A clicked hex is modified.
    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            hexGrid.ColorCell(hit.point, activeColor);
        }
    }

    // Switches the active color.
    public void SelectColor(int index)
    {
        activeColor = colors[index];
    }
}
