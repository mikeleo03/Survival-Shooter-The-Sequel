using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ShopManager : MonoBehaviour {

	public GameObject healingPet;
	public GameObject attackingPet;
	
	Canvas canvas;
	Transform player;

	void Awake()
	{
		canvas = GetComponent<Canvas>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Start()
	{
		canvas = GetComponent<Canvas>();
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.B))
		{
			canvas.enabled = !canvas.enabled;
		}
	}

    public void Exit()
	{
		canvas.enabled = false;
	}

	public void SpawnHealingPet()
	{
        Instantiate(healingPet, player.position, Quaternion.identity);     
	}

	public void SpawnAttackingPet()
	{
		Instantiate(attackingPet, player.position, Quaternion.identity);
	}	
}
