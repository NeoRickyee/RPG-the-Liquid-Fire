using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LevelData : ScriptableObject 
{
	// stores x, height, y of Tile(s)
	// not actually location vector of a GameObject
	public List<Vector3> tiles;
}