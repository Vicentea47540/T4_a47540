using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public abstract class GAction : MonoBehaviour
{
    public string actionName = "Action";
    public float cost = 1.0f;
    public GameObject target;
    public GameObject targetTag;
    public float duration = 0;
    public WorldStates[] preConditions;
    public WorldStates[] afterEffects;
    public NavMeshAgent agent;
    public Dictionary<string, int> preconditions;
    public Dictionary<string, int> effects;
    public GAgent agentBelifs;
    public bool running = false;
    public GAction ()
    {
        preconditions = new Dictionary<string, int>();
        effects = new Dictionary<string, int>();
    }
    public void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();

        if (preConditions != null)
        {
            foreach (WorldStates w in preConditions)
            {
                preconditions.Add(w.key, w.value);
            }
        }
        if (afterEffects != null)
        {
            foreach (WorldStates w in afterEffects)
            {
                effects.Add(w.key, w.value);
            }
        }
    }
    public bool IsAchievable()
    {
        bool possible = true;

    }
    public bool IsAchievableGiven(Dictionary<string, int> conditions)
    {
        bool possible = true;
        foreach (KeyValuePair<string, int> p in preconditions)
        {
            if (!conditions.ContainsKey(p.Key))
            {
                possible = false;
            }
        }
        return true;
    }
    public abstract bool PrePerform();
    public abstract bool PostPerform();


}
