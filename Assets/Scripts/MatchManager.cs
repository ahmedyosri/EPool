using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    private MatchManager instance;
    public MatchManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = this;
            }
            return instance;
        }
    }

    public List<Transform> ballPositions;

    public void Awake()
    {
        for (int i = 0; i < ballPositions.Count; i++)
        {
            int x = Random.Range(0, ballPositions.Count);
            int y = Random.Range(0, ballPositions.Count);
            Swap(ballPositions[x], ballPositions[y]);
        }
    }

    public void Swap<T>(T a, T b)
    {
        T rhs = a;
        a = b;
        b = rhs;
    }
}
