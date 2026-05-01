using System.Collections.Generic;
using UnityEngine;

public class GridBlock : MonoBehaviour
{
    public LayerMask wallMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float distanceBetweenNodes;

    GridNode[,] nodeGrid;
    public List<GridNode> finalPath;
    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
    }

    // Karena pipa bergerak, kita perlu update grid secara berkala
    void Update()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        nodeGrid = new GridNode[gridSizeX, gridSizeY];
        Vector2 bottomLeft = (Vector2)transform.position - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = bottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
                bool isWall = Physics2D.OverlapCircle(worldPoint, nodeRadius, wallMask);
                nodeGrid[x, y] = new GridNode(isWall, worldPoint, x, y);
            }
        }
    }

    public List<GridNode> GetNeighboringNodes(GridNode node)
    {
        List<GridNode> neighbors = new List<GridNode>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    neighbors.Add(nodeGrid[checkX, checkY]);
            }
        }
        return neighbors;
    }

    public GridNode NodeFromWorldPoint(Vector2 worldPos)
    {
        float xPos = ((worldPos.x - transform.position.x) + gridWorldSize.x / 2) / gridWorldSize.x;
        float yPos = ((worldPos.y - transform.position.y) + gridWorldSize.y / 2) / gridWorldSize.y;
        xPos = Mathf.Clamp01(xPos);
        yPos = Mathf.Clamp01(yPos);
        int x = Mathf.RoundToInt((gridSizeX - 1) * xPos);
        int y = Mathf.RoundToInt((gridSizeY - 1) * yPos);
        return nodeGrid[x, y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));
        if (nodeGrid != null)
        {
            foreach (GridNode n in nodeGrid)
            {
                Gizmos.color = n.isWall ? Color.red : Color.white;
                if (finalPath != null && finalPath.Contains(n)) Gizmos.color = Color.black;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - distanceBetweenNodes));
            }
        }
    }
}