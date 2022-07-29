using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricEffect : MonoBehaviour
{
    public ElectricArc electricArcPrefab;
    private static List<ElectricEffect> activeEffects = new List<ElectricEffect>();
    private List<ElectricArc> electricArcInstances = new List<ElectricArc>();
    public float damagePerSecond = 2;
    public LayerMask enemyLayerMask;

    void Start()
    {
        GetComponent<DiceEffectRoot>().activationFinishedDelegate += OnActivationFinished;
        ElectricArc arc = Instantiate(electricArcPrefab, transform);
        electricArcInstances.Add(arc);
        arc.target = KeyboardMovement.instance.gameObject;
        arc.origin = gameObject;
        foreach(ElectricEffect effect in activeEffects)
        {
            arc = Instantiate(electricArcPrefab, transform);
            electricArcInstances.Add(arc);
            arc.target = effect.gameObject;
            arc.origin = gameObject;
        }
        activeEffects.Add(this);
    }

    private void OnDestroy()
    {
        activeEffects.Remove(this);
        foreach(ElectricArc arc in electricArcInstances)
            Destroy(arc.gameObject);
        electricArcInstances.Clear();
    }

    private void OnActivationFinished()
    {
        foreach(ElectricArc arc in electricArcInstances)
            Destroy(arc.gameObject);
        electricArcInstances.Clear();
    }

    private void Update()
    {
        for(int i=electricArcInstances.Count-1; i>=0; i--)
        {
            if(electricArcInstances[i].target == null || electricArcInstances[i].origin == null)
            {
                Destroy(electricArcInstances[i].gameObject);
                electricArcInstances.RemoveAt(i);
            }
        }
        foreach(ElectricArc arc in electricArcInstances)
        {
            Vector3 direction = arc.target.transform.position - arc.origin.transform.position;
            RaycastHit[] hits = Physics.RaycastAll(arc.transform.position, direction, direction.magnitude, enemyLayerMask);
            foreach(RaycastHit hit in hits)
            {
                DamageReceiver damageReceiver = hit.collider.GetComponent<DamageReceiver>();
                if(damageReceiver != null)
                    damageReceiver.OnDamageReceived(damagePerSecond * Time.deltaTime, Vector3.zero);
            }
        }
    }
}
