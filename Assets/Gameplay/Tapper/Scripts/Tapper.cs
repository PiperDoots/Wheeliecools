using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;
using TMPro;

public class Tapper : MonoBehaviour
{
	// Fill up glasses with a liquid, currently very very manual with Keys

	public GlassSpot glassSpot;
	
	[SerializeField] private Renderer selector;
	[SerializeField] private TextMeshPro drinkTitle;
	[SerializeField] private TextMeshPro liquidCounter;

    private Liquid[] liquids = new Liquid[11];
    private InventoryManager Inventory;
	private int liquidIndex = 0;

	[Header("Keybindings")]
	public Key fillKey = Key.Space;
	public Key nextColour = Key.E;
	public Key prevColour = Key.Q;
	public Key resetGlass = Key.R;

	public Glass SelectedGlass => glassSpot?.CurrentGlass;
	public Color ActiveColour => (liquids != null && liquids.Length > 0) ? liquids[liquidIndex].color : Color.white;
	public Liquid ActiveLiquid => (liquids != null && liquids.Length > 0) ? liquids[liquidIndex] : null;

	public float LiquidLeft;
	void Start()
	{
		Inventory = InventoryManager.Instance;
		Array.Copy(Inventory.Liquids,liquids,Inventory.Liquids.Length);
	}

	void Update()
	{
		if (glassSpot == null) return;

		HandleLiquidSelection();
		HandleFilling();
		HandleReset();
		HandleOptics();
	}

	private void HandleLiquidSelection()
	{
		if (liquids == null || liquids.Length == 0) return;

		if (Keyboard.current[nextColour].wasPressedThisFrame)
		{
			liquidIndex = (liquidIndex + 1) % liquids.Length;
		}
		if (Keyboard.current[prevColour].wasPressedThisFrame)
		{
			liquidIndex = (liquidIndex - 1 + liquids.Length) % liquids.Length;
		}
	}

	private void HandleFilling()
	{
		if (!Keyboard.current[fillKey].isPressed) return;

		Glass target = SelectedGlass;
		if (target == null || target.IsFull) return;

		float amount = target.FillDelta;
		if(Inventory.AmountOfLiquid(ActiveLiquid) > amount){
			target.AddFill(amount, ActiveLiquid);
			Inventory.TakeLiquid(ActiveLiquid, amount * target.Capacity); //Multiply by the capacity to get how many cL
		}
	}

	private void HandleReset()
	{
		if (Keyboard.current[resetGlass].wasPressedThisFrame)
			SelectedGlass?.ResetGlass();
	}

	private void HandleOptics()
	{
		selector.material.color = ActiveColour;
		drinkTitle.text = ActiveLiquid.liquidName;
		liquidCounter.text = Math.Round(Inventory.AmountOfLiquid(ActiveLiquid),2).ToString() + "cL";
	}

	void OnDrawGizmos()
	{
		if (glassSpot.CurrentGlass == null) return;
		Gizmos.color = (liquids != null && liquids.Length > 0)
				? liquids[liquidIndex].color
				: Color.gray;
		Gizmos.DrawWireSphere(glassSpot.CurrentGlass.transform.position, 0.15f);
	}
}