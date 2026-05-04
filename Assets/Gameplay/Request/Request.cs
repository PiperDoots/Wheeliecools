using UnityEngine;

[CreateAssetMenu(fileName = "NewRequest", menuName = "Request")]
public class Request : ScriptableObject
{
	// I will be able to randomly generate these trust 

	public Recipe recipe;

	[Range(0f, 1f)]
	[Tooltip("How full the glass should be (0–1 of its total capacity)")]
	public float targetFill = 0.75f;

	// Glass
	// Additional?
}