using OIM.PS.SyncProject.Common;

namespace OIM.PS.SyncProject.Web.Models;

public class GeneratorViewModel
{
	public string Namespace { get; set; } = "";
	public string ClassName { get; set; } = "";
	public List<Param> Parameters { get; set; } = new();
	public List<SyncClass> SyncClasses { get; set; } = new();
}

public class AddParamModel
{
	public string ParamName { get; set; } = "";
	public string Description { get; set; } = "";
	public DataTypes DataType { get; set; } = DataTypes.String;
	public bool IsSensibleData { get; set; }
}

public class AddClassModel
{
	public string ClassName { get; set; } = "";
	public bool IsManyToMany { get; set; }
	public List<GenClassPropModel> Properties { get; set; } = new();
}

public class GenClassPropModel
{
	public string PropertyName { get; set; } = "";
	public DataTypes DataType { get; set; } = DataTypes.String;
	public bool IsPrimaryKey { get; set; }
	public bool IsUniqueKey { get; set; }
	public bool IsMandatory { get; set; }
	public bool IsAutoFill { get; set; }
	public bool IsDisplay { get; set; }
	public bool IsRevision { get; set; }
	public bool IsMultivalue { get; set; }
	public int OrderNumber { get; set; }
	public bool IsCombinedPrimaryKey { get; set; }
	public bool IncludeInCombinedPrimaryKey { get; set; }
}
