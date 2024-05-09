using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class ShopManager : MonoBehaviour {

	public GameObject healingPet;
	public GameObject attackingPet;
	public int healingPetPrice = 100;
	public int attackingPetPrice = 100;

	public float messageDelay = 1f;

	
	Transform player;
	Text balanceText;
	PlayerCurrency playerCurrency;
	GameObject shopPanel;
	GameObject messagePanel;
	Canvas canvas;
	InputAction shopAction;
	float messageTimer;

	bool isAccessible;

	void Awake()
	{
		shopAction = ControlRef.control.Player.Shop;
	}

    void Start()
	{
		canvas = GetComponent<Canvas>();
		canvas.enabled = false;
		player = GameObject.FindGameObjectWithTag("Player").transform;
		
		shopPanel = transform.Find("ShopPanel").gameObject;
		messagePanel = transform.Find("MessagePanel").gameObject;
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

		shopPanel.SetActive(false);
		messagePanel.SetActive(false);
	}
	
	void Update()
	{
		balanceText.text = playerCurrency.Balance().ToString();
		messageTimer -= Time.deltaTime;
		if (isAccessible && shopAction.WasPressedThisFrame()) 
		{
			if (shopPanel.activeSelf)
			{
				showNothing();
				return;
			}

			resetCanvas();
			showShop();
			return;
		} 
		else if (isAccessible && !shopPanel.activeSelf) 
		{
			resetCanvas();
			showMessage("Press key B to open shop");
			return;
		}   
		else if (!isAccessible && shopAction.WasPressedThisFrame()) 
		{
			messageTimer = messageDelay; 
			resetCanvas();
			showMessage("Go to the shopkeeper to access the shop.");
			return;
		}
		if (messageTimer < 0.01f && !shopPanel.activeSelf) 
		{
			showNothing();
		}
	}
	

    public void Exit()
	{
		shopPanel.SetActive(false);
	}

	public void SpawnHealingPet()
	{
		if (playerCurrency.Balance() < healingPetPrice) {
			return;
		}
		playerCurrency.subtract(healingPetPrice);
        GameObject pet = Instantiate(healingPet, player.position, Quaternion.identity);     
		pet.transform.SetParent(player, false);
	}

	public void SpawnAttackingPet()
	{
		if (playerCurrency.Balance() < attackingPetPrice) {
			return;
		}
		playerCurrency.subtract(attackingPetPrice);
		GameObject pet = Instantiate(attackingPet, player.position, Quaternion.identity);
		pet.transform.SetParent(player, false);
	}	

	public void setAccessible(bool value)
	{
		isAccessible = value;
	}
	void resetCanvas()
	{
		canvas.enabled = false;
		canvas.enabled = true;
	}
	void showShop()
	{
		shopPanel.SetActive(true);
		messagePanel.SetActive(false);
	}

	void showMessage(String message="")
	{
		shopPanel.SetActive(false);
		messagePanel.SetActive(true);
		Text messageText = messagePanel.transform.Find("Text").GetComponent<Text>();
		messageText.text = message;
	}

	void showNothing() 
	{
		shopPanel.SetActive(false);
		messagePanel.SetActive(false);
	}
}
