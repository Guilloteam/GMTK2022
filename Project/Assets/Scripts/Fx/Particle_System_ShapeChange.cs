using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_System_ShapeChange : MonoBehaviour
{
	public ParticleSystem Vent;
	public float ShapeRadius = .7f;
	public float timeVar = 0;
	public float ShapeRadiusStop = 2f;
	public float ShapeRadiusAdd = .5f;	
    // Start is called before the first frame update
    void Start()
    {
        Vent = GetComponent<ParticleSystem>();
	
    }

    // Update is called once per frame
    void Update()
    {
        
	timeVar += Time.deltaTime;
	if (timeVar > .5)
		{
		timeVar = 0;
		if (ShapeRadius < ShapeRadiusStop)
			ShapeRadius += ShapeRadiusAdd;
		if (ShapeRadius > ShapeRadiusStop)
			ShapeRadius = ShapeRadiusStop;

		}

	ParticleSystem.MainModule psMain = Vent.main;
	ParticleSystem.ShapeModule psShape = Vent.shape;
	psMain.startLifetime = Vent.startLifetime;
	psMain.startSpeed = Vent.startSpeed;
	psShape.radius = ShapeRadius;

	//Vent.Emit (1);
	
    }
}
