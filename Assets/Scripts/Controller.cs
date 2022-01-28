using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Controller : MonoBehaviour
{
    public int maxEnemyCount;
    public int EnemiesDestroyed;
    public int numEnemiesRemaining;
    public int assualtWait;
    public int spawnWait;
    public int waveWait;
    public int waveCount;
    public int assualt;
    public bool nextAssualt;
    public Enemies[] ebase;
    public Player playerScript;
    public Save_Script save;

    public static bool isComplete;

    Transform target;
    List<Enemies> enemyList = new List<Enemies>();

    private void Awake()
    {
        place_player();
    }

    void Start()
    {
        nextAssualt = true;
    }


    void Update()
    {
        if(!Player.isDead)
        {
            enemies_remaining();
            if ((nextAssualt) && (assualt < 3) && (numEnemiesRemaining <= 0))
            {
                StartCoroutine(Spawner());
                StartCoroutine(Lootcreate());
            }
            if ((assualt >= 3) && (numEnemiesRemaining <= 0))
                isComplete = true;
        }
        else
        {
            StopCoroutine(Spawner());
            StopCoroutine(Lootcreate());
        }
    }

    void place_player()
    {
        GameObject handler;
        NavMeshHit hit;
        Vector3 test = new Vector3(0.0f, 4.0f, 0.0f);
            do
            {
            } while (!NavMesh.SamplePosition(test, out hit, 4.1f, 1));
            test = hit.position;
            test.y += 20.0f;
        handler = Instantiate(Resources.Load("Player"), test, Quaternion.identity) as GameObject;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    IEnumerator Lootcreate()
    {
        yield return new WaitForSecondsRealtime(Random.Range(60.0f, 180.0f));
        GameObject handler;
        if (!GameObject.Find("Nanite(Clone)"))
        {
            NavMeshHit hit;
            NavMeshPath path = new NavMeshPath();
            Vector3 test = new Vector3();
            Vector3 temp = new Vector3();
            test = target.position;
            bool ans;
            do
            {
                do
                {
                    temp.Set(Random.Range(-124.5f, 124.5f), 4.0f, Random.Range(-124.5f, 124.5f));
                } while (!NavMesh.SamplePosition(temp, out hit, 4.1f, 1));
                test.y = hit.position.y;
                ans = NavMesh.CalculatePath(hit.position, test, 1, path);
            } while ((ans == false) || (path.status != NavMeshPathStatus.PathComplete));
            handler = Instantiate(Resources.Load("Nanite"), hit.position, Quaternion.identity) as GameObject;
            Destroy(handler, 300.0f);
        }
    }

    IEnumerator Spawner()
    {
        nextAssualt = false;
        assualt++;
        waveCount = 0;
        int spawned = 0, multiplier = 0;
        yield return new WaitForSecondsRealtime(assualtWait);
        while (waveCount < 4)
        {
            waveCount++;
            multiplier++;
            Vector3 place = new Vector3();
            while (spawned < maxEnemyCount * waveCount * multiplier * assualt)
            {
                spawned++;
                spawnWait = Random.Range(1, 10);
                findPosition(ref place);

                switch (waveCount)
                {
                    case 1:
                        enemyList.Add(Instantiate(ebase[0], place, transform.rotation));
                        break;
                    case 2:
                        enemyList.Add(Instantiate(ebase[Random.Range(0, 2)], place, transform.rotation));
                        break;
                    case 3:
                        enemyList.Add(Instantiate(ebase[2], place, transform.rotation));
                        break;
                    case 4:
                        enemyList.Add(Instantiate(ebase[Random.Range(0, 3)], place, transform.rotation));
                        break;
                }

                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
        nextAssualt = true;
    }

    private void enemies_remaining()
    {
        numEnemiesRemaining = 0;
        foreach (Enemies e in enemyList)
            if (e != null)
                numEnemiesRemaining++;
    }

    private void findPosition(ref Vector3 temp)
    {
        NavMeshHit hit;
        bool ans;
        int i = 1000;
        float[] randoms = new float[2];
        Vector3[] vrand = new Vector3[4];
        NavMeshPath path = new NavMeshPath();
        Vector3 test = new Vector3();
        test = target.position;
        do
        {
            do
            {
                randoms[0] = 124.5f;
                randoms[1] = -124.5f;
                vrand[0] = new Vector3(Random.Range(-124.5f, 124.5f), 4.0f, randoms[Random.Range(0, 2)]);
                vrand[1] = new Vector3(randoms[Random.Range(0, 2)], 4.0f, Random.Range(-124.5f, 124.5f));
                temp = vrand[Random.Range(0, 2)];
            } while (!NavMesh.SamplePosition(temp, out hit, 4.1f, 1));
            test.y = hit.position.y;
            ans = NavMesh.CalculatePath(hit.position, test, 1, path);
        } while (((ans == false) || (path.status != NavMeshPathStatus.PathComplete)) && (i > 0));
        temp = hit.position;
    }
}
