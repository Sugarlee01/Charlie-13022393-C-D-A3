using UnityEngine;

public class Tile : MonoBehaviour
{
	private	SpriteRenderer	spriteRenderer;

	public bool IsBuildTower { set; get; }

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		OnColorReset();

		IsBuildTower = false;
	}

	public void OnSelectedTile()
	{
		spriteRenderer.color = Color.blue;
	}

	public void OnColorReset()
	{
		spriteRenderer.color = new Color(0, 0.69f, 0.31f);
	}
}
