using UnityEngine;
using UnityEngine.InputSystem;

public class RecipeChecker : MonoBehaviour
{

	// Basically just prints a score check, not amazing yet like why would I get a score for a glass of the wrong thing but we ball

	public GlassFiller filler;
	public Request[] requests;

	public Key checkKey = Key.Enter;

	void Update()
	{
		if (Keyboard.current[checkKey].wasPressedThisFrame) CheckSelectedGlass();
	}

	public void CheckSelectedGlass()
	{
		Glass glass = filler?.SelectedGlass;
		if (glass == null) 
		{ 
			Debug.Log("No glass selected."); 
			return; 
		}

		Debug.Log($"Glass contents (fill: {glass.FillAmount * 100f:0.0}%)");
		foreach (var kvp in glass.LiquidAmounts)
		{
			Debug.Log($"  {kvp.Key.liquidName}: {kvp.Value * 100f:0.0}%");
		}

		if (requests == null || requests.Length == 0) return;

		foreach (Request request in requests)
		{
			if (request == null || request.recipe == null) continue;
			Glass.RecipeScore score = glass.CheckRecipe(request);
			LogScore(request, score);
		}
	}

	private void LogScore(Request request, Glass.RecipeScore score)
	{
		Debug.Log($" --- {request.recipe.recipeName} (target fill: {request.targetFill * 100f:0.0}%) --- " +
				  $"Total: {score.total:0.0}/100  " +
				  $"(Ingredients: {score.ingredientScore:0.0}  " +
				  $"Purity: {score.purityScore:0.0}  " +
				  $"Fill: {score.fillScore:0.0})");

		foreach (var ing in score.ingredients)
		{
			string bar = ing.score >= 100f ? "v" : $"{ing.score:0.0}%";
			Debug.Log($"  {bar}  {ing.liquidName}: " + $"{ing.actualAmount * 100f:0.0}% in glass, " + $"target {ing.targetAmount * 100f:0.0}%");
		}
	}
}