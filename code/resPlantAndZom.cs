using System.Collections.Generic;
using Godot;

namespace GodotProjects.code;

public enum PlantType
{
    peaShooter,
    rePeater,
    sunFlower,
    potatoDL,
    cherryBaoDan,
    snowPeaShooter,
    nut,
    heYe,
    daZuiHua,
    xiaoPenGu,
    daPenGu,
    sunShroom
}
public enum ZomType
{
    zombie,
    luZhang
}
public enum BulletType
{
    pea,
    snowPea,
    xpgBlt,
    dpgBlt
}
public partial class resPlantAndZom : Node2D
{
    public Dictionary<PlantType, PackedScene> plantTypeToPackedScene;
    public Dictionary<BulletType, PackedScene> bulletTypeToPackedScene;
    public static resPlantAndZom Instance;
    [Export] public PackedScene peaShooter;
    [Export] public PackedScene rePeater;
    [Export] public PackedScene sunFlower;
    [Export] public PackedScene potatoDL;
    [Export] public PackedScene nut;
    [Export] public PackedScene cherryBaoDan;
    [Export] public PackedScene snowPeaShooter;
    [Export] public PackedScene heYe;
    [Export] public PackedScene daZuiHua;
    [Export] public PackedScene xiaoPenGu;
    [Export] public PackedScene daPenGu;
    [Export] public PackedScene sunShroom;
    //Zombies===============================================================
    [Export] public PackedScene zombie;
    [Export] public PackedScene luZhang;
    //Bullets===============================================================
    [Export] public PackedScene pea;
    [Export] public PackedScene snowPea;
    [Export] public PackedScene xpgBlt;
    [Export] public PackedScene dpgBlt;
    public override void _Ready()
    {
        Instance = this;
        plantTypeToPackedScene = new Dictionary<PlantType, PackedScene>{
            {PlantType.peaShooter,peaShooter},
            {PlantType.sunFlower,sunFlower},
            {PlantType.potatoDL,potatoDL},
            {PlantType.cherryBaoDan,cherryBaoDan},
            {PlantType.snowPeaShooter,snowPeaShooter},
            {PlantType.nut,nut},
            {PlantType.rePeater,rePeater},
            {PlantType.heYe,heYe},
            {PlantType.daZuiHua,daZuiHua},
            {PlantType.xiaoPenGu,xiaoPenGu},
            {PlantType.daPenGu,daPenGu},
            {PlantType.sunShroom,sunShroom}
        };
        bulletTypeToPackedScene = new Dictionary<BulletType, PackedScene>{
            {BulletType.pea,pea},
            {BulletType.snowPea,snowPea},
            {BulletType.xpgBlt,xpgBlt},
            {BulletType.dpgBlt,dpgBlt}
        };
    }
    public PackedScene matchPlant(PlantType plantType)
    {
        if (plantTypeToPackedScene.TryGetValue(plantType, out PackedScene scene))
        {
            return scene;
        }
        return null;
    }

    public PackedScene matchZom(ZomType zomType)
    {
        switch (zomType)
        {
            case ZomType.zombie:
                return zombie;
            case ZomType.luZhang:
                return luZhang;
            default:
                return null;
        }
    }
    public PackedScene matchBullet(BulletType bulletType)
    {
        if (bulletTypeToPackedScene.TryGetValue(bulletType, out PackedScene scene))
        {
            return scene;
        }
        return null;
    }
}