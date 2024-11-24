using System.Collections.Generic;
using Godot;

namespace GodotProjects.code;

[Tool]
public partial class plantSelect : Node2D
{
	[Export] public int MAXcardNUm;
	/* public GridContainer leftCardCao; */
	public List<clickButton> waitPlantCards = new List<clickButton>();
	Vector2 oriPos;
	float xJianJu;
	float yJianJu;
	Vector2I hangLieNum = new Vector2I(1, 8);
	[Export]
	public Vector2I HangLieNum
	{
		get => hangLieNum;
		set
		{
			if (Engine.IsEditorHint())
			{
				Node2D yx = GetParent<Control>().GetChild<Node2D>(1);
				Vector2 Size = yx.GlobalPosition - GlobalPosition;
				xJianJu = Size.X / value.X;
				yJianJu = Size.Y / value.Y;
				oriPos = GlobalPosition + /* new Vector2(xJianJu, yJianJu) / 2; */new Vector2(0, 0);
				int i = 0;
				foreach (Node2D item in GetChildren())
				{
					item.GlobalPosition = oriPos + new Vector2(xJianJu * (i % value.X), yJianJu * (i / value.X));
					i++;
				}
				hangLieNum = value;
			}
		}
	}
	public bool hasSort;
	[Export]
	public bool HasSort
	{
		get => hasSort;
		set
		{
			hasSort = value;
		}
	}
	public void sortCardInList(int selfIdx, Vector2 pos, Node2D node2D)
	{
		Vector2 zanCun = new Vector2(0, 0);
		for (int i = selfIdx + 1; i < waitPlantCards.Count; i++)
		{
			Node2D up = waitPlantCards[i].GetParent<Node2D>();
			if (!waitPlantCards[i - 1].isInList)
			{
				GD.Print("if1");
				zanCun = up.GlobalPosition;
				up.RemoveChild(waitPlantCards[i]);
				node2D.AddChild(waitPlantCards[i]);
				waitPlantCards[i].GlobalPosition = pos - new Vector2(waitPlantCards[i].UIpianYI.X, waitPlantCards[i].UIpianYI.Y);
			}
			else
			{
				GD.Print("else1");
				up.RemoveChild(waitPlantCards[i]);
				GetChild<Node2D>(i - 1).AddChild(waitPlantCards[i]);
				waitPlantCards[i].GlobalPosition = zanCun - new Vector2(waitPlantCards[i].UIpianYI.X, waitPlantCards[i].UIpianYI.Y);
				zanCun = up.GlobalPosition;
			}
		}
	}
}