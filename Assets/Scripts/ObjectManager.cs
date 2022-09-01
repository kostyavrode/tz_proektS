using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obj;
    public void CreateObjects(List<SplineComputer> splines)
    {
        for(int i=0;i<Random.Range(3,10f);i++)
        {
            Instantiate(obj[Random.Range(0, obj.Length)], splines[Random.Range(0, splines.Count)].GetPoint(Random.Range(0, 3)).position
                + new Vector3(Random.Range(2, 10), Random.Range(2, 10), Random.Range(2, 10)), Quaternion.identity, this.transform);
        }
    }
}
