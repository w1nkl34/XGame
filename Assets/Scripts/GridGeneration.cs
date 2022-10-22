using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridGeneration 
{
    public static List<Transform> GenerateGrid(int gridSize,GameManager gameManager)
    {
        List<Transform> map = new List<Transform>(); 
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector2 position = new Vector2(x,y);
                GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                newObj.transform.position = position;
                newObj.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
                GridController gridController = newObj.AddComponent<GridController>();
                gridController.gameManager = gameManager;
                map.Add(newObj.transform);
            }
        }
        return map;
    }
}
