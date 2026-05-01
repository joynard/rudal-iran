using UnityEngine;

public class GridNode
{
    public int gridX;
    public int gridY;
    public bool isWall;
    public Vector2 worldPosition;
    public GridNode parentNode;

    public int moveCost; // gCost
    public int heuristicCost; // hCost

    public int TotalCost { get { return moveCost + heuristicCost; } }

    public GridNode(bool isWallInp, Vector2 worldPosInp, int xInp, int yInp)
    {
        isWall = isWallInp;
        worldPosition = worldPosInp;
        gridX = xInp;
        gridY = yInp;
    }
}