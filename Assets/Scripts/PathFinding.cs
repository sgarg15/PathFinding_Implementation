﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {

  public Transform seeker, target;

  Grid grid;

  void Awake(){
    grid = GetComponent<Grid>();
  }

  void Update(){
    FindPath(seeker.position, target.position);
  }
  void FindPath(Vector3 startPosition, Vector3 targetPosition){
    Node startNode = grid.NodeFromWorldPoint(startPosition);
    Node targetNode = grid.NodeFromWorldPoint(targetPosition);

    List<Node> openSet = new List<Node>();
    HashSet<Node> closedSet = new HashSet<Node>();
    openSet.Add(startNode);

    while (openSet.Count > 0){
      Node currentNode = openSet[0];
      for (int i = 1; i < openSet.Count; i ++) {
        if(openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost) {
          currentNode = openSet[i];
        }
      }
      openSet.Remove(currentNode);
      closedSet.Add(currentNode);

      if(currentNode == targetNode) {
        RetracePath(startNode, targetNode);
        return;
      }

      foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
        if(!neighbour.walkable || closedSet.Contains(neighbour)){
          continue;
        }
        int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
        if(newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)){
          neighbour.gCost = newMovementCostToNeighbour;
          neighbour.hCost = GetDistance(neighbour, targetNode);
          neighbour.parent = currentNode;

          if(!openSet.Contains(neighbour)){
            openSet.Add(neighbour);
          }
        }
      }
    }
  }

  void RetracePath(Node startNode, Node endNode){
    List<Node> path = new List<Node> ();
    Node currentNode = endNode;

    while (currentNode != startNode){
      path.Add(currentNode);
      currentNode = currentNode.parent;
    }
    path.Reverse();

    grid.path = path;
  }

  int GetDistance(Node nodeA, Node nodeB) {
    int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
    int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

    if(distX > distY){
      return 14*distY + 10 * (distX-distY);
    } else {
      return 14*distX + 10 * (distY-distX);
    }

  }
}
