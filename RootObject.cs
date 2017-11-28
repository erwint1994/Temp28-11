using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;



namespace WindowsFormsApp1
{

    public class RootObject

    {
        public int Id { get; set; }
        public List<string> Addr_from { get; set; }
        public List<string> Addr_to { get; set; }
        public List<string> Addr_cc { get; set; }
        public List<string> Addr_bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Description { get; set; }
        public string Eventtype_ad { get; set; }
        public string Docref_ad { get; set; }
        public int Doc_id { get; set; }
        public string Doctype_ad { get; set; }
        public string Rel_ad { get; set; }
        public int Msg_status_id { get; set; }
        public string Doc_contents { get; set; }
        public string Doc_filename { get; set; }
        public string Doc_contents_2 { get; set; }
        public string Doc_filename_2 { get; set; }
        public string Doc_contents_3 { get; set; }
        public string Doc_filename_3 { get; set; }
        public string Doc_contents_4 { get; set; }
        public string Doc_filename_4 { get; set; }
        public string Doc_contents_5 { get; set; }
        public string Doc_filename_5 { get; set; }
        public DateTime Dt_sent { get; set; }
        public string SendLog { get; set; }
        public DateTime Dt_created { get; set; }
        public DateTime Dt_modified { get; set; }
        public int Syshumres_id { get; set; }
        public int Systerminal_id { get; set; }
        public int Syscompany_id { get; set; }
    }
}