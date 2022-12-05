using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Linq;

public class Node{
    public Node parent;
    public float runningCost;
    public Dictionary<string, int> state;
    public GAction action;
    public Node(Node parent, float runningCost, Dictionary<string, int> state, GAction action)
    {
        this.parent = parent;
        this.runningCost = runningCost;
        this.state = new Dictionary<string, int>(allstates);
        this.action = action;
    }
}

public class GPlanner : MonoBehaviour
{
    public Queue<GAction> plan(List<GAction> actions, Dictionary<string, int> goal, Dictionary<string, int> worldState)
    {
        List<Node> leaves = new List<Node>();
        foreach (GAction a in actions)
        {
            if (a.IsAchievableGiven(worldState))
            {
                usableActions.Add(a);
            }
        }

        List<Node> frontier = new List<Node>();
         Node start = new Node(null, 0, GWorld.Instance.GetWorld().GetStates(), null);

        bool success = BuildGraph(start, leaves, usableActions, goal);

        if (!success)
        {
            Debug.Log("NO PLAN");
            return null;
        }

        Node cheapest = null;
        foreach (Node leaf in leaves)
        {
            if (cheapest == null)
            {
                cheapest = leaf;
            }
            else
            {
                if (leaf.runningCost < cheapest.runningCost)
                {
                    cheapest = leaf;
                }
            }
        }

        List<GAction> result = new List<GAction>();
        Node n = cheapest;
        while (n != null)
        {
            if (n.action != null)
            {
                result.Insert(0, n.action);
            }
            n = n.parent;
        }

        Queue<GAction> queue = new Queue<GAction>();
        foreach (GAction a in result)
        {
            queue.Enqueue(a);
        }

        Debug.Log("The Plan is: ");
        foreach (GAction a in queue)
        {
            Debug.Log(a.actionName);
        }

        return queue;
    }

}
