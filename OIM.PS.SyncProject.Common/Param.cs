using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OIM.PS.SyncProject.Common
{
    
    public class Param
    {
        public Param() { }
        public Param(string paramName, string description)
        {
            Description = description;
            ParamName = paramName;
        }
		public Param(string paramName, string description, bool isSensibleData)
		{
			Description = description;
			ParamName = paramName;
			IsSensibleData = isSensibleData;
		}

		public string ParamName { get; set; }
        public string Description { get; set; }

        private DataTypes dataType = DataTypes.String;

        public DataTypes DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }
        public bool IsSensibleData { get; set; }
    }
}
