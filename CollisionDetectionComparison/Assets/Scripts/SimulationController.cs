using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SimulationController : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject galtonSphere;
    public GameObject ballSpherePrefab;
    [Space(10)]
    public GameObject galtonAABB;
    public GameObject ballAABBPrefab;
    [Space(10)]
    public GameObject galtonOBB;
    public GameObject ballOBBPrefab;
    [Space(10)]
    public GameObject galtonMesh;
    public GameObject ballMeshPrefab;

    [Header("UI Components")]
    public Text fpsUIText;
    public Text msUIText;
    public Text timeScaleUIText;
    public Text ballCountUIText;

    private List<GameObject> ballsList;
    private int[] ballsCount;

    private float keyCooldown = 0.2f;
    private float keyCDPassedTime = 0.0f;

    private int frameNumber = 0;
    private const int framesHistoryLength = 10;
    
    private List<Tuple<int, float, float>> history;

    private float[] lastXFpsValue = new float[framesHistoryLength];
    private float fps = 0.0f;
    private float[] lastXMsValue = new float[framesHistoryLength];
    private float ms = 0.0f;

    public enum CollisionMode
    {
        NONE,
        SPHERE,
        MESH,
        AABB,
        OBB
    };

    private CollisionMode mode;

    public void ExitApplication()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        ballsList = new List<GameObject>();
        ballsCount = new int[11];
        Time.timeScale = 3.0f;
        timeScaleUIText.text = "pręd: " + Time.timeScale.ToString("0.00");

        history = new List<Tuple<int, float, float>>();
    }

    // Update is called once per frame
    void Update()
    {
        lastXMsValue[frameNumber] = Time.deltaTime * 1000;
        lastXFpsValue[frameNumber] = 1.0f / Time.deltaTime;
        
        if (++frameNumber == framesHistoryLength)
        {
            float fpsAvg = 0.0f;
            for (int i = 0;i< framesHistoryLength;++i)
            {
                fpsAvg += lastXFpsValue[i];
            }
            fps = fpsAvg / framesHistoryLength;

            float msAvg = 0.0f;
            for (int i = 0; i < framesHistoryLength; ++i)
            {
                msAvg += lastXMsValue[i];
            }
            ms = msAvg / framesHistoryLength;

            history.Add(Tuple.Create(ballsList.Count, fps, ms));

            frameNumber = 0;
        }

        
        
        fpsUIText.text = "FPS: " + ((int)fps).ToString();
        msUIText.text = "ms: " + ms.ToString("0.00");

        keyCDPassedTime += Time.deltaTime;
        if (keyCDPassedTime >= keyCooldown)
        {
            keyCDPassedTime = 0.0f;
            if (Input.GetKey(KeyCode.F))
            {
                SaveResults();
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                AddNextBalls();
            }
            else if (Input.GetKey(KeyCode.Alpha1))
            {
                ChangeCollisionMode(CollisionMode.AABB);
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                ChangeCollisionMode(CollisionMode.OBB);
            }
            else if (Input.GetKey(KeyCode.Alpha3))
            {
                ChangeCollisionMode(CollisionMode.MESH);
            }
            else if (Input.GetKey(KeyCode.Alpha4))
            {
                ChangeCollisionMode(CollisionMode.SPHERE);
            }
            else if (Input.GetKey(KeyCode.KeypadPlus))
            {
                IncreaseTimeScale();
            }
            else if (Input.GetKey(KeyCode.KeypadMinus))
            {
                DecreaseTimeScale();
            }
        }
    }

    public void AddNextBalls()
    {
        GameObject ballPrefab = null;
        switch (mode)
        {
            case CollisionMode.SPHERE:
                ballPrefab = ballSpherePrefab;
                break;
            case CollisionMode.AABB:
                ballPrefab = ballAABBPrefab;
                break;
            case CollisionMode.OBB:
                ballPrefab = ballOBBPrefab;
                break;
            case CollisionMode.MESH:
                ballPrefab = ballMeshPrefab;
                break;
        }
        if (ballPrefab != null)
        {

            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    ballsList.Add(Instantiate(ballPrefab, new Vector3(-2.5f + UnityEngine.Random.Range(0.0f, 4.7f), 5.0f - UnityEngine.Random.Range(0.0f, 1.0f), 0.073f), Quaternion.identity));
                }
            }
            ballCountUIText.text = "Liczba piłeczek: " + ballsList.Count.ToString();
        }
        else
        {
            Debug.LogError("No collision mode chosen. Can't instatiate with null prefab.");
        }
    }

    private void ChangeActivityOfGaltonBoard(CollisionMode mode, bool activity)
    {
        switch (mode)
        {
            case CollisionMode.SPHERE:
                if (galtonSphere != null)
                {
                    galtonSphere.SetActive(activity);
                }
                else
                {
                    Debug.LogWarning("Couldn't find \"GaltonBoardSphere\" object.");
                }
                break;
            case CollisionMode.AABB:
                if (galtonAABB != null)
                {
                    galtonAABB.SetActive(activity);
                }
                else
                {
                    Debug.LogWarning("Couldn't find \"GaltonBoardAABB\" object.");
                }
                break;
            case CollisionMode.OBB:
                if (galtonOBB != null)
                {
                    galtonOBB.SetActive(activity);
                }
                else
                {
                    Debug.LogWarning("Couldn't find \"GaltonBoardOBB\" object.");
                }
                break;
            case CollisionMode.MESH:
                if (galtonMesh != null)
                {
                    galtonMesh.SetActive(activity);
                }
                else
                {
                    Debug.LogWarning("Couldn't find \"GaltonBoardMesh\" object.");
                }
                break;
        }
    }

    public void ChangeCollisionMode(int mode)
    {
        switch (mode)
        {
            case 1:
                ChangeCollisionMode(CollisionMode.SPHERE);
                break;
            case 2:
                ChangeCollisionMode(CollisionMode.AABB);
                break;
            case 3:
                ChangeCollisionMode(CollisionMode.OBB);
                break;
            case 4:
                ChangeCollisionMode(CollisionMode.MESH);
                break;
        }
    }

    private void ChangeCollisionMode(CollisionMode mode)
    {
        this.mode = mode;

        ResetSimulation();

        switch (mode)
        {
            case CollisionMode.SPHERE:
                ChangeActivityOfGaltonBoard(CollisionMode.SPHERE, true);
                break;
            case CollisionMode.AABB:
                ChangeActivityOfGaltonBoard(CollisionMode.AABB, true);
                break;
            case CollisionMode.OBB:
                ChangeActivityOfGaltonBoard(CollisionMode.OBB, true);
                break;
            case CollisionMode.MESH:
                ChangeActivityOfGaltonBoard(CollisionMode.MESH, true);
                break;
        }
    }

    private void ResetSimulation()
    {
        ChangeActivityOfGaltonBoard(CollisionMode.SPHERE, false);
        ChangeActivityOfGaltonBoard(CollisionMode.AABB, false);
        ChangeActivityOfGaltonBoard(CollisionMode.OBB, false);
        ChangeActivityOfGaltonBoard(CollisionMode.MESH, false);

        DestroyAllObjectsWithTag("BallSphere");
        DestroyAllObjectsWithTag("BallAABB");
        DestroyAllObjectsWithTag("BallOBB");
        DestroyAllObjectsWithTag("BallMesh");
        
        ballsList = new List<GameObject>();
        ballsCount = new int[11];
        Time.timeScale = 3.0f;

        history = new List<Tuple<int, float, float>>();

        ballCountUIText.text = "Liczba piłeczek: 0";
        timeScaleUIText.text = "pręd: 3.00";
    }

    private void DestroyAllObjectsWithTag(string tag)
    {
        var gameObjects = GameObject.FindGameObjectsWithTag(tag);

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }

    public void IncreaseTimeScale()
    {
        Time.timeScale += 0.1f;
        timeScaleUIText.text = "pręd: " + Time.timeScale.ToString("0.00");
    }

    public void DecreaseTimeScale()
    {
        if (Time.timeScale > 0.1f)
        {
            Time.timeScale -= 0.1f;
            timeScaleUIText.text = "pręd: " + Time.timeScale.ToString("0.00");
        }
    }

    public void SaveResults()
    {
        ballsCount = new int[11];
        foreach (var ball in ballsList)
        {
            for (int i = 1; i < 12; ++i)
            {
                if (ball.transform.position.y < 0 &&
                    ball.transform.position.x > GameObject.Find("Border" + (i).ToString()).transform.position.x &&
                    ball.transform.position.x < GameObject.Find("Border" + (i + 1).ToString()).transform.position.x)
                {
                    ++ballsCount[i - 1];
                    break;
                }
            }
        }

        for (int i = 0; i < ballsCount.Length; ++i)
        {
            Debug.Log("W nr " + i + " jest " + ballsCount[i] + " piłeczek.");
        }

        int n = ballsList.Count;
        string results = "";
        for (int i = 0; i < ballsCount.Length; ++i)
        {
            results += i.ToString() + " - " + ballsCount[i].ToString() + "\n";
        }

        string historyResult = "";
        foreach(var hist in history)
        {
            historyResult += hist.Item1.ToString() + '\t' + hist.Item2.ToString() + '\t' + hist.Item3.ToString() + '\n';
        }

        File.WriteAllText("history" + mode.ToString() + ".txt", historyResult);

        File.WriteAllText(mode.ToString() + ".txt", results);
    }
}
