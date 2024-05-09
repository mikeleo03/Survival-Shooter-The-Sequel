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
	public int healingPetPrice = 100;
	public int attackingPetPrice = 100;

	Canvas canvas;
	Transform player;
	Text balanceText;
	PlayerCurrency playerCurrency;
	

	void Awake()
	{
	}

	void Start()
	{
		canvas = GetComponent<Canvas>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		playerCurrency = player.GetComponent<PlayerCurrency>();
		balanceText = transform
			.Find("ShopPanel")
			.Find("BalancePlaceholder")
			.GetComponent<Text>();
		Text healingPetText = transform
			.Find("ShopPanel")
			.Find("HealingPetButton")
			.Find("Text")
			.GetComponent<Text>();
		Text attackingPetText = transform
			.Find("ShopPanel")
			.Find("AttackingPetButton")
			.Find("Text")
			.GetComponent<Text>();
		healingPetText.text += "\n$"+healingPetPrice;
		attackingPetText.text += "\n$"+attackingPetPrice;

	}
	
	void Update()
	{
		balanceText.text = playerCurrency.Balance().ToString();
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
		if (playerCurrency.Balance() < healingPetPrice) {
			return;
		}
		playerCurrency.subtract(healingPetPrice);
        Instantiate(healingPet, player.position, Quaternion.identity);     
	}

	public void SpawnAttackingPet()
	{
		if (playerCurrency.Balance() < attackingPetPrice) {
			return;
		}
		playerCurrency.subtract(attackingPetPrice);
		Instantiate(attackingPet, player.position, Quaternion.identity);
	}	
}
