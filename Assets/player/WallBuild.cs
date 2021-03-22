using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuild : MonoBehaviour
{
    private bool BuildWall = false;
    private bool building = false;
    public bool GridSnaping = true;
    private bool SnapEdge = false;
    public GameObject edgePrefab;
    private GameObject start;
    private GameObject end;
    
    public GameObject wallPrefab;
    private GameObject wall;
    public Camera cam;


    private List<GameObject> walls;

    // Start is called before the first frame update
    void Start()
    {
        walls = new List<GameObject>();
    }

    void OnDisable()
    {
        BuildWall = false;
    }

    public void SetBuildMode()
    {
        BuildWall = !BuildWall;
        Destroy(wall);
        Destroy(start);
        Destroy(end);
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (BuildWall)
        {
            if (Input.GetMouseButtonDown(1))
            {
                BuildWall = false;
                return;
            }
            if (Input.GetMouseButtonDown(0))
                SetStart();
            else if (Input.GetMouseButtonUp(0))
                SetEnd();
            else if (Input.GetKeyDown(KeyCode.LeftShift))
                SnapEdge = true;
            else if (Input.GetKeyUp(KeyCode.LeftShift))
                SnapEdge = false;
            if (building)
                Adjust();
        }
    }

    private void SetStart()
    {
        building = true;

        var startpos = new Vector3(GetWorldPoint().x, 0, GetWorldPoint().z);
        if (GridSnaping)
            startpos = GridSnap(startpos);

        start = (GameObject)Instantiate(edgePrefab, startpos, Quaternion.identity);
        end = (GameObject)Instantiate(edgePrefab, startpos, Quaternion.identity);

        wall = (GameObject)Instantiate(wallPrefab, startpos, Quaternion.identity);

        if (SnapEdge & walls.Count != 0)
            start.transform.position = closestWallSnap(GetWorldPoint()).transform.position;
    }

    private void SetEnd()
    {
        building = false;
        UpdateEndPos();
        SetEndPoles();
    }

    void Adjust()
    {
        UpdateEndPos();

        start.transform.LookAt(end.transform.position);
        end.transform.LookAt(start.transform.position);

        end.transform.rotation = Quaternion.Euler(0, end.transform.eulerAngles.y, end.transform.eulerAngles.z);
        start.transform.rotation = Quaternion.Euler(0, start.transform.eulerAngles.y, start.transform.eulerAngles.z);

        end.transform.position = new Vector3(end.transform.position.x, 3, end.transform.position.z);
        start.transform.position = new Vector3(start.transform.position.x, 3, start.transform.position.z);
        
        wall.transform.position = 0.5f * (end.transform.position + start.transform.position);
        wall.transform.rotation = start.transform.rotation;

        float distance = Vector3.Distance(start.transform.position, end.transform.position);
        wall.transform.localScale = new Vector3(wall.transform.localScale.x, wall.transform.localScale.y ,distance);
    }


    GameObject closestWallSnap(Vector3 worldPoint)
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        float currentDistance = Mathf.Infinity;
        foreach (GameObject w in walls)
        {
            currentDistance = Vector3.Distance(worldPoint, w.transform.position);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = w;
            }
        }
        return closest;
    }
    
    private void UpdateEndPos()
    {
        Vector3 endPos = GetWorldPoint();
        if (GridSnaping)
            endPos = GridSnap(endPos);

        end.transform.position = endPos;

    }
    
    private void SetEndPoles()
    {
        start.tag = "wall";
        end.tag = "wall";
        walls.Add(start);
        walls.Add(end);
    }

    Vector3 GetWorldPoint()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            return (hit.point + new Vector3(0,2.5f,0));
        return Vector3.zero;
    }

    Vector3 GridSnap(Vector3 originalPosition)
    {
        int granularity = 1;
        float x = Mathf.Floor(originalPosition.x/granularity)*granularity;
        float y = originalPosition.y;
        float z = Mathf.Floor(originalPosition.z/granularity)*granularity;

        return new Vector3(x,y,z);
    }
}
