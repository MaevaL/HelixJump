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
    public UIController uiController;

    // Use this for initialization
    void Awake () {
        ProceduralGeneration();
        rotation = transform.localEulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
        //Work with touch
        //Follow mouvement of the mouse or touch
        if (Input.GetMouseButton(0) && !GameController.instance.isGameOver)
        {
            //Avoid controls of the helix when there is a UI screen
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Vector2 curInputPos = Input.mousePosition; //position of the current input

            if(prevInputPos == Vector3.zero)
            {
                prevInputPos = curInputPos;
            }

            float delta = prevInputPos.x - curInputPos.x; // distance between the two positions
            prevInputPos = curInputPos;

            transform.Rotate(Vector3.up * delta * speed );
        }

        if (Input.GetMouseButtonUp(0))
        {
            prevInputPos = Vector3.zero;
        }
	}

    /// <summary>
    /// Destroy the current level
    /// </summary>
    public void DestroyLevel()
    {
        foreach (GameObject step in currentStepLevel)
        {
            Destroy(step);
        }

        currentStepLevel.Clear();
 
    }
    /// <summary>
    /// Generate a procedural level
    /// </summary>
    public void ProceduralGeneration()
    {
        GameObject helixStepGo;
        GameObject chunk = EasyChunk[Random.Range(0, EasyChunk.Count)];

        float randomRotation = 0;

        gameObject.transform.rotation = Quaternion.identity;

        //All the platforms will be child of Steps
        GameObject Steps = new GameObject("Steps");
        currentStepLevel.Add(Steps);
        Steps.transform.position = Vector3.zero;


        for (int i = 0; i < StepNumber; i++)
        {
            //Initial values for the first chunk
            helixStepGo = Instantiate(chunk, transform.position, Quaternion.identity);
            helixStepGo.transform.position = InitialSpawn + new Vector3(0, -(i * SpaceBetweenStep), 0);

            helixStepGo.transform.Rotate(new Vector3(0, randomRotation, 0));

            //Random values for other chunks
            chunk = RandomChunk();
            randomRotation = Random.Range(0, 180);

            helixStepGo.transform.parent = Steps.transform;

            currentStepLevel.Add(helixStepGo);
        }

        //Initialisation of the Goal chunk
        helixStepGo = Instantiate(GoalChunk, transform.position, Quaternion.identity);
        helixStepGo.transform.position = InitialSpawn + new Vector3(0, -(StepNumber * SpaceBetweenStep), 0);
        helixStepGo.transform.parent = Steps.transform;
        currentStepLevel.Add(helixStepGo);


        Steps.transform.parent = gameObject.transform;      
    }

    /// <summary>
    /// Return a randomized chunk
    /// </summary>
    /// <returns>a randomised chunk</returns>
    private GameObject RandomChunk()
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

    /// <summary>
    /// Reactivate all the platforms
    /// </summary>
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
}
