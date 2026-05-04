using UnityEngine;
using UnityEngine.InputSystem;

public class GlassFiller : MonoBehaviour
{
	// Fill up glasses with a liquid, currently very very manual with Keys

	public Glass[] glasses;

	[Header("Active Selection")]
	public int selectedIndex = 0;

	//	public Color[] liquids = new Color[]
	//	{
	//		Color.red,
	//		Color.blue,
	//		Color.green,
	//		Color.yellow,
	//		new Color(0.6f, 0f, 1f)
	//	};

	public Liquid[] liquids = new Liquid[0];

	private int liquidIndex = 0;

	[Header("Keybindings")]
	public Key fillKey = Key.Space;
	public Key nextGlass = Key.RightArrow;
	public Key prevGlass = Key.LeftArrow;
	public Key nextColour = Key.E;
	public Key prevColour = Key.Q;
	public Key resetGlass = Key.R;
	public Glass SelectedGlass => (glasses != null && glasses.Length > 0) ? glasses[selectedIndex] : null;
	public Color ActiveColour => (liquids != null && liquids.Length > 0) ? liquids[liquidIndex].color : Color.white;
	public Liquid ActiveLiquid => (liquids != null && liquids.Length > 0) ? liquids[liquidIndex] : null;

	void Update()
	{
		if (glasses == null || glasses.Length == 0) return;

		HandleGlassSelection();
		HandleColourSelection();
		HandleFilling();
		HandleReset();
	}
	private void HandleGlassSelection()
	{
		if (Keyboard.current[nextGlass].wasPressedThisFrame)
		{
			selectedIndex = (selectedIndex + 1) % glasses.Length;
		}
		if (Keyboard.current[prevGlass].wasPressedThisFrame)
		{
			selectedIndex = (selectedIndex - 1 + glasses.Length) % glasses.Length;
		}

		selectedIndex = Mathf.Clamp(selectedIndex, 0, glasses.Length - 1);
	}

	private void HandleColourSelection()
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

		target.AddFill(target.FillDelta, ActiveLiquid);
	}

	private void HandleReset()
	{
		if (Keyboard.current[resetGlass].wasPressedThisFrame)
			SelectedGlass?.ResetGlass();
	}
	void OnDrawGizmos()
	{
		if (glasses == null) return;
		for (int i = 0; i < glasses.Length; i++)
		{
			if (glasses[i] == null) continue;
			Gizmos.color = (i == selectedIndex && liquids != null && liquids.Length > 0) ? liquids[liquidIndex].color : Color.gray;
			Gizmos.DrawWireSphere(glasses[i].transform.position, 0.15f);
		}
	}
}