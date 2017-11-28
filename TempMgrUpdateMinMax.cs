using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using paSSQL;
using pasLogger;

namespace pasTemp
{
    public class TempMgrUpdateMinMax
    {
        // celsius
        // min celsius naar kelvin
        public static float MinTempK()
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("UPDATE tbl_Location SET MinimumTemperatureKelvin = MinimumTemperatureCelsius + 273.15");
            }
            catch
            {
                Logger.Log(LogType.ltError, "MinTempK");
            }
            return LResult;
        }
        // max celsius naar kelvin
        public static float MaxTempK()
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("UPDATE tbl_Location SET MaximumTemperatureKelvin = MaximumTemperatureCelsius + 273.15");
            }
            catch
            {
                Logger.Log(LogType.ltError, "MaxTempK");
            }
            return LResult;
        }
        // min celsius naar farhenheid 
        public static float MinTempF()
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("UPDATE tbl_Location SET MinimumTemperatureFarhenheid = MinimumTemperatureCelsius * 1.8 + 32");
            }
            catch
            {
                Logger.Log(LogType.ltError, "MinTempF");
            }
            return LResult;
        }
        // max celsius naar farhenheid
        public static float MaxTempF()
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal(" UPDATE tbl_Location SET MaximumTemperatureFarhenheid = MaximumTemperatureCelsius * 1.8 + 32");
            }
            catch
            {
                Logger.Log(LogType.ltError, "MaxTempF");
            }
            return LResult;
        }

        // kelvin
        // min kelvin naar celsius
        public static float MinKToC()
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("UPDATE tbl_Location SET MinimumTemperatureCelsius = MinimumTemperatureKelvin - 273.15");
            }
            catch
            {
                Logger.Log(LogType.ltError, "MinKToC");
            }
            return LResult;
        }
        // max kelvin naar celsius
        public static float MaxKToC()
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("UPDATE tbl_Location SET MaximumTemperatureCelsius = MaximumTemperatureKelvin - 273.15");
            }
            catch
            {
                Logger.Log(LogType.ltError, "MaxKToC");
            }
            return LResult;
        }
        // min kelvin naar farhenheid
        public static float MinKToF()
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("UPDATE tbl_Location SET MinimumTemperatureFarhenheid = MinimumTemperatureKelvin * 1.8 - 459.67");
            }
            catch
            {
                Logger.Log(LogType.ltError, "MinKToF");
            }
            return LResult;
        }
        // max kelvin naar farhenheid
        public static float MaxKToF()
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("UPDATE tbl_Location SET MaximumTemperatureFarhenheid = MaximumTemperatureKelvin * 1.8 - 459.67");
            }
            catch
            {
                Logger.Log(LogType.ltError, "MaxKToF");
            }
            return LResult;
        }

        // farhenheid
        // min farhenheid naar celsius
        public static float MinFToC()
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("UPDATE tbl_Location SET MinimumTemperatureCelsius = (MinimumTemperatureFarhenheid -32) / 1.8");
            }
            catch
            {
                Logger.Log(LogType.ltError, "MinFToC");
            }
            return LResult;
        }
        // max farhenheid naar celsius
        public static float MaxFToC()
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("UPDATE tbl_Location SET MaximumTemperatureCelsius = (MaximumTemperatureFarhenheid -32) / 1.8");
            }
            catch
            {
                Logger.Log(LogType.ltError, "MaxFToC");
            }
            return LResult;
        }
        // min farhenheid naar kelvin
        public static float MinFToK()
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("UPDATE tbl_Location SET MinimumtemperatureKelvin = (MinimumTemperatureFarhenheid + 459.67) * 0.56");
            }
            catch
            {
                Logger.Log(LogType.ltError, "MinFToK");
            }
            return LResult;
        }
        // max farhenheid naar kelvin
        public static float MaxFToK()
        {
            float LResult = 0;
            try
            {
                LResult = SQL.GetSQLDecimal("UPDATE tbl_Location SET MaximumTemperatureKelvin = (MaximumTemperatureFarhenheid + 459.67) * 0.56");
            }
            catch
            {
                Logger.Log(LogType.ltError, "MaxFToK");
            }
            return LResult;
        }
    }
}