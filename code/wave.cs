using System.Collections.Generic;

namespace GodotProjects.code;

public class wave
{
    public List<zr> zrs = new List<zr>();
    public bool isBigWave;
    public float location;
    public wave()
    {
        zrs.Add(new zr());
        zrs.Add(new zr());
        zrs.Add(new zr());
    }
}