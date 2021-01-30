public class PeriodOnOff : OnOff
{
    public float FlipEverySecond = 1;
    
    // Update is called once per frame
    void Update()
    {
        if (ActionEx.CheckCooldown(Flip, FlipEverySecond))
            Flip();
    }
}
