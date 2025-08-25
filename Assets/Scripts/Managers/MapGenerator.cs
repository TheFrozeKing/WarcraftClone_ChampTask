using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance { get; private set; }

    [SerializeField] private float _treeNoiseScale = 10;
    [SerializeField] private GameObject _treePrefab;
    [SerializeField] private float _rockNoiseScale = 10;
    [SerializeField] private GameObject _rockPrefab;

    [SerializeField] private int _baseFieldRadius = 15;
    [SerializeField] private TownHall _basePref;
    private List<TownHall> _availableBases = new();
    private List<Vector3> _basePositions = new();
    private int _baseCount;
    private NavMeshSurface _groundMeshSurface;

    private int _mapSize;

    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private Transform _ground;
    [SerializeField] private Transform _natureHolder;

    private Camera _minimapCamera;


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        _groundMeshSurface = GetComponent<NavMeshSurface>();
        _minimapCamera = transform.GetChild(2).GetComponent<Camera>();
    }
    private void Start()
    {
        _mapSize = (int)Mathf.Sqrt(_gameSettings.MapSize);
        _ground.localScale = new Vector3(_mapSize,_mapSize,1);
        _ground.position = new Vector3(_mapSize/2,0,_mapSize/2);
        _baseCount = _gameSettings.Players.Count;

        _minimapCamera.transform.position = _ground.position + (Vector3.up * 30);
        _minimapCamera.orthographicSize = _mapSize/2;

        float radius = Random.Range(0, 365);
        for(int i = 0; i < _baseCount; i++)
        {
            Vector3 basePos = _ground.position + Quaternion.Euler(0,radius,0) * Vector3.forward * _mapSize/3;
            _basePositions.Add(basePos);
            TownHall newBase = Instantiate(_basePref, basePos, Quaternion.identity);
            _availableBases.Add(newBase);

            radius += 360 / _baseCount;
        }


        float[,] rocksNoise = GenerateNoise(_rockNoiseScale);
        float[,] treeNoise = GenerateNoise(_treeNoiseScale);

        GenerateRocksByNoise(rocksNoise);
        GenerateForestByNoise(treeNoise, rocksNoise);

        _groundMeshSurface.BuildNavMesh();
        GetComponent<MatchStarter>().StartMatch(_availableBases);
    }


    private float[,] GenerateNoise(float noiseScale)
    {
        float[,] noise = new float[_mapSize,_mapSize];

        float offset = Random.Range(0, 5000);

        for(int x = 0; x < _mapSize; x++)
        {
            for (int y = 0; y < _mapSize; y++)
            {
                if (CheckProximityToBases(new Vector3(x, 0, y)))
                {
                    float nx = ((float)x / _mapSize - 0.5f) + offset;
                    float ny = ((float)y / _mapSize - 0.5f) + offset;
                    nx *= noiseScale;
                    ny *= noiseScale;
                    noise[x, y] = Mathf.PerlinNoise(nx, ny);
                }
                else
                {
                    noise[x, y] = 0;
                }
            }
        }

        return noise;
    }

    private void GenerateRocksByNoise(float[,] rocksNoise)
    {
        for (int x = 0; x < _mapSize; x++)
        {
            for (int y = 0; y < _mapSize; y++)
            {
                if (rocksNoise[x, y] > 0.7f)
                {
                    var newRock = Instantiate(
                        _rockPrefab,
                        new Vector3(x + Random.Range(-0.5f, 0.5f), 0, y + Random.Range(-0.5f, 0.5f)),
                        Quaternion.Euler(0, Random.Range(0, 360), 0),
                        _natureHolder);
                    float rockScale = Random.Range(0.5f, 1f);
                    newRock.transform.GetChild(0).localScale = new Vector3(rockScale, rockScale, rockScale);
                }
            }
        }
    }

    private void GenerateForestByNoise(float[,] treeNoise, float[,] rocksNoise)
    {
        for (int x = 0; x < _mapSize; x++)
        {
            for (int y = 0; y < _mapSize; y++)
            {
                if (treeNoise[x, y] > 0.5f && rocksNoise[x, y] < 0.5f)
                {
                    var newTree = Instantiate(_treePrefab, new Vector3(x + Random.Range(-0.5f, 0.5f), 0, y + Random.Range(-0.5f, 0.5f)), Quaternion.identity, _natureHolder);
                    float treeScale = Random.Range(0.5f, 2f);
                    newTree.transform.GetChild(0).localScale = new Vector3(treeScale, treeScale, treeScale);
                }
            }
        }
    }

    private bool CheckProximityToBases(Vector3 pos)
    {
        foreach(var basePos in _basePositions)
        {
            if(Vector3.Distance(basePos, pos) <= _baseFieldRadius)
            {
                return false;
            }
        }
        return true;
    }
}
