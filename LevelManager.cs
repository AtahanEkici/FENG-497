using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	private Vector3 pos;
	private Vector3 pos2;
	private int range;
	private GameObject go;
	private GameObject temp;

	public Transform player;
	[Header("Side Walls")]
	public GameObject wallsPrefab;
	public float currentWallY;
	public float wallTall = 11.5f;
	public float distanceBeforeSpawn = 10f;
	public int initialWalls = 3;
	public List<GameObject> wallPool;

	[Header("Platforms")]
	public GameObject blockPrefab; // normal Donut //
	public GameObject blockPrefab_pink; // pink donut //
	public GameObject blockPrefab_movable;  // moving chocolate //
	public float currentBlockY;  // distance between the ground and the current location //
	public float distanceBetweenBlocks = 5f;
	public float distanceBeforeSpawnBlock = 10f;
	public int initBlocksLine = 2;
	public List<GameObject> blocksPool;

	private void Awake()
	{
		InitSideWalls();
		InitBlocks();
	}
	private void Update()
	{
		if (currentWallY - player.position.y < distanceBeforeSpawn)
		{
			SpawnSideWall();
		}

		if (currentBlockY - player.position.y < distanceBeforeSpawnBlock)
		{
			SpawnBlocks();
		}
	}
	private void InitSideWalls()
	{
		for (int i = 0; i < initialWalls; ++i)
		{
			pos = new Vector3(0, currentWallY);
			go = Instantiate(wallsPrefab, pos, Quaternion.identity, transform);
			wallPool.Add(go);
			currentWallY += wallTall;
		}
	}
	private void InitFirst()
	{
		pos2 = new Vector3(Random.Range(-5, 5), currentBlockY);
		range = Random.Range(0, 100);
		go = Instantiate(blockPrefab, pos2, Quaternion.identity, transform);
		blocksPool.Add(go);
		currentBlockY += distanceBetweenBlocks;
	}
	private void InitBlocks()
	{
		InitFirst();

		for (int i = 0; i < initBlocksLine; i++)
		{
			pos2 = new Vector3(Random.Range(-5, 5), currentBlockY);
			range = Random.Range(0, 100);

			if (range <= 50)
			{
				go = Instantiate(blockPrefab, pos2, Quaternion.identity, transform);
				blocksPool.Add(go);
				currentBlockY += distanceBetweenBlocks;
			}
			else if (range > 50 && range < 90)
			{
				go = Instantiate(blockPrefab_pink, pos2, Quaternion.identity, transform);
				blocksPool.Add(go);
				currentBlockY += distanceBetweenBlocks;
			}
			else
			{
				pos2 = new Vector3(-5, currentBlockY);
				go = Instantiate(blockPrefab_movable, pos2, Quaternion.identity, transform);
				blocksPool.Add(go);
				currentBlockY += distanceBetweenBlocks;
			}
		}
	}
	private void SpawnSideWall()
	{
		wallPool[0].transform.position = new Vector3(0, currentWallY);
		currentWallY += wallTall;
		temp = wallPool[0];
		wallPool.RemoveAt(0);
		wallPool.Add(temp);
	}
	private void SpawnBlocks()
	{
		pos2 = new Vector3(Random.Range(-5, 5), currentBlockY);
		range = Random.Range(0, 100);

		if (range <= 55)
		{
			go = Instantiate(blockPrefab, pos2, Quaternion.identity, transform);
			blocksPool.Add(go);
			currentBlockY += distanceBetweenBlocks;
		}
		else if (range > 55 && range < 90)
		{
			go = Instantiate(blockPrefab_pink, pos2, Quaternion.identity, transform);
			blocksPool.Add(go);
			currentBlockY += distanceBetweenBlocks;
		}
		else
		{
			pos2 = new Vector3(-5, currentBlockY);
			go = Instantiate(blockPrefab_movable, pos2, Quaternion.identity, transform);
			blocksPool.Add(go);
			currentBlockY += distanceBetweenBlocks;
		}
		Destroy(blocksPool[0]);
		blocksPool.RemoveAt(0);
	}
}