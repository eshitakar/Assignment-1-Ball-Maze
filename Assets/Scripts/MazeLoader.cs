using UnityEngine;
using System.Collections;

public class MazeLoader : MonoBehaviour {
	public int mazeRows, mazeColumns;
	public GameObject wall;
	public float size;

	private MazeCell[,] mazeCells;

    public GameObject player;
    //public GameObject exit;
    public GameObject camera;
    public GameObject cameraTarget;

    bool first = true;

	// Use this for initialization
	void Start () {
		InitializeMaze ();

		MazeAlgorithm ma = new HuntAndKillMazeAlgorithm (mazeCells);
		ma.CreateMaze ();
        spawnObjects();
        first = true;
	}
	
	// Update is called once per frame
	void Update () {
        /*Vector3 previousAccel = Vector3.zero;
        if (first)
        {
            previousAccel = new Vector3(Input.acceleration.x, 0, -Input.acceleration.z);
        }

        Vector3 temp = new Vector3(Input.acceleration.x, 0, -Input.acceleration.z);
        Vector3 smoothedAccel = Vector3.Lerp(temp, previousAccel, 0.1f);
        previousAccel = smoothedAccel;

        player.transform.Translate(smoothedAccel);*/
    }

	private void InitializeMaze() {

		mazeCells = new MazeCell[mazeRows,mazeColumns];

		for (int r = 0; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++) {
				mazeCells [r, c] = new MazeCell ();

				// For now, use the same wall object for the floor!
				mazeCells [r, c] .floor = Instantiate (wall, new Vector3 (r*size, -(size/2f), c*size), Quaternion.identity) as GameObject;
				mazeCells [r, c] .floor.name = "Floor " + r + "," + c;
				mazeCells [r, c] .floor.transform.Rotate (Vector3.right, 90f);

				if (c == 0) {
					mazeCells[r,c].westWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) - (size/2f)), Quaternion.identity) as GameObject;
					mazeCells [r, c].westWall.name = "West Wall " + r + "," + c;
				}

				mazeCells [r, c].eastWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) + (size/2f)), Quaternion.identity) as GameObject;
				mazeCells [r, c].eastWall.name = "East Wall " + r + "," + c;

				if (r == 0) {
					mazeCells [r, c].northWall = Instantiate (wall, new Vector3 ((r*size) - (size/2f), 0, c*size), Quaternion.identity) as GameObject;
					mazeCells [r, c].northWall.name = "North Wall " + r + "," + c;
					mazeCells [r, c].northWall.transform.Rotate (Vector3.up * 90f);
				}

				mazeCells[r,c].southWall = Instantiate (wall, new Vector3 ((r*size) + (size/2f), 0, c*size), Quaternion.identity) as GameObject;
				mazeCells [r, c].southWall.name = "South Wall " + r + "," + c;
				mazeCells [r, c].southWall.transform.Rotate (Vector3.up * 90f);
			}
		}
	}

    private void spawnObjects()
    {
        int playerx = Random.Range(0, mazeRows - 1);
        int playerz = Random.Range(0, mazeColumns - 1);

        Vector3 playerposition = new Vector3(playerx * (int)size + (int)size, 0, playerz * (int)size + (int)size);

        Instantiate(player, playerposition, Quaternion.identity);
        Instantiate(cameraTarget, playerposition, Quaternion.identity);
        Instantiate(camera, playerposition + new Vector3(0, 5, 0), Quaternion.Euler(90.0f,0.0f,0.0f));

        int exitx;
        int exitz;

        int isHorizontal = Random.Range(0, 1);
        int isEast = Random.Range(0, 1);
        int isNorth = Random.Range(0, 1);

        if (isHorizontal == 0)
        {
            
            if(isEast == 0)
            {
                exitx = Random.Range(0, mazeRows - 1);
                exitz = 0;

            }
            else
            {
                exitx = Random.Range(0, mazeRows - 1);
                exitz = mazeColumns - 1;
            }
        }
        else
        {
           
            if(isNorth == 0)
            {
                exitx = mazeRows - 1;
                exitz = Random.Range(0, mazeColumns - 1);
            }
            else
            {
                exitx = 0;
                exitz = Random.Range(0, mazeColumns - 1);
            }
        }

        //Choose an exit
        if(isHorizontal == 1 && isNorth == 1)
        {
            mazeCells[exitx, exitz].northWall.tag = "Exit";
            var exitRenderer = mazeCells[exitx, exitz].northWall.GetComponent<Renderer>();
            exitRenderer.enabled = false;
            Collider exitCol = mazeCells[exitx, exitz].northWall.GetComponent<Collider>();
            exitCol.isTrigger = true;
            var box = mazeCells[exitx, exitz].northWall.GetComponent<BoxCollider>();
            box.size = new Vector3(0.5f, 1f, 0.5f);
            box.center = new Vector3(-5f, 0f, 0f);
        } else if(isHorizontal == 1 && isNorth == 0)
        {
            mazeCells[exitx, exitz].southWall.tag = "Exit";
            var exitRenderer = mazeCells[exitx, exitz].southWall.GetComponent<Renderer>();
            exitRenderer.enabled = false;
            Collider exitCol = mazeCells[exitx, exitz].southWall.GetComponent<Collider>();
            exitCol.isTrigger = true;
            var box = mazeCells[exitx, exitz].southWall.GetComponent<BoxCollider>();
            box.size = new Vector3(0.5f, 1f, 0.5f);
            box.center = new Vector3(5f, 0f, 0f);
        } else if(isHorizontal == 0 && isEast == 0)
        {
            mazeCells[exitx, exitz].westWall.tag = "Exit";
            var exitRenderer = mazeCells[exitx, exitz].westWall.GetComponent<Renderer>();
            exitRenderer.enabled = false;
            Collider exitCol = mazeCells[exitx, exitz].westWall.GetComponent<Collider>();
            exitCol.isTrigger = true;
            var box = mazeCells[exitx, exitz].westWall.GetComponent<BoxCollider>();
            box.size = new Vector3(0.5f, 1f, 0.5f);
            box.center = new Vector3(0f, 0f, -5f);
        } else
        {
            mazeCells[exitx, exitz].eastWall.tag = "Exit";
            var exitRenderer = mazeCells[exitx, exitz].eastWall.GetComponent<Renderer>();
            exitRenderer.enabled = false;
            Collider exitCol = mazeCells[exitx, exitz].eastWall.GetComponent<Collider>();
            exitCol.isTrigger = true;
            var box = mazeCells[exitx, exitz].eastWall.GetComponent<BoxCollider>();
            box.size = new Vector3(0.5f, 1f, 0.5f);
            box.center = new Vector3(0f, 0f, 5f);
        }
       
        

        /*while (exitx == playerx && exitz == playerz)
        {
            exitx = Random.Range(0, mazeRows - 1);
            exitz = Random.Range(0, mazeColumns - 1);
        }

        Vector3 exitposition = new Vector3(exitx * size, -size / 2f, exitz * size);

        Instantiate(exit, exitposition, Quaternion.identity);*/
    }
}
