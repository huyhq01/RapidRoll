using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] GameObject platformPrefab;
    [SerializeField] GameObject dangerPrefab;
    [SerializeField] Camera cam;
    [SerializeField] int defaultSize;
    [SerializeField] int maxSize;
    [SerializeField] float spawnRate;

    private ObjectPool<GameObject> Ppool;
    private ObjectPool<GameObject> Dpool;
    private Vector3 spawnPosition;
    private int countToSpawn;
    private float leftBound, bottomBound, widthPrefab, leftBorder, bottomBorder;

    public float GetTopBound() { return -bottomBound; }
    public float GetBottomBound() { return bottomBound; }
    public float GetLeftBorder() { return leftBorder; }


    // Start is called before the first frame update
    void Start()
    {
        widthPrefab = platformPrefab.GetComponent<SpriteRenderer>().bounds.size.x;

        Vector3 edgeCamCoordinate = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        leftBorder = edgeCamCoordinate.x;
        bottomBorder = edgeCamCoordinate.y;
        leftBound = leftBorder + (widthPrefab / 2);
        bottomBound = bottomBorder - 2;


        #region createPool
        Ppool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(platformPrefab);
        }, prefab =>
        {
            prefab.gameObject.SetActive(true);
            prefab.transform.position = spawnPosition;
        }, prefab =>
        {
            prefab.gameObject.SetActive(false);
        }, prefab =>
        {
            Destroy(prefab.gameObject);
        }, false, defaultSize, maxSize);

        Dpool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(dangerPrefab);
        }, prefab =>
        {
            prefab.gameObject.SetActive(true);
            prefab.transform.position = spawnPosition;
        }, prefab =>
        {
            prefab.gameObject.SetActive(false);
        }, prefab =>
        {
            Destroy(prefab.gameObject);
        }, false, defaultSize, maxSize);

        #endregion

        countToSpawn = Random.Range(1, 4);
        InvokeRepeating(nameof(Spawn), 0, spawnRate);
    }
    void Spawn()
    {
        spawnPosition = new Vector3(Random.Range(leftBound, -leftBound), bottomBound);
        if (countToSpawn == 1)
        {
            GameObject d = Dpool.Get();
            countToSpawn = Random.Range(2, 5);
        }
        else
        {
            GameObject p = Ppool.Get();
            countToSpawn--;
        }
    }

    public void Deactive(GameObject platform)
    {
        Ppool.Release(platform);
    }
}
