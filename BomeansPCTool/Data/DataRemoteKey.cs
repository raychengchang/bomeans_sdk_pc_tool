using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BomeansPCTool
{
    [DataContract]
    class DataRemoteKeyContainer
    {
        [DataMember]
        public Boolean status;

        [DataMember]
        public int statuc_code;

        [DataMember]
        public string status_message;

        [DataMember]
        public DataRemoteKey[] data;
    }

    [DataContract]
    public class DataRemoteKey
    {
        [DataMember]
        public string button_type;
        [DataMember]
        public string button_key;
        [DataMember]
        public string button_name;

        public DataRemoteKey(String type, String keyId, String keyName)
        {
            button_type = type;
            button_key = keyId;
            button_name = keyName;
        }
    }
}
