using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    GridBlock grid;

    void Awake() { grid = GetComponent<GridBlock>(); }

    public List<GridNode> FindPath(Vector2 startPos, Vector2 targetPos)
    {
        GridNode startNode = grid.NodeFromWorldPoint(startPos);
        GridNode targetNode = grid.NodeFromWorldPoint(targetPos);

        List<GridNode> openList = new List<GridNode>();
        HashSet<GridNode> closedList = new HashSet<GridNode>();
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            GridNode currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].TotalCost < currentNode.TotalCost || openList[i].TotalCost == currentNode.TotalCost && openList[i].heuristicCost < currentNode.heuristicCost)
                    currentNode = openList[i];
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == targetNode) return RetracePath(startNode, targetNode);

            foreach (GridNode neighbor in grid.GetNeighboringNodes(currentNode))
            {
                if (neighbor.isWall || closedList.Contains(neighbor)) continue;

                int newCostToNeighbor = currentNode.moveCost + GetDistance(currentNode, neighbor);
                if (newCostToNeighbor < neighbor.moveCost || !openList.Contains(neighbor))
                {
                    neighbor.moveCost = newCostToNeighbor;
                    neighbor.heuristicCost = GetDistance(neighbor, targetNode);
                    neighbor.parentNode = currentNode;
                    if (!openList.Contains(neighbor)) openList.Add(neighbor);
                }
            }
        }
        return null;
    }

    List<GridNode> RetracePath(GridNode start, GridNode end)
    {
        List<GridNode> path = new List<GridNode>();
        GridNode current = end;
        while (current != start)
        {
            path.Add(current);
            current = current.parentNode;
        }
        path.Reverse();
        return path;
    }

    int GetDistance(GridNode a, GridNode b)
    {
        int dstX = Mathf.Abs(a.gridX - b.gridX);
        int dstY = Mathf.Abs(a.gridY - b.gridY);
        return dstX + dstY; // Manhattan Distance
    }
}