using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class IASpawner : MonoBehaviour
{

    [Header("IaToActivate")]
    private List<GameObject> IaActive;
    [SerializeField] private List<GameObject> IaInactive;
    private int phase = 0;
    
    [Header("Spawning delay")]
    public int delayMin;
    public int DelayMax;
    [SerializeField] private float[] delayMultByPhase;
    
    [Header("Become Client")]
    public float probability;
    private Random _random;
    public List<Material> _materials;
    

    public static IASpawner instance;
    
    public int[] IaToSpawnByPhase;

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
            try
            {
                //ia.GetComponent<Client>().enabled = false;
            }
            catch
            {

            }
            ia.SetActive(false);
        }
    }

    public bool SetIaAsClient(IAStateMachine ia)
    {
        if(_random.NextDouble() < phase * Time.deltaTime * probability && _materials.Count > 0)
        {
            Material mat = _materials[_random.Next(_materials.Count)];
            _materials.Remove(mat);
            ia.clientMat = mat;
            return true;
            
        }
        return false;
    }

    public void SpawnIA(int phaseID)
    {
        if (phaseID != phase)
        {
            phase = phaseID;
            StartCoroutine(Spawn(IaToSpawnByPhase[phase], IaInactive, IaActive));
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
