using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class IASpawner : MonoBehaviour
{
    
    private List<GameObject> IaActive;
    [SerializeField] private List<GameObject> IaInactive;
    
    private List<GameObject> passerbyActive;
    [SerializeField] private List<GameObject> passerbyInactive;
    
    public int delayMin;
    public int DelayMax;
    
    private int phase = 0;
    private Random _random;
    [SerializeField] private float[] delayMultByPhase;

    public static IASpawner instance;
    
    public int[] ClientToSpawnByPhase;
    public int[] PasserbyToSpawnByPhase;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        IaActive = new List<GameObject>();
        passerbyActive = new List<GameObject>();
        _random = new Random();
        foreach (GameObject ia in IaInactive)
        {
            ia.SetActive(false);
        }
        
        foreach (GameObject ia in passerbyInactive)
        {
            ia.SetActive(false);
        }
    }

    public void SpawnIA(int phaseID)
    {
        if (phaseID != phase)
        {
            phase = phaseID;
            StartCoroutine(Spawn(ClientToSpawnByPhase[phase], IaInactive, IaActive));
            StartCoroutine(Spawn(PasserbyToSpawnByPhase[phase], passerbyInactive, passerbyActive));
        }

    }
   
    private IEnumerator Spawn(int nb, List<GameObject> iaInact, List<GameObject> iaAct)
    {
        for (int i = 0; i < nb; i++)
        {
            if (iaInact.Count > 0)
            {
                GameObject ia = iaInact[0];
                ia.SetActive(true);
                iaAct.Add(ia);
                iaInact.Remove(ia);
                yield return new WaitForSeconds(_random.Next(delayMin, DelayMax)*delayMultByPhase[phase]);
            }
        }
    }
}
