using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpawnerController : Singleton<SpawnerController>
{
    [SerializeField] List<GameObject> _monsters;
    [SerializeField] float _monsterSpawnTime;
    [SerializeField] float _monsterSpawnTimer;

    [SerializeField] GameObject _mouse, _fire, _heart;

    int _spawnCount;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject monster in _monsters)
        {
            ObjectPooling.instance.CreatePool(monster, 5);
        }
        ObjectPooling.instance.CreatePool(_mouse, 5);
        ObjectPooling.instance.CreatePool(_fire, 5);

        _monsterSpawnTime = 1.5f;
        _monsterSpawnTimer = _monsterSpawnTime;
    }

    public void Init()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        _spawnCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_spawnCount);
        if (GameManager.instance._gameState != GameManager.GAME_STATE.PLAY)
            return;

        _monsterSpawnTimer -= Time.deltaTime;
        if( _monsterSpawnTimer < 0)
        {
            if(_spawnCount % 30 == 0 && _spawnCount != 0)
            {
                SpawnHeart();
            }
            else
            {
                int spawnChoice = Random.Range(0, 3);
                if (spawnChoice == 0)
                    SpawnMonster();
                else if (spawnChoice == 1)
                    SpawnMouse();
                else
                    SpawnFire();
            }

            if(_spawnCount < 15)
            {
                _monsterSpawnTimer = _monsterSpawnTime - Random.Range(0, 0.5f);
            }
            else
            {
                _monsterSpawnTimer = _monsterSpawnTime - Random.Range(0.5f, 1.2f);
            }
            _spawnCount++;
        }
    }

    void SpawnMonster()
    {
        GameObject monster =  ObjectPooling.instance.GetObject(_monsters[Random.Range(0,_monsters.Count)]);
        monster.SetActive(true);
        int line = Random.Range(0, 2);
        if(line == 0)
        {
            monster.transform.position = new Vector3(15, 1, 0);
        }
        else
        {
            monster.transform.position = new Vector3(15, -1.5f, 0);
        }
    }

    void SpawnMouse()
    {
        GameObject mouse = ObjectPooling.instance.GetObject(_mouse);
        mouse.SetActive(true);
        int line = Random.Range(0, 2);
        if (line == 0)
        {
            mouse.transform.position = new Vector3(15, 0.85f, 0);
        }
        else
        {
            mouse.transform.position = new Vector3(15, -1.75f, 0);
        }
        mouse.GetComponent<MouseController>().Init();
    }

    void SpawnFire()
    {
        GameObject fire = ObjectPooling.instance.GetObject(_fire);
        fire.SetActive(true);
        int line = Random.Range(0, 2);
        if (line == 0)
        {
            fire.transform.position = new Vector3(15, 0.58f, 0);
        }
        else
        {
            fire.transform.position = new Vector3(15, -2f, 0);
        }
    }
    void SpawnHeart()
    {
        GameObject heart = ObjectPooling.instance.GetObject(_heart);
        heart.SetActive(true);
        int line = Random.Range(0, 2);
        if (line == 0)
        {
            heart.transform.position = new Vector3(15, 0.8f, 0);
        }
        else
        {
            heart.transform.position = new Vector3(15, -1.7f, 0);
        }
    }
}
