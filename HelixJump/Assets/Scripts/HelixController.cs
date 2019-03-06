using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HelixController : MonoBehaviour {

    private Vector3 prevInputPos; 
    private Vector3 rotation; //rotation angle
    public float speed = 0.2f;
    private List<GameObject> currentStepLevel = new List<GameObject>();
    private Quaternion initialrotation;
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
	
	// Update is called once per frame
	void Update () {
        //Work with touch
        //Follow mouvement of the mouse or touch
        if (Input.GetMouseButton(0) && !GameController.instance.IsGameOver)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
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

        currentStepLevel.Clear();
 
    }
    public void ProceduralGeneration()
    {
        GameObject helixStepGo;
        GameObject ChunkToInstantiate = EasyChunk[Random.Range(0, EasyChunk.Count)];

        float randomRotation = 0;

        gameObject.transform.rotation = Quaternion.identity;

        GameObject Steps = new GameObject("Steps");
        currentStepLevel.Add(Steps);
        Steps.transform.position = Vector3.zero;

        for (int i = 0; i < StepNumber; i++)
        {
            //Initial values for first chunk
            helixStepGo = Instantiate(ChunkToInstantiate, transform.position, Quaternion.identity);
            helixStepGo.transform.position = InitialSpawn + new Vector3(0, -(i * SpaceBetweenStep), 0);

            helixStepGo.transform.Rotate(new Vector3(0, randomRotation, 0));

            //Random values for other chunks
            ChunkToInstantiate = RandomChunk();
            randomRotation = Random.Range(0, 180);

            helixStepGo.transform.parent = Steps.transform;

            currentStepLevel.Add(helixStepGo);
        }

        //Instantiation of Goal chunk
        helixStepGo = Instantiate(GoalChunk, transform.position, Quaternion.identity);
        helixStepGo.transform.position = InitialSpawn + new Vector3(0, -(StepNumber * SpaceBetweenStep), 0);
        helixStepGo.transform.parent = Steps.transform;
        currentStepLevel.Add(helixStepGo);


        Steps.transform.parent = gameObject.transform;      
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
    public void ProceduralTestGeneration()
    {
        GameObject helixStepGo;
        GameObject ChunkToInstantiate = EasyChunk[Random.Range(0, EasyChunk.Count)];
        float Rotation = 0;
        GameObject Steps;
        Steps = Instantiate(new GameObject("Steps"));
        Steps.transform.position = Vector3.zero;

        float randomRotation = Rotation;

        for (int i = 0; i < StepNumber; i++)
        {
            helixStepGo = Instantiate(ChunkToInstantiate, transform.position, Quaternion.identity);


            helixStepGo.transform.position = InitialSpawn + new Vector3(0, -(i * SpaceBetweenStep), 0);



            helixStepGo.transform.parent = Steps.transform;
        }

        helixStepGo = Instantiate(GoalChunk, transform.position, Quaternion.identity);
        helixStepGo.transform.position = InitialSpawn + new Vector3(0, -(StepNumber * SpaceBetweenStep), 0);
        helixStepGo.transform.parent = Steps.transform;
        Steps.transform.parent = gameObject.transform;
    }

    public void OnRestartBehavior()
    {
        gameObject.transform.rotation = Quaternion.identity;
        foreach (GameObject step in currentStepLevel)
        { 
            step.SetActive(true);
            if(step.name != "Steps")
                step.transform.localScale = new Vector3(0.8f, 2f, 0.8f);
        }
    }
}
