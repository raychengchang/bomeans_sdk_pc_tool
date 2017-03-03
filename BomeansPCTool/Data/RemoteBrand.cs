using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomeansPCTool
{
    public class RemoteBrand
    {
        public String ID { get; set; }
        public String NameEN { get; set; }
        public String Name { get; set; }

        public RemoteBrand(String id, String nameEn, String name)
        {
            ID = id;
            NameEN = nameEn;
            Name = name;
        }

        public override string ToString()
        {
            if (NameEN.Equals(Name))
            {
                return NameEN;
            }
            else
            {
                return String.Format("{0} ({1})", NameEN, Name);
            }
        }
    }
}
