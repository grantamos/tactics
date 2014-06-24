using UnityEngine;
using System.Collections;

public class Terrain : MonoBehaviour {

	public GameObject tile;

	public int width = 16;
	public int height = 16;

	public int iterations = 2;
	public int killAmount = 1;
	public int growAmount = 3;
	public int doubleGrowAmount = 5;

	private int[,] map;

	// Use this for initialization
	void Start () {
		GenerateMap ();
	}

	void GenerateMap() {

		for (int i = this.transform.childCount - 1; i >= 0; i--)
			GameObject.Destroy(this.transform.GetChild (i).gameObject);

		map = Automata.CreateHexMap (width, height, iterations, killAmount, growAmount, doubleGrowAmount);

		Vector3 position = new Vector3 (0, 0, 0);

		float radius = 0.5f;
		float hexHeight = 2 * radius;
		float rowHeight = 1.5f * radius;
		float halfWidth = Mathf.Sqrt((radius * radius) - (radius / 2) * (radius / 2));
		float hexWidth = 2 * halfWidth;
		float extraWidth = hexHeight - rowHeight;

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				float xPos = x * hexWidth + ((y & 1) * halfWidth);
				float yPos = y * rowHeight;

				if (x == width - 1 && y % 2 == 1)
					continue;

				position.x = xPos;
				position.y = map [x, y] * 0.5f;
				position.z = yPos;

				GameObject g = (GameObject) GameObject.Instantiate (tile, position, Quaternion.identity);
				g.transform.parent = this.transform;
			}
		}

		this.transform.position = new Vector3(-width / 2 * hexWidth, 0, -height / 2 * rowHeight);
	}

	void OnGUI()
	{
		if (GUI.Button (new Rect (10, 10, 100, 100), "Generate"))
		{
			this.GenerateMap();
		}
	}
}
