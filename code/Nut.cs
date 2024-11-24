using System;
using Godot;
using GodotProjects.anim.plant.nut;

namespace GodotProjects.code;

public partial class Nut : baseCard
{
    [Export] private float spd;
    private float ySpd;
    private int randomValue;
    bool isXie;
    float basePosX;
    [Export] AudioStreamPlayer rolling, zhuangJi, zhuangJi2;
    Random random = new Random();

    public override void _Process(double delta)
    {
        if (isRoll && isplanted)
        {
            GlobalPosition += new Vector2((float)(spd * delta), (float)(ySpd * delta));
        }
        if (GlobalPosition.X - basePosX > 300)
        {
            QueueFree();
        }
    }
    public override void _Ready()
    {
        base._Ready();
        rolling = GetNode<AudioStreamPlayer>("rolling");
        zhuangJi = GetNode<AudioStreamPlayer>("duang");
        zhuangJi2 = GetNode<AudioStreamPlayer>("duang2");
    }
    public override void placed(bool isWaterPlant)
    {
        base.placed(isWaterPlant);
        basePosX = GlobalPosition.X;
        if (reciever.Instance.modePublic == modeInGame.nutRoll)
        {
            isRoll = true;
            GetNode<stateMachineNut>("AnimationTree").isRoll = true;
            rolling.Play();
        }
        else
        {
            isRoll = false;
            GetNode<stateMachineNut>("AnimationTree").isRoll = false;
        }
    }
    public void collideWithZom(Area2D zom)
    {
        if (reciever.Instance.modePublic != modeInGame.nutRoll) return;
        if (!isplanted) return;
        IbeKnocked inkd = zom.GetParent<IbeKnocked>();
        if (inkd != null)
        {
            inkd.Knocked();
        }
        randomValue = random.Next(0, 2);
        if (zom.CollisionLayer == 2)
        {
            if (!isXie)
            {
                isXie = true;
                if (randomValue == 0)
                {
                    ySpd = spd;
                }
                else if (randomValue == 1)
                {
                    ySpd = -spd;
                }
                else
                {
                    ySpd = 0;
                }
            }
            else
            {
                ySpd = -ySpd;
            }
            int rdm = random.Next(0, 2);
            if (rdm == 0)
            {
                zhuangJi.Play();
            }
            else
            {
                zhuangJi2.Play();
            }
        }
        else if (zom.CollisionLayer == 32)
        {
            ySpd = -ySpd;
        }
    }
}