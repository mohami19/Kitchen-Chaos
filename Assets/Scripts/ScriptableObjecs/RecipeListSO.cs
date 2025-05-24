using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(menuName = "ScriptableObjects/RecipeListSO")] only need one in the project
public class RecipeListSO : ScriptableObject
{
    public List<RecipeSO> recipeSOList;
}
