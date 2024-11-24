using Godot;

namespace GodotProjects.code;

public partial class pea_bullet : bulletBase
{
    [Export] public AudioStreamPlayer audioStreamPlayer;
    [Export] public float freezeTime;
    public override void atkZom(Area2D zom)
    {
        base.atkZom(zom);
        audioStreamPlayer.Play();
        if (isAnimPlayer)
        {
            animPlayer.Play("explode");
            canMove = false;
        }
        else
        {
            if (!noExplode)
            {
                canMove = false;
                anim.Play("explode");
            }
        }
        if (zombieB.Hp > 0)
        {
            if (btp == BulletType.snowPea)
            {
                if (!zombieB.isFrozen)
                {
                    baseCard.playFreeze();
                }
                Timer timer = zombieB.GetNode<Timer>("freezeTime");
                timer.WaitTime = freezeTime;
                zombieB.GetNode<Sprite2D>("Sprite2D").Modulate = new Color("25a2e3");
                zombieB.currrentSpd = zombieB.spd * 0.5f;
                zombieB.isFrozen = true;
                timer.Start();
            }
            zombieB.Hp -= atkValue;
        }
    }
}