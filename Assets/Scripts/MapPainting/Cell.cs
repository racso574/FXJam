using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    private int x;
    private int y;
    private blackboard board;
    private bool isLeftMouseButtonHeld = false;
    private bool isRightMouseButtonHeld = false;

    public void Initialize(int x, int y, blackboard board)
    {
        this.x = x;
        this.y = y;  // Corrección: asignar el valor correcto a y
        this.board = board;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            board.UpdateCell(x, y, true);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            board.UpdateCell(x, y, false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            board.UpdateCell(x, y, true);
        }
        else if (Input.GetMouseButton(1))
        {
            board.UpdateCell(x, y, false);
        }
    }
}
