using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OIM.PS.SyncProject.Common
{
    public class SyncClass
    {
        public SyncClass() { }
        public SyncClass(string className, List<GenClassProp> properties)
        {
            ClassName = className;
            Properties = properties;
        }
        
        public string ClassName { get; set; }
		public bool IsManyToMany { get; set; }

		private List<GenClassProp> _properties;
        public List<GenClassProp> Properties
        {
            get 
            { 
                if(_properties != null)
                {
                    _properties = _properties.OrderBy(q=>q.OrderNumber).ToList();
                }

                return _properties;
            }
            set { _properties = value; }
        }

        public string PropertyNames
        {
            get
            {
                if(Properties == null)
                {
                    return null;
                }

                StringBuilder sb = new StringBuilder();
                foreach (var item in Properties.OrderBy(q=>q.OrderNumber).ToList())
                {
                    sb.Append(item.PropertyName);
                    sb.Append(",");
                }

                return sb.ToString().Trim().TrimEnd(',');
            }
        }
    }
}
