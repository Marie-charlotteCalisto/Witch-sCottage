using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuild : MonoBehaviour
{
    private bool building = false;
    public bool GridSnaping = true;
    private bool SnapWall = false;
    public GameObject start;
    public GameObject end;
    
    public GameObject wallPrefab;
    private GameObject wall;

    private List<GameObject> walls;

    // Start is called before the first frame update
    void Start()
    {
        walls = new List<GameObject>();        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
            SetStart();
        else if (Input.GetMouseButtonUp(0))
            SetEnd();
        else if (Input.GetKeyDown(KeyCode.LeftShift))
            SnapWall = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            SnapWall = false;
        else if (building)
            Adjust();
    }

    private void SetStart()
    {
        building = true;

        Vector3 startPos = GetWorldPoint();
        if (GridSnaping)
            startPos = GridSnap(startPos);

        start.transform.position = startPos;
        wall = (GameObject)Instantiate(wallPrefab, start.transform.position, Quaternion.identity);

        if (SnapWall & walls.Count != 0)
            start.transform.position = closestWallSnap(GetWorldPoint()).transform.position;
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

    private void SetEnd()
    {
        building = false;
        UpdateEndPos();
        SetEndPoles();
    }
    
    private void SetEndPoles()
    {
        GameObject p1 = (GameObject)Instantiate(wallPrefab, start.transform.position, start.transform.rotation);
        GameObject p2 = (GameObject)Instantiate(wallPrefab, end.transform.position, end.transform.rotation);
    
        p1.tag = "wall";
        p2.tag = "wall";
        walls.Add(p1);
        walls.Add(p2);
    }

    void Adjust()
    {
        UpdateEndPos();

        start.transform.LookAt(end.transform.position);
        end.transform.LookAt(start.transform.position);
        
        wall.transform.position = 0.5f * (end.transform.position + start.transform.position);
        wall.transform.rotation = start.transform.rotation;

        float distance = Vector3.Distance(start.transform.position, end.transform.position);
        wall.transform.localScale = new Vector3(wall.transform.localScale.x, wall.transform.localScale.y ,distance);
    }

    Vector3 GetWorldPoint()
    {
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
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
