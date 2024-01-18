using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AStarPathfinding
{
    public static Vector2Int[] RunAStar(Grid grid, Vector2Int startPos, Vector2Int endPos)
    {
        int cellCost = 10;

        PriorityQueue<Vector2Int> openset = new PriorityQueue<Vector2Int>();
        List<Vector2Int> closedset = new List<Vector2Int>();

        Dictionary<Vector2Int, float> gCosts = new Dictionary<Vector2Int, float>();

        Dictionary<Vector2Int, Vector2Int> parentDict = new Dictionary<Vector2Int, Vector2Int>();

        openset.Enqueue(startPos, 0);
        gCosts.Add(startPos, 0);

        int times = 0;

        while(openset.Count() > 0)
        {
            Vector2Int currentPoint = openset.Dequeue();

            closedset.Add(currentPoint);

            foreach(Vector2Int neighbour in grid.FindCellNeighboursVectorUDLR(currentPoint))
            {
                Debug.Log(neighbour);
                times++;
                if(neighbour == endPos)
                {
                    parentDict[endPos] = currentPoint;

                    return ReconstructPath(parentDict, endPos, startPos);
                }
                else if(closedset.Contains(neighbour))
                {
                    continue;
                }

                float newGCost = gCosts[currentPoint] + cellCost;

                if(openset.Contains(neighbour))
                {
                    gCosts[neighbour] = newGCost;
                    int hCost = (int)CalculateManhattenDistance(neighbour, endPos)*10;

                    float fCost = gCosts[neighbour] + hCost;

                    parentDict[neighbour] = currentPoint;

                    // openset.SetPriority(neighbour, fCost);

                    
                    if (openset.GetPriority(neighbour) > fCost)
                    {
                        openset.SetPriority(neighbour, fCost);
                    }
                }
                else
                {
                    gCosts[neighbour] = newGCost;
                    int hCost = (int)CalculateManhattenDistance(neighbour, endPos)*10;

                    float fCost = gCosts[neighbour] + hCost;

                    parentDict[neighbour] = currentPoint;

                    openset.Enqueue(neighbour, fCost);
                }
            }
        }
        return null;
    }

    private static Vector2Int[] ReconstructPath(Dictionary<Vector2Int, Vector2Int> parentList, Vector2Int goal, Vector2Int start)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Vector2Int current = goal;

        while(current!=start)
        {
            path.Add(current);

            current = parentList[current];
        }

        path.Remove(goal);

        path.Reverse();

        return path.ToArray();
    }

    private static float CalculateManhattenDistance(Vector2Int point1, Vector2Int point2)
    {
        return (Mathf.Abs(point2.x - point1.x) + Mathf.Abs(point2.y - point1.y));
    }
}
