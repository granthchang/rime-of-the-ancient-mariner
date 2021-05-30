using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LineRenderer))]
public class ReflectionScript : MonoBehaviour
{
    public int reflections;
    public float maxLength;
/*    public GameObject goal;*/
    public UnityEvent onGoalAchieved;

    public enum Puzzle { puzzle1, puzzle2, puzzle3, none }
    public static Puzzle currentPuzzle;

    public bool isGoal1Achieved;
    public bool isLadderUnlocked;
    public bool isGoal3Achieved;

    private LineRenderer linerenderer;
    private Ray ray;
    private RaycastHit hit;
/*	private Vector3 direction;
	private GameObject enemy;
*/
	private void Awake()
    {
        linerenderer = GetComponent<LineRenderer>();        
    }
    // Start is called before the first frame update
    void Start()
    {
		/*enemy = GameObject.FindGameObjectWithTag("Enemy");*/

		switch (PlayerPrefs.GetInt("Current Checkpoint")) {
			case 0:
				currentPuzzle = Puzzle.puzzle1;
				break;
			case 1:
				currentPuzzle = Puzzle.puzzle2;
				break;
			case 2:
				currentPuzzle = Puzzle.puzzle3;
				break;
			default:
				break;
		}
	}

    void FixedUpdate()
    {
        Debug.Log(currentPuzzle);
        ray = new Ray(transform.position, transform.forward);
        
        linerenderer.positionCount = 1;
        linerenderer.SetPosition(0, transform.position);
        float remainingLength = maxLength;

        for(int i = 0; i < reflections; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
            {
                linerenderer.positionCount += 1;
                linerenderer.SetPosition(linerenderer.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                /*Debug.Log(hit.collider.tag);*/
                
                if (currentPuzzle == Puzzle.puzzle1 && hit.collider.gameObject.tag == "Goal")
                {
                    onGoalAchieved.Invoke();
                    isGoal1Achieved = true;
                    /*GameObject.Destroy(enemy);*/
                    currentPuzzle = Puzzle.puzzle2;
                    Debug.Log("Puzzle 1 completed");
                }
                
                else if (currentPuzzle == Puzzle.puzzle2)
                {
                    if (hit.collider.gameObject.tag == "Goal 2") {
                        isLadderUnlocked = true;
                        currentPuzzle = Puzzle.puzzle3;
                        Debug.Log("Puzzle 2 completed");
                    }
                }
                
                else if (currentPuzzle == Puzzle.puzzle3 && hit.collider.gameObject.tag == "Goal 3")
                {
                    isGoal3Achieved = true;
                    onGoalAchieved.Invoke();
                    currentPuzzle = Puzzle.none;
                    Debug.Log("Puzzle 3 completed");

                }

                /*                if (hit.collider.gameObject == goal && !isGoal1Achieved)
                                {

                                }
                                else if (hit.collider.gameObject.tag == "Goal 2" && !isLadderUnlocked)
                                {

                                }
                                else if (hit.collider.gameObject.tag == "Goal 3" && !isGoal3Achieved)
                                {
                                    isGoal3Achieved = true;
                                    onGoalAchieved.Invoke();
                                }*/
                if (hit.collider.tag != "Mirror")
                    break;
                
            }
            else
            {
                linerenderer.positionCount += 1;
                linerenderer.SetPosition(linerenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }
        }

    }
}
