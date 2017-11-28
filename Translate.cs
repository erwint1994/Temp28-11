using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using paSSQL;
namespace Vertalen
{
    public struct TrnRec
    {
        public string NL;
        public string EN;
        public string DE;
    }
    public static class Vertaal
    {
        static public Dictionary<string, TrnRec> TrnRecList = new Dictionary<string, TrnRec>();
        static public void LoadTranslation()
        {
            DataView dv = SQL.GetSQLDataView("SELECT NL, EN, DE FROM tbl_Translate");
            try
            {
                foreach (DataRowView R in dv)
                {
                    TrnRec LR = new TrnRec
                    {
                        NL = (string)R["NL"],
                        EN = (string)R["EN"],
                        DE = (string)R["DE"]
                    };
                    TrnRecList.Add(LR.NL, LR);
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }
        static public bool WriteNewTekst(TrnRec ATrnRec)
        {
            try
            {
                SQL.SQLParams.Clear();
                SQLParam P = new SQLParam
                { PName = "NLVal", PType = SqlDbType.VarChar, PValue = ATrnRec.NL }; SQL.SQLParams.Add(P.PName, P);
                P = new SQLParam
                {
                    PName = "ENVal",
                    PType = SqlDbType.VarChar,
                    PValue = ATrnRec.EN
                }; SQL.SQLParams.Add(P.PName, P);
                P = new SQLParam
                {
                    PName = "DEVal",
                    PType = SqlDbType.VarChar,
                    PValue = ATrnRec.DE
                }; SQL.SQLParams.Add(P.PName, P);
                SQL.InsertUpdateQuery("INSERT INTO tbl_Translate (NL, EN, DE) VALUES (@NLValue, @ENValue, @DEValue)");
                return true;
            }
            catch
            {
                return false;
            }
        }
        static public bool WriteUpdatetekst(TrnRec ATrnRec)
        {
            try
            {
                SQL.SQLParams.Clear();
                SQLParam P = new SQLParam
                {
                    PName = "NLVal",
                    PType = SqlDbType.VarChar,
                    PValue = ATrnRec.NL
                }; SQL.SQLParams.Add(P.PName, P);
                P = new SQLParam
                {
                    PName = "ENVal",
                    PType = SqlDbType.VarChar,
                    PValue = ATrnRec.EN
                }; SQL.SQLParams.Add(P.PName, P);
                P = new SQLParam
                {
                    PName = "DEVal",
                    PType = SqlDbType.VarChar,
                    PValue = ATrnRec.DE
                }; SQL.SQLParams.Add(P.PName, P);
                SQL.InsertUpdateQuery("UPDATE tbl_Translate SET EN = @ENValue, DE = @DEValue WHERE NL = @NLValue");
                return true;
            }
            catch
            {
                return false;
            }
        }

        static public void DoVertaalForm(Control AForm, string ALang)
        {
            foreach (Control LControl in AForm.Controls)
            {
                LControl.Text = Translate(LControl.Text, ALang);
            }
        }
        static public string Translate(string AFrom, string ALang)
        {
            if (TrnRecList.ContainsKey(AFrom))
            {
                return TrnRecList[AFrom].EN;
            }
            else
            {
                TrnRec LR = new TrnRec
                {
                    NL = AFrom
                };
                // Ophalen met API
                // Wegschrijven naar DB
                WriteNewTekst(LR);
                // Teruggeven vanuit lijst            
                return LR.NL;
            }
        }
        static public void VertaalContexMenueStrip(ToolStripMenuItem AForm, string ALang)
        {
            AForm.Text = Translate(AForm.Text, ALang);
        }
        static public void VertaalContexMenueStrip2(ToolStripItem AForm, string ALang)
        {
            AForm.Text = Translate(AForm.Text, ALang);
        }
    }
}


 
