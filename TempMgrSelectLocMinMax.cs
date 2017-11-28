using paSSQL;
using pasLogger;
using System;

namespace pasTemp
{
    public static class TempMgrSelectLocMinMax
    {
        public static float Test1(/*DateTime AFromC1, DateTime AToC1*/)
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("SELECT Location, MinimumTemperatureFarhenheid, MaximumTemperatureFarhenheid FROM tbl_Location WHERE id=2");
            }
            catch (Exception)
            {               
                Logger.Log(LogType.ltError, "Test1");
            }
            return LResult;
        }
    }
}
