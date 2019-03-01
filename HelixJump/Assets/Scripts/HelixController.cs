using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour {

    private Vector3 prevInputPos; 
    private Vector3 rotation; //rotation angle
    public float speed = 0.2f;
    private List<GameObject> currentStepLevel = new List<GameObject>();
    [Header("Procedural")]
   
    public GameObject GoalChunk;
    public List<GameObject> EasyChunk;
    public List<GameObject> MediumChunk;
    public List<GameObject> HardChunk;
    public Vector3 InitialSpawn;
    public int StepNumber;
    public float SpaceBetweenStep;

    // Use this for initialization
    void Awake () {
        ProceduralGeneration();
        rotation = transform.localEulerAngles;
	}

    void Start()
    {
        GameController.instance.RestartEvent += OnRestartBehavior;
    }
	
	// Update is called once per frame
	void Update () {
        //Work with touch
        //Follow mouvement of the mouse or touch
        if (Input.GetMouseButton(0))
        {
            Vector2 curInputPos = Input.mousePosition; //point touch the screen

            if(prevInputPos == Vector3.zero)
            {
                prevInputPos = curInputPos;
            }

            float delta = prevInputPos.x - curInputPos.x; // distance between two positions
            prevInputPos = curInputPos;

            transform.Rotate(Vector3.up * delta * speed );
        }

        if (Input.GetMouseButtonUp(0))
        {
            prevInputPos = Vector3.zero;
        }
	}

    public void DestroyLevel()
    {
        foreach (GameObject step in currentStepLevel)
        {
            Destroy(step);
        }
    }
    public void ProceduralGeneration()
    {
        Debug.Log("spawn");
        GameObject helixStepGo;
        GameObject ChunkToInstantiate = EasyChunk[Random.Range(0, EasyChunk.Count)];

        float randomRotation = 0;

        GameObject Steps = Instantiate(new GameObject("Steps"));
        Steps.transform.position = Vector3.zero;

        for (int i = 0; i < StepNumber; i++)
        {
            helixStepGo = Instantiate(ChunkToInstantiate, transform.position, Quaternion.identity);
            helixStepGo.transform.Rotate(new Vector3(0, randomRotation, 0));

            ChunkToInstantiate = RandomChunk();

            randomRotation = Random.Range(0, 180);

            helixStepGo.transform.position = InitialSpawn + new Vector3(0, -(i * SpaceBetweenStep), 0);


            helixStepGo.transform.parent = Steps.transform;
            currentStepLevel.Add(helixStepGo);
        }

        helixStepGo = Instantiate(GoalChunk, transform.position, Quaternion.identity);
        helixStepGo.transform.position = InitialSpawn + new Vector3(0, -(StepNumber * SpaceBetweenStep), 0);
        helixStepGo.transform.parent = Steps.transform;

        Steps.transform.parent = gameObject.transform;
        currentStepLevel.Add(Steps);
        currentStepLevel.Add(helixStepGo);
    }

    public GameObject RandomChunk()
    {
        float randomChunk = Random.Range(0, 100);
        if (randomChunk < 33)
        {
            return EasyChunk[Random.Range(0, EasyChunk.Count)];
        }
        else if (randomChunk > 33 && randomChunk < 80)
        {
            return MediumChunk[Random.Range(0, MediumChunk.Count)];
        }
        else
        {
            return HardChunk[Random.Range(0, HardChunk.Count)];
        }
    }
    //public void ProceduralTestGeneration()
    //{
    //    GameObject helixStepGo;
    //    GameObject ChunkToInstantiate = EasyChunk[Random.Range(0, EasyChunk.Count)];
    //    float Rotation = 0;
    //    GameObject Steps;
    //    Steps = Instantiate(new GameObject("Steps"));
    //    Steps.transform.position = Vector3.zero;

    //    float randomRotation = Rotation;

    //    for (int i = 0; i < StepNumber; i++)
    //    {
    //        helixStepGo = Instantiate(ChunkToInstantiate, transform.position, Quaternion.identity);


    //        helixStepGo.transform.position = InitialSpawn + new Vector3(0, -(i * SpaceBetweenStep), 0);



    //        helixStepGo.transform.parent = Steps.transform;
    //    }

    //    helixStepGo = Instantiate(GoalChunk, transform.position, Quaternion.identity);
    //    helixStepGo.transform.position = InitialSpawn + new Vector3(0, -(StepNumber * SpaceBetweenStep), 0);
    //    helixStepGo.transform.parent = Steps.transform;
    //    Steps.transform.parent = gameObject.transform;
    //}

    public void OnRestartBehavior()
    {
        foreach(GameObject step in currentStepLevel)
        {
            step.SetActive(true);
        }
    }
}
