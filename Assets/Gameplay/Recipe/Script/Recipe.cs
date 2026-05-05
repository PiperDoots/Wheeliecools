using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Drink/Recipe")]
public class Recipe : ScriptableObject
{
	// Put some parts into your glass to make the thing they want!

	[System.Serializable]
	public class Ingredient
	{
		public Liquid liquid;

		[Range(0f, 1f)]
		[Tooltip("Proportion of this liquid in the recipe. All ingredients should sum to 1.")]
		public float ratio = 0.5f;
	}

	public string recipeName = "New Recipe";
	public Ingredient[] ingredients;
}