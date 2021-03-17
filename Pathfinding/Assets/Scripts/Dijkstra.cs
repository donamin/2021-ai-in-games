using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Connection
{
    public int from, to;
    public float cost;

    public Connection(int from, int to)
    {
        this.from = from;
        this.to = to;
    }
}

public struct NodeRecord
{
    public int node, connection;
    public float costSoFar;
}

public class Dijkstra : MonoBehaviour
{
    public GameObject[] nodes;

    List<Connection> connections = new List<Connection>();

    const float connectionDrawOffset = 0.25f;

    int startNode = 7, goalNode = 4;
    int iteration = 0;
    List<NodeRecord> openList = new List<NodeRecord>();
    List<NodeRecord> closedList = new List<NodeRecord>();
    NodeRecord currentNode;
    string pathfindingStatus = "Initialized...";
    List<int> finalPath;
    bool foundPath = false;

    // Start is called before the first frame update
    void Start()
    {
        connections.Add(new Connection(0, 1));
        connections.Add(new Connection(0, 5));
        connections.Add(new Connection(1, 2));
        connections.Add(new Connection(1, 3));
        connections.Add(new Connection(2, 0));
        //connections.Add(new Connection(3, 1));
        connections.Add(new Connection(3, 4));
        connections.Add(new Connection(3, 8));
        //connections.Add(new Connection(4, 1));
        connections.Add(new Connection(4, 6));
        connections.Add(new Connection(5, 0));
        connections.Add(new Connection(5, 9));
        //connections.Add(new Connection(6, 1));
        connections.Add(new Connection(7, 2));
        connections.Add(new Connection(8, 6));
        connections.Add(new Connection(9, 4));

        NodeRecord startRecord;
        startRecord.node = startNode;
        startRecord.connection = -1;
        startRecord.costSoFar = 0;
        openList.Add(startRecord);

        currentNode.node = -1;

        foreach (Connection conn in connections)
        {
            conn.cost = Vector3.Distance(nodes[conn.from].transform.position, nodes[conn.to].transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Run one Dijkstra iteration
            if(!foundPath && openList.Count > 0)
            {
                iteration++;
                pathfindingStatus = "In progress...";
                currentNode = FindSmallestOpenNode();
                if (currentNode.node == goalNode)
                {
                    //We have found the goal, so we should terminate.
                }
                else
                {
                    List<int> connectionIDs = GetConnections(currentNode.node);
                    foreach(int id in connectionIDs)
                    {
                        int endNode = connections[id].to;
                        if (Contains(closedList, endNode) != -1)
                            continue;
                        NodeRecord endNodeRecord;
                        float endNodeCost = currentNode.costSoFar + connections[id].cost;
                        int indexInOpenList = Contains(openList, endNode);
                        if (indexInOpenList > -1)
                        {
                            //Make sure that the new found route is better than the previous one
                            endNodeRecord = openList[indexInOpenList];
                            if (endNodeRecord.costSoFar <= endNodeCost)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            //This is an unvisited node, so we need to store it in the open list.
                            endNodeRecord.node = endNode;
                        }
                        endNodeRecord.costSoFar = endNodeCost;
                        endNodeRecord.connection = id;
                        if (indexInOpenList > -1)
                        {
                            //Update the statistics in the open list
                            openList[indexInOpenList] = endNodeRecord;
                        }
                        else
                        {
                            //Otherwise, add the new node to the open list.
                            openList.Add(endNodeRecord);
                        }
                    }
                    openList.Remove(currentNode);
                    closedList.Add(currentNode);
                }
                if(currentNode.node == goalNode)
                {
                    //We have found a path
                    finalPath = new List<int>();
                    string path = string.Format("{0}", goalNode);
                    while(currentNode.node != startNode)
                    {
                        finalPath.Add(currentNode.connection);
                        path = string.Format("{0} -> ", connections[currentNode.connection].from) + path;
                        int sourceNode = connections[currentNode.connection].from;
                        int indexInClosedList = Contains(closedList, sourceNode);
                        currentNode = closedList[indexInClosedList];
                    }
                    pathfindingStatus = "Terminated, path: " + path;
                    foundPath = true;
                }
                else if(openList.Count == 0)
                {
                    //Search has failed. No paths found.
                    pathfindingStatus = "Terminated, no path!";
                }
            }
            else
            {
                iteration = 0;
                openList.Clear();
                NodeRecord startRecord;
                startRecord.node = startNode;
                startRecord.connection = -1;
                startRecord.costSoFar = 0;
                openList.Add(startRecord);
                closedList.Clear();
                pathfindingStatus = "Initialized...";
                foundPath = false;
                currentNode.node = -1;
            }
        }
    }

    NodeRecord FindSmallestOpenNode()
    {
        int minNode = -1;
        float minCost = float.MaxValue;
        for(int i = 0; i < openList.Count; i++)
        {
            if(openList[i].costSoFar < minCost)
            {
                minCost = openList[i].costSoFar;
                minNode = i;
            }
        }
        NodeRecord result = openList[minNode];
        openList.RemoveAt(minNode);
        return result;
    }

    List<int> GetConnections(int from)
    {
        List<int> conns = new List<int>();
        for(int c = 0; c < connections.Count; c++)
        {
            if(connections[c].from == from)
            {
                conns.Add(c);
            }
        }
        return conns;
    }

    int Contains(List<NodeRecord> list, int node)
    {
        for(int i = 0; i < list.Count; i++)
        {
            if(list[i].node == node)
            {
                return i;
            }
        }
        return -1;
    }

    void OnDrawGizmos()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.normal.textColor = Color.white;
        Handles.Label(new Vector3(0, 0, 0), string.Format("Iteration {0}", iteration), style);
        string openStr = "";
        for (int i = 0; i < openList.Count; i++)
        {
            openStr += string.Format("{0} (from {1}, cost {2:0.0})"
                , openList[i].node
                , openList[i].connection == -1 ? -1 : connections[openList[i].connection].from
                , openList[i].costSoFar);
            if(i + 1 < openList.Count)
                openStr += ", ";
        }
        Handles.Label(new Vector3(0, -0.6f, 0), string.Format("Open list: {0}", openStr), style);
        string closedStr = "";
        for (int i = 0; i < closedList.Count; i++)
        {
            closedStr += closedList[i].node.ToString();
            if (i + 1 < closedList.Count)
                closedStr += ", ";
        }
        Handles.Label(new Vector3(0, -1.2f, 0), string.Format("Closed list: {0}", closedStr), style);
        Handles.Label(new Vector3(0, -1.8f, 0), pathfindingStatus, style);

        for (int i = 0; i < nodes.Length; i++)
        {
            style.fontSize = 16;
            if (iteration > 0 && i == currentNode.node)
            {
                style.fontSize = 20;
                style.fontStyle = FontStyle.Bold;
                style.normal.textColor = Color.yellow;
            }
            else if (i == startNode)
            {
                style.fontSize = 20;
                style.fontStyle = FontStyle.Bold;
                style.normal.textColor = Color.green;
            }
            else if (i == goalNode)
            {
                style.fontSize = 20;
                style.fontStyle = FontStyle.Bold;
                style.normal.textColor = Color.red;
            }
            else
                style.normal.textColor = Color.white;
            Handles.Label(nodes[i].transform.position - new Vector3(0.1f, -0.3f, 0), string.Format("{0}", i), style);
        }
        for(int i = 0; i < connections.Count; i++)
        {
            Connection conn = connections[i];
            Vector3 start = nodes[conn.from].transform.position;
            Vector3 end = nodes[conn.to].transform.position;
            Vector3 fwd = (end - start).normalized * connectionDrawOffset;
            Vector3 rgt = new Vector3(fwd.y, -fwd.x, fwd.z);
            Vector3 mid = (start + end) / 2 + rgt;
            Color c = Color.white;
            if ((foundPath && finalPath.Contains(i)))
                c = Color.green;
            else if (conn.from == currentNode.node)
                c = Color.yellow;
            Debug.DrawLine(start + rgt, end + rgt, c);
            Debug.DrawLine(mid, mid -fwd + rgt, c);
            Debug.DrawLine(mid, mid - fwd - rgt, c);
            style.fontSize = 20;
            style.normal.textColor = c;
            Handles.Label(mid + rgt, string.Format("{0:0.0}", conn.cost), style);
        }
    }
}