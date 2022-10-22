using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int gridSize = 5;
    public GameObject tickBG;
    private int matchCount = 0;
    private UIController uIController;
    List<Transform> map = new List<Transform>();
    List<Transform> ticks = new List<Transform>();
    bool matchIncreased = false;


    private void Awake()
    {
        Application.targetFrameRate = 60;
        uIController = FindObjectOfType<UIController>();
    }
    private void Start()
    {
        GenerateGame(gridSize);
    }

    public void AddTick(Transform sender)
    {
       GameObject tick = Instantiate(tickBG,
                    new Vector3(sender.transform.position.x, sender.transform.position.y, sender.transform.position.z - 0.5f),
                    Quaternion.identity);
        ticks.Add(tick.transform);
        tick.transform.localScale = new Vector3(5, 5, 5);
        ControlTickNeighbors();
    }

    public void GenerateGame(int toChange)
    {
        gridSize = toChange;
        ResetScene();

        float calculateSize = Screen.height > Screen.width ? (float)Screen.height / (float)Screen.width / 2f
          : (float)Screen.width / (float)Screen.height / 3f;
        
        map = GridGeneration.GenerateGrid(gridSize,this);

        Camera.main.transform.position = new Vector3((gridSize-1) / 2f, (gridSize-1) / 2f, -1);
        Camera.main.orthographicSize = gridSize * calculateSize;
    }

    private void ResetScene()
    {
        foreach (Transform child in map)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in ticks)
        {
            if(child != null)
            Destroy(child.gameObject);
        }
       ticks = new List<Transform>();
       matchCount = 0;
       uIController.ChangeMatchCount(matchCount);
    }

    private void ControlTickNeighbors()
    {
        matchIncreased = false;
        for (int x= 0; x<ticks.Count; x++)
        {
            List<Transform> ticksToCheck = new List<Transform>();
            ticksToCheck.Add(ticks[x]);
            CheckNextNeighbor(ticksToCheck, 0);  
        }
       
    }
    private void CheckNextNeighbor(List<Transform> tickToCheck,int totalNeighborCount)
    {
        int neighborCount = 0;
        for(int x= 0; x<ticks.Count; x++)
        {
            bool returnVal = false;
            foreach(Transform child in tickToCheck)
            {
                if (child == ticks[x])
                    returnVal = true;
            }
            if (returnVal)
                continue;
            if (ticks[x] == null || tickToCheck[tickToCheck.Count - 1] == null)
                continue;

            float distance = Vector3.Distance(ticks[x].position, tickToCheck[tickToCheck.Count-1].position);
            if (distance == 1)
            {
                tickToCheck.Add(ticks[x]);
                neighborCount++;
                totalNeighborCount++;
            }

            if (neighborCount == 0)
            {
                continue;
            }
            else if (neighborCount == 1)
            {
                CheckNextNeighbor(tickToCheck, totalNeighborCount);
                continue;
            }
        }
        if (totalNeighborCount == 2)
        {
            for (int i = 0; i < tickToCheck.Count; i++)
            {
                if (tickToCheck[i] != null)
                {
                    if(!matchIncreased)
                    {
                        matchIncreased = true;
                        matchCount++;
                        uIController.ChangeMatchCount(matchCount);
                    }
                    Destroy(tickToCheck[i].gameObject);
                }

            }
            return;
        }
    }
}
