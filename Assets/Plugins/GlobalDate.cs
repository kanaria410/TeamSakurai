public static class GlobalDate
{
    static bool stopFlag = false;

    public static bool _STOP_FLAG
    {
        get { return stopFlag; }
        set { stopFlag = value; }
    }
}