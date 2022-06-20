using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class IASpawner : MonoBehaviour
{
    
    private List<GameObject> IaActive;
    [SerializeField] private List<GameObject> IaInactive;
    
    public int delayMin;
    public int DelayMax;
    
    private int phase = 0;
    private Random _random;
    [SerializeField] private float[] delayMultByPhase;

    public static IASpawner instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        IaActive = new List<GameObject>();
        _random = new Random();
        foreach (GameObject ia in IaInactive)
        {
            ia.SetActive(false);
        }
    }

    public void SpawnIA(int nb, int phaseID)
    {
        if (phaseID != phase)
        {
            phase = phaseID;
            StartCoroutine(Spawn(nb));
        }

    }
   
    private IEnumerator Spawn(int nb)
    {
        for (int i = 0; i < nb; i++)
        {
            if (IaInactive.Count > 0)
            {
                GameObject ia = IaInactive[0];
                ia.SetActive(true);
                IaActive.Add(ia);
                IaInactive.Remove(ia);
                yield return new WaitForSeconds(_random.Next(delayMin, DelayMax)*delayMultByPhase[phase]);
            }
        }
    }
}
