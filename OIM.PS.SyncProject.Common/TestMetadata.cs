using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OIM.PS.SyncProject.Common
{
    public class TestMetadata
    {
        public static PSSyncMetadata GetTestMetadata()
        {
            PSSyncMetadata _meta = new PSSyncMetadata();
            _meta.Parameters = new List<Param>();
            _meta.SyncClasses = new List<SyncClass>();

            _meta.Namespace = "OIMPSSyncConnector";
            _meta.ClassName = "OIMPSSyncConnectorClass";

            _meta.Parameters.Add(new Param("URL", "URL to API server"));
            _meta.Parameters.Add(new Param("Username", "Username to login to API server"));
            _meta.Parameters.Add(new Param("Password", "Password to API server"));

            _meta.SyncClasses.Add(new SyncClass("User",
                new List<GenClassProp>()
                {
                    new GenClassProp()
                    {
                        PropertyName = "ID",
                        DataType = DataTypes.String,
                        IsPrimaryKey = true,
                        IsAutoFill = true
                    },
                    new GenClassProp(){ PropertyName = "FirstName" },
                    new GenClassProp() {PropertyName="LastName"},
                    new GenClassProp() {PropertyName="UserName", IsMandatory = true, IsDisplay = true},
                    new GenClassProp() {PropertyName="IsActive", IsMandatory = true, DataType = DataTypes.Bool},
                    new GenClassProp() {PropertyName="StartDate", IsMandatory = true, DataType = DataTypes.DateTime}
                }));

            _meta.SyncClasses.Add(new SyncClass("Group",
                new List<GenClassProp>()
                {
                    new GenClassProp()
                    {
                        PropertyName = "GroupID", DataType = DataTypes.String, IsPrimaryKey = true, IsAutoFill = true
                    },
                    new GenClassProp(){ PropertyName = "GroupName", IsMandatory = true, IsDisplay = true },
                    new GenClassProp() {PropertyName="Description"},
                    new GenClassProp() {PropertyName="IsActive", IsMandatory = true, DataType = DataTypes.Bool}
                }));

            _meta.SyncClasses.Add(new SyncClass("UserInGroup",
                new List<GenClassProp>()
                {
                    new GenClassProp()
                    {
                        PropertyName = "UserId", DataType = DataTypes.String, IsPrimaryKey = true, IsAutoFill = true
                    },
                    new GenClassProp()
                    {
                        PropertyName = "GroupId",  DataType = DataTypes.String, IsPrimaryKey = true
                    },
                    new GenClassProp()
                    {
                        PropertyName = "Description",  IsDisplay = true
                    }
                }));

            return _meta;
        }

    }
}
