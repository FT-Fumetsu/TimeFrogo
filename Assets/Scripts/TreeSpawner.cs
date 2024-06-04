using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawners = null;
    [SerializeField] private GameObject[] _treesPrefab = null;
    [SerializeField] private float _offsetPosition = .2f;
    [SerializeField] private float _offsetRotation = .2f;
    [SerializeField] private float _offsetScale = .2f;


    private void Start()
    {
        foreach (Transform spawner in _spawners)
        {
            GameObject randomTreePrefab = _treesPrefab[Random.Range(0, _treesPrefab.Length)];
            GameObject treeInstance = Instantiate(randomTreePrefab, spawner);

            Vector3 offsetPos = new Vector3(
                Random.Range(-_offsetPosition, _offsetPosition),
                0f,
                Random.Range(-_offsetPosition, _offsetPosition));
            treeInstance.transform.localPosition = Vector3.zero + offsetPos;

            Vector3 offsetRot = new Vector3(
                0f,
                Random.Range(-_offsetRotation, _offsetRotation),
                0f);
            treeInstance.transform.localRotation = Quaternion.Euler(offsetRot);

            Vector3 offsetScale = new Vector3(
                Random.Range(-_offsetScale, _offsetScale),
                Random.Range(-_offsetScale, _offsetScale),
                Random.Range(-_offsetScale, _offsetScale));
            treeInstance.transform.localScale = Vector3.one + offsetScale;
        }
    }
}
