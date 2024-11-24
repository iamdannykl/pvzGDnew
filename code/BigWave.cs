using System.Collections.Generic;

namespace GodotProjects.code;

public class BigWave
{
    public wave bigWave;
    public float location;
    public List<wave> miniWaves = new List<wave>();
    public BigWave(wave inWave)
    {
        bigWave = inWave;
    }
}