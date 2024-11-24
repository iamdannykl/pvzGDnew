using System;
using System.Collections.Generic;
using Godot;

namespace GodotProjects.code;

public partial class GridSys : Node2D
{
	public static GridSys Instance;
	public int hangShu;
	public List<hang> hangList = new List<hang>();
	public List<int> selectedSY = new List<int>();
	public List<int> daiXuan = new List<int>();
	public List<HangType> hangTypes = new List<HangType>();
	public int thisMapIndex;
	public reciever reciever;
	[Export]
	public Node2D zuoxia, youshang;
	public float XjianGe, YjianGe;
	public GridS nowGrd;
	public Vector2 nowPos;
	public bool isOut = true;
	private PackedScene packedScene;
	Vector2 realZX;
	Vector2 distance;
	Vector2 gridPoint;
	int countGD = 0;
	public List<GridS> gridList = new List<GridS>();
	public List<hang> grass = new List<hang>();
	public List<hang> water = new List<hang>();
	public List<hang> roof = new List<hang>();
	void grassPaiXu()
	{

	}
	public void LuanXuHang()
	{
		Random random = new Random();
		HangType currentType, slctType;
		for (int i = 0; i < hangList.Count; i++)
		{
			currentType = hangList[i].hangType;
			for (int j = 0; j < hangList.Count; j++)
			{
				slctType = hangList[j].hangType;
				if (selectedSY.Contains(j) || currentType != slctType)//只有同类型的行才能被随机调换
				{
					continue;
				}
				else
				{
					daiXuan.Add(j);
				}
			}
			GD.Print("random.Next(daiXuan.Count):" + random.Next(daiXuan.Count));
			GD.Print("daiXuanCount:" + daiXuan.Count);
			//GD.Print("zomIF:" + hangList[i].zomInfos.Count);
			int index = daiXuan[random.Next(daiXuan.Count)];
			//01234
			//20143
			daiXuan.Clear();
			selectedSY.Add(index);
		}
		for (int i = 0; i < selectedSY.Count; i++)//随机后重新给每一行分配僵尸
		{
			GD.Print("index:" + selectedSY[i]);
			hangList[i].NewZomInfos = new List<zomInfo>(hangList[selectedSY[i]].zomInfos);
			hangList[selectedSY[i]].zomInfos.Clear();
		}
		for (int i = 0; i < hangList.Count; i++)
		{
			foreach (zomInfo zif in hangList[i].NewZomInfos)
			{
				Zombie_base zom = resPlantAndZom.Instance.matchZom(zif.zomType).Instantiate<Zombie_base>();
				zif.hangShu = i + 1;
				zom.hangNum = zif.hangShu;
				zom.GlobalPosition = posByHang(zif) + new Vector2(XjianGe * 10, 0);
				zom.crtIt();
				GetTree().CurrentScene.AddChild(zom);
			}
		}
		GD.Print("finishLX");
		selectedSY.Clear();
		daiXuan.Clear();
	}
	Vector2 posByHang(zomInfo zom)
	{
		return new Vector2(zom.pos.X, hangList[zom.hangShu - 1].hangTou.Y);
	}
	public override void _Ready()
	{
		Instance = this;
		packedScene = GD.Load<PackedScene>("res://Prefabs/GDlogo.tscn");
		reciever = GetTree().Root.GetNode<reciever>("GameScene/reciever");
		thisMapIndex = reciever.RcmapIndex;
		hangShu = reciever.RcHangShu;
		XjianGe = (youshang.GlobalPosition.X - zuoxia.GlobalPosition.X) / 8f;
		YjianGe = -(youshang.GlobalPosition.Y - zuoxia.GlobalPosition.Y) / (hangShu - 1);
		GD.Print("XJG:" + XjianGe * 9);
		initHang();
		createHang();
		CreateGridBaseGrid();
		realZX = zuoxia.GlobalPosition - new Vector2(XjianGe / 2, -YjianGe / 2);
	}
	void initHang()
	{
		if (reciever.thisGuanQiaType == guanQiaType.grassDay || reciever.thisGuanQiaType == guanQiaType.grassNight)
		{
			for (int i = 0; i < hangShu; i++)
			{
				hangTypes.Add(HangType.grass);
			}
		}
		if (reciever.thisGuanQiaType == guanQiaType.poolDay || reciever.thisGuanQiaType == guanQiaType.poolNight)
		{
			hangTypes.Add(HangType.grass);
			hangTypes.Add(HangType.grass);
			hangTypes.Add(HangType.water);
			hangTypes.Add(HangType.water);
			hangTypes.Add(HangType.grass);
			hangTypes.Add(HangType.grass);
		}
		if (reciever.thisGuanQiaType == guanQiaType.roof)
		{
			for (int i = 0; i < hangShu; i++)
			{
				hangTypes.Add(HangType.roof);
			}
		}
	}
	void createHang()
	{
		for (int i = 0; i < hangShu; i++)
		{
			hangList.Add(new hang(i + 1, new Vector2(zuoxia.GlobalPosition.X - XjianGe / 2, zuoxia.GlobalPosition.Y - i * YjianGe), hangTypes[i]));
		}
	}
	public hang GetHangByMouse()//通过鼠标获取行
	{
		Vector2 clickPos = GetPosByMouse();
		distance = clickPos - realZX;
		gridPoint = new Vector2((int)(distance.X / XjianGe), (int)(-distance.Y / YjianGe));
		if (gridPoint.X > 8 || gridPoint.X < 0 || gridPoint.Y > (hangShu - 1) || gridPoint.Y < 0)
		{
			isOut = true;
			gridPoint = new Vector2(-1, -1);
			return null;
		}
		else
		{
			isOut = false;
		}
		if (gridPoint == new Vector2(-1, -1)) return null;
		else
		{
			return hangList[(int)gridPoint.Y];
		}
	}

