using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;


[RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent (typeof(ThirdPersonCharacter))]
public class RandomPointOnNavMesh : MonoBehaviour
{

	public float range = 10.0f;

	public NavMeshAgent m_agent { get; private set; }
	// the navmesh agent required for the path finding
	public ThirdPersonCharacter character { get; private set; }
	// the character we are controlling
	bool hasArrived = false;
	bool isResting = false;

	private void Start ()
	{
		// get the components on the object we need ( should not be null due to require component so no need to check )
		m_agent = GetComponentInChildren<NavMeshAgent> () as NavMeshAgent;
		character = GetComponent<ThirdPersonCharacter> () as ThirdPersonCharacter;

		m_agent.updateRotation = false;
		m_agent.updatePosition = true;

		Vector3 point;
		if (RandomPoint (transform.position, range, out point)) {
			Debug.DrawRay (point, Vector3.up, Color.blue, 1.0f);
		}
		m_agent.SetDestination (point);
	}


	private void Update ()
	{

		if (hasArrived && !isResting) {
			Vector3 point;
			if (RandomPoint (transform.position, range, out point)) {
				Debug.DrawRay (point, Vector3.up, Color.blue, 1.0f);
			}
			m_agent.SetDestination (point);
		}
			

		if (m_agent.remainingDistance > m_agent.stoppingDistance) {
			hasArrived = false;
			character.Move (m_agent.desiredVelocity, false, false);
		} else {
			character.Move (Vector3.zero, false, false);

			if (!isResting) {
				isResting = true;
				Invoke ("RestAtTarget", Random.Range (1f, 6f));
			}
		}
	}

	void RestAtTarget ()
	{
		hasArrived = true;
		isResting = false;
	}

	bool RandomPoint (Vector3 center, float range, out Vector3 result)
	{
		for (int i = 0; i < 30; i++) {
			Vector3 randomPoint = center + Random.insideUnitSphere * range;
			NavMeshHit hit;
			if (NavMesh.SamplePosition (randomPoint, out hit, 1.0f, NavMesh.AllAreas)) {
				result = hit.position;
				return true;
			}
		}
		result = Vector3.zero;
		return false;
	}


}
