using Godot;

namespace GodotProjects.code;

public partial class bulletBase : Area2D
{
    public AnimatedSprite2D anim;
    [Export] public BulletType btp;
    [Export] public AnimationPlayer animPlayer;
    [Export] public bool isAnimPlayer;
    [Export] public float bltSpd;
    [Export] public bool noExplode;
    [Export] public int atkValue;
    public baseCard baseCard;
    public bool canMove = true;
    public Zombie_base zombieB;
    public override void _Ready()
    {
        if (!isAnimPlayer)
        {
            anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
            anim.Connect("animation_finished", new Callable(this, nameof(desSelf)));
        }
        ZIndex = 4;
    }
    public void desSelf()
    {
        if (noExplode)
        {
            if (anim.Animation == "idle")
            {
                canMove = false;
                realDes();
            }
            return;
        }
        if (anim.Animation == "explode")
        {
            Visible = false;
            CollisionMask = 0;
            GetNode<Timer>("Timer").Start();
        }
    }
    public virtual void atkZom(Area2D zom)
    {
        if (!canMove) return;
        zombieB = zom.Owner as Zombie_base;
        zombieB.beAttacked();
    }
    void realDes()
    {
        QueueFree();
    }
    public override void _PhysicsProcess(double delta)
    {
        if (canMove)
        {
            GlobalPosition += new Vector2(bltSpd, 0);
        }
        else
        {
            GlobalPosition += new Vector2(0, bltSpd * 0.25f);
        }
    }
}