using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
	[Header("Kcal")]
	[SerializeField] private float kcal=3;
	[SerializeField] private float soiling = 2;

	[Header("Snap Settings")]
	bool isSnaping;
	bool throwing;
	[SerializeField] private Rigidbody myRigidbody;

	public void Throw(float angle, float speed, float Distance, Transform player)
	{
		if (!isSnaping)
        {

			if (!throwing)
            {
				myRigidbody.AddForce((Vector3.right + Vector3.up*2) * 1.5f, ForceMode.Impulse);
				throwing = true;
            }
			//transform.Translate((Vector3.right * 2 + Vector3.up).normalized * speed * Time.deltaTime, Space.World);
        }
		else
		{
			myRigidbody.velocity = Vector3.zero;
			myRigidbody.useGravity = false;
			transform.Translate((player.position - transform.position).normalized * speed * Time.deltaTime, Space.World);
			if (Vector3.Distance(transform.position, player.position) < 0.5f)
			{
				Eat();
			}
		}

		if(Vector3.Distance(player.position, transform.position) > Distance)
		{
			isSnaping = true;
		}
		void Eat()
		{
			if (player.TryGetComponent(out CharacterStats result))
			{
				result.Hunger(kcal);
				result.Dirty(soiling);
			}
			Destroy(transform.gameObject);
		}
	}
}
