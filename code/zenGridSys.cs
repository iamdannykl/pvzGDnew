using System.Collections.Generic;
using Godot;

namespace GodotProjects.code;

public partial class zenGridSys : Node2D
{
	public static zenGridSys Instance;
	[Export] public int hangShu;
	public List<hang> hangList = new List<hang>();
	public List<int> selectedSY = new List<int>();
	public List<int> daiXuan = new List<int>();
	public List<HangType> hangTypes = new List<HangType>();
	public int thisMapIndex;
	[Export]
	public Node2D zuoxia, youshang;
	public float XjianGe, YjianGe;
	public zenGrid nowGrd;
	public Vector2 nowPos;
	public bool isOut = true;
	private PackedScene packedScene;
	Vector2 realZX;
	Vector2 distance;
	Vector2 gridPoint;
	int countGD = 0;
	public List<zenGrid> gridList = new List<zenGrid>();
	public List<hang> grass = new List<hang>();
	public List<hang> water = new List<hang>();
	public List<hang> roof = new List<hang>();
	void grassPaiXu()
	{

	}
	public override void _Ready()
	{
		Instance = this;
		XjianGe = (youshang.GlobalPosition.X - zuoxia.GlobalPosition.X) / 7f;
		YjianGe = -(youshang.GlobalPosition.Y - zuoxia.GlobalPosition.Y) / (hangShu - 1);
		CreateGridBaseGrid();
		realZX = zuoxia.GlobalPosition - new Vector2(XjianGe / 2, -YjianGe / 2);
	}
	public override void _Process(double delta)
	{
		nowGrd = GetGridByMouse();
		if (nowGrd != null)
		{
			GD.Print(nowGrd.Point);
		}
	}
	private void CreateGridBaseGrid()
	{
		int num = 0;
		for (int j = 0; j < hangShu; j++)
		{
			for (int i = 0; i < 8; i++)
			{
				gridList.Add(new zenGrid(new Vector2(i, j), zuoxia.GlobalPosition + new Vector2(XjianGe * i, -YjianGe * j), false));
				num++;
			}
		}
		GD.Print(num);
	}
	public Vector2 GetPosByMouse()
	{
		return GetGlobalMousePosition();
	}
	public zenGrid GetGridByPoint(Vector2 point)
	{
		if (point == new Vector2(-1, -1)) return null;
		else
		{
			nowGrd = gridList[(int)(point.X + 1 + point.Y * 8) - 1];
			//GD.Print(nowGrd.Plant);
			return gridList[(int)(point.X + 1 + point.Y * 8) - 1];
		}
	}
	public void GetPointByMouse()
	{
		Vector2 clickPos = GetPosByMouse();
		distance = clickPos - realZX;
		gridPoint = new Vector2((int)(distance.X / XjianGe), (int)(-distance.Y / YjianGe));
		//GD.Print(gridPoint, gridPoint.X > 8 || gridPoint.X < 0 || gridPoint.Y > 4 || gridPoint.Y < 0);
		if (gridPoint.X > 7 || gridPoint.X < 0 || gridPoint.Y > (hangShu - 1) || gridPoint.Y < 0)
		{
			isOut = true;
			//GD.Print("NULL" + isOut);
			nowPos = new Vector2(-1, -1);
		}
		else
		{
			isOut = false;
			nowPos = gridPoint;
		}
	}
	public zenGrid GetGridByMouse()
	{
		Vector2 clickPos = GetPosByMouse();
		distance = clickPos - realZX;
		gridPoint = new Vector2((int)(distance.X / XjianGe), (int)(-distance.Y / YjianGe));
		if (gridPoint.X > 7 || gridPoint.X < 0 || gridPoint.Y > (hangShu - 1) || gridPoint.Y < 0)
		{
			isOut = true;
			nowPos = new Vector2(-1, -1);
			return null;
		}
		else
		{
			isOut = false;
		}
		if (gridPoint == new Vector2(-1, -1)) return null;
		else
		{
			//GD.Print((int)(gridPoint.X + gridPoint.Y * 8));
			return gridList[(int)(gridPoint.X + gridPoint.Y * 8)];
		}
	}
}