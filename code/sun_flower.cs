namespace GodotProjects.code;

public partial class sun_flower : baseCard
{
    public override void placed(bool isWaterPlant)
    {
        base.placed(false);
        //GetNode<sunCreator>("sunCreator").timer.Start();
        GetNode<sunCreator>("sunCreator").first.Start();
    }
}