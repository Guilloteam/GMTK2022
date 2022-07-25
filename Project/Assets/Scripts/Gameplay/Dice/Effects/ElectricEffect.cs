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
        arc.target = KeyboardMovement.instance.transform;
        foreach(ElectricEffect effect in activeEffects)
        {
            arc = Instantiate(electricArcPrefab, transform);
            electricArcInstances.Add(arc);
            arc.target = effect.transform;
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
        foreach(ElectricArc arc in electricArcInstances)
        {
            Vector3 direction = arc.target.position - arc.transform.position;
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