	private void CreateGridBaseGrid()
	{
		int num = 0;
		for (int j = 0; j < hangShu; j++)
		{
			for (int i = 0; i < 9; i++)
			{
				gridList.Add(new GridS(new Vector2(i, j), zuoxia.GlobalPosition + new Vector2(XjianGe * i, -YjianGe * j), false, num, hangList[j].hangType));
				num++;
			}
		}
	}
	public Vector2 GetPosByMouse()
	{
		return GetGlobalMousePosition();
	}
	public GridS GetGridByPoint(Vector2 point)
	{
		if (point == new Vector2(-1, -1)) return null;
		else
		{
			nowGrd = gridList[(int)(point.X + 1 + point.Y * 9) - 1];
			//GD.Print(nowGrd.Plant);
			return gridList[(int)(point.X + 1 + point.Y * 9) - 1];
		}
	}
	public void GetPointByMouse()
	{
		Vector2 clickPos = GetPosByMouse();
		distance = clickPos - realZX;
		gridPoint = new Vector2((int)(distance.X / XjianGe), (int)(-distance.Y / YjianGe));
		//GD.Print(gridPoint, gridPoint.X > 8 || gridPoint.X < 0 || gridPoint.Y > 4 || gridPoint.Y < 0);
		if (gridPoint.X > 8 || gridPoint.X < 0 || gridPoint.Y > (hangShu - 1) || gridPoint.Y < 0)
		{
			isOut = true;
			//GD.Print("NULL" + isOut);
			nowPos = new Vector2(-1, -1);
		}
		else
		{
			isOut = false;
			//GD.Print((int)(gridPoint.X + 1 + gridPoint.Y * 9) - 1);
			nowPos = gridPoint;
		}
	}
	public GridS GetGridByMouse()
	{
		Vector2 clickPos = GetPosByMouse();
		distance = clickPos - realZX;
		gridPoint = new Vector2((int)(distance.X / XjianGe), (int)(-distance.Y / YjianGe));
		if (gridPoint.X > 8 || gridPoint.X < 0 || gridPoint.Y > (hangShu - 1) || gridPoint.Y < 0)
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
			return gridList[(int)(gridPoint.X + 1 + gridPoint.Y * 9) - 1];
		}
	}
}