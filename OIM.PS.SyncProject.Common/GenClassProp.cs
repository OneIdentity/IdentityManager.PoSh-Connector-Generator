using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OIM.PS.SyncProject.Common
{
    public class GenClassProp
    {
        private DataTypes _DataType = DataTypes.String;
        private bool _IsPrimaryKey;

        public string PropertyName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DataTypes DataType
        {
            get { return _DataType; }
            set { _DataType = value; }
        }

        [JsonIgnore]
        public string DataTypeClass
        {
            get
            {
                string ret = "string";

                switch (_DataType)
                {
                    case DataTypes.String:
                        ret = "string";
                        break;
                    case DataTypes.Bool:
                        ret = "bool";
                        break;
                    case DataTypes.Int:
                        ret = "int";
                        break;
                    case DataTypes.DateTime:
                        ret = "DateTime";
                        break;
                    default:
                        ret = "string";
                        break;
                }

                return ret;
            }
        }

        public bool IsPrimaryKey
        {
            get { return _IsPrimaryKey; }
            set
            {
                _IsPrimaryKey = value;
                IsMandatory = true;
                IsUniqueKey = true;
            }
        }
        public bool IsUniqueKey { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsAutoFill { get; set; }
        public bool IsDisplay { get; set; }
        public bool IsRevision { get; set; }
        public bool IsMultivalue { get; set; }
        public int OrderNumber { get; set; }
    }
}
