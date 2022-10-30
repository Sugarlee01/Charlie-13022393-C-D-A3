using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDetector : MonoBehaviour
{
	[SerializeField]
	private	TowerSpawner	towerSpawner;
	[SerializeField]
	private	TowerDataViewer	towerDataViewer;

	private	Camera			mainCamera;
	private	Ray				ray;
	private	RaycastHit		hit;
	private	Transform		hitTransform = null;
	private	Transform		previousHitTransform = null;

	private void Awake()
	{
		mainCamera = Camera.main;
	}

	private void Update()
	{
		if ( EventSystem.current.IsPointerOverGameObject() == true )
		{
			return;
		}

		if ( towerSpawner.IsOnTowerButton )
		{
			ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			
			if ( Physics.Raycast(ray, out hit, Mathf.Infinity) )
			{
				if ( hit.transform.CompareTag("Tile") )
				{
					if ( previousHitTransform == hit.transform )
					{
						hit.transform.GetComponent<Tile>().OnSelectedTile();
					}
					else
					{
						OnChangePreviousTileColor();
					}

					previousHitTransform = hit.transform;
				}
			}
			else
			{
				OnChangePreviousTileColor();
			}
		}
		else
		{
			OnChangePreviousTileColor();
		}

		if ( Input.GetMouseButtonDown(0) )
		{
			ray = mainCamera.ScreenPointToRay(Input.mousePosition);

			if ( Physics.Raycast(ray, out hit, Mathf.Infinity) )
			{
				hitTransform = hit.transform;

				if ( hit.transform.CompareTag("Tile") )
				{
					towerSpawner.SpawnTower(hit.transform);
					hit.transform.GetComponent<Tile>().OnColorReset();
				}
				else if ( hit.transform.CompareTag("Tower") )
				{
					towerDataViewer.OnPanel(hit.transform);
				}
			}
		}
		else if ( Input.GetMouseButtonUp(0) )
		{
			if ( hitTransform == null || hitTransform.CompareTag("Tower") == false )
			{
				towerDataViewer.OffPanel();
			}

			hitTransform = null;
		}
	}

	private void OnChangePreviousTileColor()
	{
		if ( previousHitTransform != null )
		{
			previousHitTransform.GetComponent<Tile>().OnColorReset();
		}
	}
}