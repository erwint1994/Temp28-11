using paSSQL;
using pasLogger;
using System;

namespace pasTemp
{
    public static class TempMgrAxis
    {
        // sensor 1
        public static float YMinTempC1(DateTime AFromC1, DateTime AToC1)
        {
            float LResult = 0;
            try
            {               
                LResult = SQL.GetSQLDecimal("SELECT MIN(TemperatureCelsius)-2 FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + AFromC1.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + AToC1.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=1;");
            }
            catch
            { 
                Logger.Log(LogType.ltError, "YMaxTempCelsius1");
            }
            return LResult;
        }
        public static float YMaxTempC1(DateTime AFromC1, DateTime AToC1)
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("SELECT MAX(TemperatureCelsius)+2 FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + AFromC1.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + AToC1.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=1;");
            }
            catch
            {
                Logger.Log(LogType.ltError, "YMinTempCelsius1");
            }
            return LResult;
        }
        public static float YMinTempK1(DateTime AFromK1, DateTime AToK1)
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("SELECT MIN(TemperatureKelvin)-2 FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + AFromK1.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + AToK1.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=1;");
            }
            catch
            {
                Logger.Log(LogType.ltError, "YMinTempKelvin1");
            }
            return LResult;
        }
        public static float YMaxTempK1(DateTime AFromK1, DateTime AToK1)
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("SELECT MAX(TemperatureKelvin)+2 FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + AFromK1.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + AToK1.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=1;");
            }
            catch
            {
                Logger.Log(LogType.ltError, "YMaxTempKelvin1");
            }
            return LResult;
        }
        public static float YMinTempF1(DateTime AFromF1, DateTime AToF1)
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("SELECT MIN(TemperatureFarhenheid)-2 FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + AFromF1.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + AToF1.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=1;");
            }
            catch
            {
                Logger.Log(LogType.ltError, "YMinTempF1");
            }
            return LResult;
        }
        public static float YMaxTempF1(DateTime AFromF1, DateTime AToF1)
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("SELECT MAX(TemperatureFarhenheid)+2 FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + AFromF1.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + AToF1.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=1;");
            }
            catch
            {
                Logger.Log(LogType.ltError, "YMaxTempF1");
            }
            return LResult;
        }
        // sensor 2
        public static float YMinTempC2(DateTime AFromC2, DateTime AToC2)
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("SELECT MIN(TemperatureCelsius)-2 FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + AFromC2.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + AToC2.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=2;");
            }
            catch
            {
                Logger.Log(LogType.ltError, "YMinTempCelsius2");
            }
            return LResult;
        }
        public static float YMaxTempC2(DateTime AFromC2, DateTime AToC2)
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("SELECT MAX(TemperatureCelsius)+2 FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + AFromC2.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + AToC2.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=2;");
            }
            catch
            {
                Logger.Log(LogType.ltError, "YMinTempCelsius2");
            }
            return LResult;
        }
        public static float YMinTempK2(DateTime AFromK2, DateTime AToK2)
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("SELECT MIN(TemperatureKelvin)-2 FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + AFromK2.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + AToK2.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=2;");
            }
            catch
            {
                Logger.Log(LogType.ltError, "YMinTempKelvin2");
            }
            return LResult;
        }
        public static float YMaxTempK2(DateTime AFromK2, DateTime AToK2)
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("SELECT MAX(TemperatureKelvin)+2 FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + AFromK2.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + AToK2.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=2;");
            }
            catch
            {
                Logger.Log(LogType.ltError, "YMaxTempKelvin2");
            }
            return LResult;
        }
        public static float YMinTempF2(DateTime AFromF2, DateTime AToF2)
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("SELECT MIN(TemperatureFarhenheid)-2 FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + AFromF2.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + AToF2.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=2;");
            }
            catch
            {
                Logger.Log(LogType.ltError, "YMinTempF2");
            }
            return LResult;
        }
        public static float YMaxTempF2(DateTime AFromF2, DateTime AToF2)
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("SELECT MAX(TemperatureFarhenheid)+2 FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + AFromF2.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + AToF2.ToString("MM/dd/yyyy HH:mm:ss") + "') AND Location_Id=2;");
            }
            catch
            {
                Logger.Log(LogType.ltError, "YMaxTempF2");
            }
            return LResult;
        }
        // sensor 1 en 2
        public static float YMinTempCAll(DateTime AFromCAll, DateTime AToCAll)
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("SELECT MIN(TemperatureCelsius)-2 FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + AFromCAll.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + AToCAll.ToString("MM/dd/yyyy HH:mm:ss") + "')");
            }
            catch
            {
                Logger.Log(LogType.ltError, "YMinTempCelsiusAll");
            }
            return LResult;
        }
        public static float YMaxTempCAll(DateTime AFromCAll, DateTime AToCAll)
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("SELECT MAX(TemperatureCelsius)+2 FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + AFromCAll.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + AToCAll.ToString("MM/dd/yyyy HH:mm:ss") + "')");
            }
            catch
            {
                Logger.Log(LogType.ltError, "YMaxTempCelsiusAll");
            }
            return LResult;
        }
        public static float YMaxTempKAll(DateTime AFromKAll, DateTime AToKAll)
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("SELECT MAX(TemperatureKelvin)+2 FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + AFromKAll.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + AToKAll.ToString("MM/dd/yyyy HH:mm:ss") + "')");
            }
            catch
            {
                Logger.Log(LogType.ltError, "YMaxTempKelvinAll");
            }
            return LResult;
        }
        public static float YMinTempKAll(DateTime AFromKAll, DateTime AToKAll)
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("SELECT MIN(TemperatureKelvin)-2 FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + AFromKAll.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + AToKAll.ToString("MM/dd/yyyy HH:mm:ss") + "')");
            }
            catch
            {
                Logger.Log(LogType.ltError, "YMaxTempKelvinAll");
            }
            return LResult;
        }
        public static float YMinTempFAll(DateTime AFromFAll, DateTime AToFAll)
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("SELECT MIN(TemperatureFarhenheid)-2 FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + AFromFAll.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + AToFAll.ToString("MM/dd/yyyy HH:mm:ss") + "')");
            }
            catch
            {
                Logger.Log(LogType.ltError, "YMinTempFarhenheidAll");
            }
            return LResult;
        }
        public static float YMaxTempFAll(DateTime AFromFAll, DateTime AToFAll)
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("SELECT MAX(TemperatureFarhenheid)+2 FROM dbo.tbl_Temperature WHERE (DateTime) BETWEEN ('" + AFromFAll.ToString("MM/dd/yyyy HH:mm:ss") + "') AND ('" + AToFAll.ToString("MM/dd/yyyy HH:mm:ss") + "')");
            }
            catch
            {
                Logger.Log(LogType.ltError, "YMaxTempFarhenheidAll");
            }
            return LResult;
        }
    }
}
