using System.Collections.Generic;
using Godot;

namespace GodotProjects.code;

public class GridS
{
    public Vector2 Point;
    //世界坐标
    public Vector2 Position;
    //是否有植物
    //public bool Plant;
    public List<baseCard> plantsOnThisGrid = new List<baseCard>();
    public bool isHeYe;
    public bool isPlantOnHeYe;
    public bool hasNanGuaTou;
    public bool hasCoffeeBean;
    public HangType gtp;
    public bool Zombie;
    public int Num;
    public GridS(Vector2 point, Vector2 position, bool zombie, int num, HangType gridType)
    {
        Point = point;
        Position = position;
        Zombie = zombie;
        Num = num;
        gtp = gridType;
    }
}