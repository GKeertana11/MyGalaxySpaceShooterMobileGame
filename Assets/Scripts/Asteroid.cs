using UnityEngine;
using System.Collections;

// Class for asteroid objects.
public class Asteroid : MonoBehaviour
{

	#region PRIVATE VARIABLES
	private bool isLarge;

	private Vector2 force;

	private SpriteRenderer spriteRenderer;
	private PolygonCollider2D polyCollider;
	Rigidbody2D rigidbody;

	private int points = 100;
	private GameManager gameManager;
	#endregion

	#region MONOBEHAVIOUR METHODS
	void OnEnable()//Active on screen
	{
		isLarge = (Random.Range(0, 2) == 0);//if 0-true else false.
		ResetFromPrefab();
		ApplyForce();
	}

	void OnDisable()
	{
		rigidbody.angularVelocity = 0f;
		rigidbody.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}

	void Awake()
	{
		rigidbody = GetComponentInChildren<Rigidbody2D>();
		gameManager = GameManager.Instance;
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		polyCollider = (PolygonCollider2D)GetComponentInChildren<Collider2D>();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("Trigger Method");
		if (collision.gameObject.layer.ToString()== Constants.SHIP_PREFAB_NAME)
		{
			Debug.Log("Hit spaceship");
			collision.gameObject.GetComponent<ShipScript>().OnHit();

			if (!isLarge)
				PoolManager.Instance.Recycle(Constants.ASTEROID_PREFAB_NAME, gameObject);
			else
				BreakApart();
		}
		else if (collision.gameObject.layer.ToString()== Constants.BULLET_PREFAB_NAME)
		{
			Debug.Log("Hit by bullet");
			PoolManager.Instance.Recycle(Constants.BULLET_PREFAB_NAME, collision.transform.parent.gameObject);

			if (!isLarge)
				PoolManager.Instance.Recycle(Constants.ASTEROID_PREFAB_NAME, gameObject);
			else
				
			BreakApart();

			gameManager.GainPoints(points);
		}
	}
	#endregion

	#region PUBLIC METHODS
	// Get the force applied to the asteroid.
	public Vector2 GetForceApplied()
	{
		return force;
	}

	// Apply a random force to the asteroid.
	public void ApplyForce()
	{
		float torque = (Random.Range(0, 2) == 0 ? -1 : 1) * RandomNormalDistributionInRange(0.5f, 4f);
		rigidbody.AddTorque(torque);

		force = new Vector2((Random.Range(0, 2) == 0 ? -1 : 1) * RandomNormalDistributionInRange(8, 50), (Random.Range(0, 2) == 0 ? -1 : 1) * RandomNormalDistributionInRange(8, 50));
		rigidbody.AddForce(force);
	}

	// Set the size of an asteroid to large or small.
	public void SetSize(bool _isLarge)
	{
		isLarge = _isLarge;
		ResetFromPrefab();
	}
	#endregion

	#region PRIVATE METHODS
	// Break apart a large asteroid into small ones.
	private void BreakApart()
	{
		int numChunks = Random.Range(2, 4);

		for (int i = 0; i < numChunks; i++)
		{
			Asteroid chunk = PoolManager.Instance.Spawn(Constants.ASTEROID_PREFAB_NAME).GetComponent<Asteroid>();

			chunk.transform.position = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f);
			chunk.transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));

			chunk.SetSize(false);
			chunk.ApplyForce();
		}

		PoolManager.Instance.Recycle(Constants.ASTEROID_PREFAB_NAME, gameObject);
	}

	// Get an asteroid prefab and set this asteroid's parameters to match.
	private void ResetFromPrefab()
	{
		if (isLarge)
		{
			GameObject prefab = PrefabManager.Instance.GetLargeAsteroidPrefab();
			spriteRenderer.sprite = prefab.GetComponentInChildren<SpriteRenderer>().sprite;

		PolygonCollider2D prefabCollider = ((PolygonCollider2D)prefab.GetComponentInChildren<Collider2D>());
			polyCollider.pathCount = prefabCollider.pathCount;//pathcount counts no of edges of polygoncollider. 
			

			for (int i = 0; i < prefabCollider.pathCount; i++)
				polyCollider.SetPath(i, prefabCollider.GetPath(i));
		}
		else
		{
			GameObject prefab = PrefabManager.Instance.GetSmallAsteroidPrefab();
			spriteRenderer.sprite = prefab.GetComponentInChildren<SpriteRenderer>().sprite;

			PolygonCollider2D prefabCollider = ((PolygonCollider2D)prefab.GetComponentInChildren<Collider2D>());
			polyCollider.pathCount = prefabCollider.pathCount;

			for (int i = 0; i < prefabCollider.pathCount; i++)
				polyCollider.SetPath(i, prefabCollider.GetPath(i));
		}
	}

	// Get a random number in a range from a normal distribution.
	private float RandomNormalDistributionInRange(float min, float max)
	{
		float mean = (min + max) / 2f;//average force
		float standardDeviation = (max - mean) / 3f;//

		float rand = RandomNormalDistribution() * standardDeviation + mean;

		if (rand > max)
			rand = max;
		else if (rand < min)
			rand = min;

		return rand;
	}

	// Get a random number in the range -1 - 1 from a normal distribution.
	private float RandomNormalDistribution()
	{
		float u = 0f;
		float v = 0f;
		float S = 1.0f;

		while (S >= 1.0f)
		{
			u = 2.0f * Random.value - 1.0f;
			v = 2.0f * Random.value - 1.0f;
			S = u * u + v * v;
		}

		float fac = Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);

		return u * fac;
	}
	#endregion
}