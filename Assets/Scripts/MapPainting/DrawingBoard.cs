using UnityEngine;
using System.Collections.Generic;

public class DrawingBoard : MonoBehaviour
{
    public Camera mainCamera;
    public Material lineMaterial;
    public float lineWidth = 0.1f;

    private List<LineRenderer> lines = new List<LineRenderer>();
    private LineRenderer currentLine;
    private List<Vector3> currentLinePositions = new List<Vector3>();
    private bool isDrawing = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }
        if (Input.GetMouseButton(0))
        {
            Draw();
        }
        if (Input.GetMouseButtonUp(0))
        {
            EndDrawing();
        }
        if (Input.GetMouseButtonDown(1))
        {
            Erase();
        }
    }

    void StartDrawing()
    {
        currentLine = new GameObject("Line").AddComponent<LineRenderer>();
        currentLine.material = lineMaterial;
        currentLine.startWidth = lineWidth;
        currentLine.endWidth = lineWidth;
        currentLine.useWorldSpace = true;

        currentLinePositions.Clear();
        isDrawing = true;
    }

    void Draw()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // Ajusta esto según la distancia de la cámara a la pizarra
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

        if (currentLinePositions.Count == 0 || Vector3.Distance(currentLinePositions[currentLinePositions.Count - 1], worldPos) > 0.1f)
        {
            currentLinePositions.Add(worldPos);
            currentLine.positionCount = currentLinePositions.Count;
            currentLine.SetPositions(currentLinePositions.ToArray());

            if (currentLinePositions.Count > 1)
            {
                // Añadir un BoxCollider para cada segmento de la línea
                Vector3 startPos = currentLinePositions[currentLinePositions.Count - 2];
                Vector3 endPos = currentLinePositions[currentLinePositions.Count - 1];
                AddColliderToSegment(startPos, endPos);
            }
        }
    }

    void EndDrawing()
    {
        lines.Add(currentLine);
        isDrawing = false;
    }

    void Erase()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            LineRenderer lineToDelete = hit.collider.GetComponentInParent<LineRenderer>();
            if (lineToDelete != null)
            {
                lines.Remove(lineToDelete);
                Destroy(lineToDelete.gameObject);
            }
        }
    }

    void AddColliderToSegment(Vector3 startPos, Vector3 endPos)
    {
        GameObject segmentCollider = new GameObject("SegmentCollider");
        segmentCollider.transform.parent = currentLine.transform;

        BoxCollider boxCollider = segmentCollider.AddComponent<BoxCollider>();
        Vector3 midPoint = (startPos + endPos) / 2;
        float segmentLength = Vector3.Distance(startPos, endPos);

        boxCollider.size = new Vector3(lineWidth, lineWidth, segmentLength);
        segmentCollider.transform.position = midPoint;
        segmentCollider.transform.LookAt(endPos);
    }
}
