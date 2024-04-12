using Godot;
using System;

public partial class Map : Node3D
{
	private GridMap gridMap;
	private int mapSize = 50;
	private int wantedRooms = 6;
	public override void _Ready()
	{
		gridMap = GetNode<GridMap>("GridMap");
		GenerateMap();
	}
	public override void _Process(double delta)
	{
	}
	public void GenerateMap()
	{
		ClearGridMap();
		SpawnAllBaseRooms();
		gridMap.SetCellItem(new Vector3I(0, 0, 0), 2, 0);
	}
	private void SpawnAllBaseRooms()
	{
        for (int row = mapSize * -1; row < mapSize; row++) {
            for (int col = mapSize * -1; col < mapSize; col++) {
				TrySpawningRoom(new Vector3I(row, 0, col));
            }
        }
    }
	private void TrySpawningRoom(Vector3I loc)
	{
        Random random = new Random();
        int randomInt = random.Next(2);
        bool randomBool = randomInt == 1;
        if (randomBool && NoNborRooms(loc)) {
            gridMap.SetCellItem(loc, 2, 0);
        }
    }
	private bool NoNborRooms(Vector3I loc)
	{
        for (int row = loc.X - 1; row < loc.X + 1; row++) {
            for (int col = loc.Y - 1; col < loc.Y + 1; col++) {
				if (gridMap.GetCellItem(new Vector3I(row, 0, col)) != -1 && (row != loc.X && col != loc.Y)) {
					return false;
				}
            }
        }
        return true;
	}
	private void ClearGridMap()
	{
		for (int row = mapSize * -1; row < mapSize; row++) {
			for (int col = mapSize * -1; col < mapSize; col++) {
				gridMap.SetCellItem(new Vector3I(row, 0, col), -1, 0);
			}
		}
	}
}
