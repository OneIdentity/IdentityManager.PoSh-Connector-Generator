using Microsoft.VisualBasic;
using Newtonsoft.Json;
using OIM.PS.SyncProject.Common;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OIM.PS.SyncProject.Generator
{
	internal class JsonFilesGenerator
	{
		public static void PopulateJSONFile(List<SyncClass> synClasses, string filePath)
		{
			Dictionary<string, List<string>> classIDs = new Dictionary<string, List<string>>();
			StringBuilder sbCombined = new StringBuilder();

			//Populate primary keys for regular classes (with single ID)
			foreach (SyncClass synClass in synClasses)
			{
				GenClassProp prop = synClass.Properties.Where(q => q.IsPrimaryKey).FirstOrDefault();
				if (prop != null)
				{
					List<string> vals = new List<string>();

					if (prop.DataType == DataTypes.String)
					{
						vals.AddRange(new string[] { Guid.NewGuid().ToString(),
							Guid.NewGuid().ToString(),
							Guid.NewGuid().ToString(),
							Guid.NewGuid().ToString(),
							Guid.NewGuid().ToString() });

						classIDs.Add(synClass.ClassName, vals);
					}
					else if (prop.DataType == DataTypes.Int)
					{
						vals.AddRange(new string[] {
							1111.ToString(),
							2222.ToString(),
							3333.ToString(),
							4444.ToString(),
							5555.ToString()});
						classIDs.Add(synClass.ClassName, vals);
					}
				}
			}
			

			foreach (SyncClass synClass in synClasses)
			{
				if (synClass.Properties.Any(q => q.IsCombinedPrimaryKey == true))
				{
					ProcessMtMClassWithCombinedPrimaryKey(filePath, classIDs, synClass);
				}
				else
				{
					ProcessRegularClass(filePath, classIDs, synClass);
				}

			}
		}

		private static void ProcessMtMClassWithCombinedPrimaryKey(string filePath, Dictionary<string, List<string>> classIDs, SyncClass synClass)
		{
			StringBuilder tmpSb = new StringBuilder();
			var objects = new List<Dictionary<string, object>>();

			//Populate primary keys for classes with combined primary key
			//Like Many-to-Many relation.
			//Class can contain either primary key or combined primary key.			

			List<GenClassProp> propsIncluded = synClass.Properties
												.Where(q => q.IncludeInCombinedPrimaryKey)
												.OrderBy(q => q.OrderNumber)
												.ToList();

			//tmpSb.Clear();
			//tmpSb.AppendLine("");

			List<string> vals = new List<string>();
			List<int> iterationValues = new List<int>();

			for (int p = 0; p < 5; p++)
			{

				var obj = new Dictionary<string, object>();

				foreach (var genProp in synClass.Properties)
				{
					if (genProp.IsCombinedPrimaryKey)
					{
						string primaryKey = GetConbinedPrimaryKey(classIDs, propsIncluded, p);

						obj.Add($"{genProp.PropertyName}", primaryKey);
					}
					else if (genProp.IncludeInCombinedPrimaryKey)
					{
						var tempKey = "";
						foreach (var key in classIDs.Keys)
						{
							if (genProp.PropertyName.Contains(key, StringComparison.OrdinalIgnoreCase))
							{
								tempKey = key;
								break;
							}
						}

						if (tempKey != "")
						{
							obj.Add($"{genProp.PropertyName}", classIDs[tempKey][p]);
						}
						else
						{
							if (genProp.DataType == DataTypes.String)
							{
								obj.Add($"{genProp.PropertyName}", Guid.NewGuid().ToString());
							}
							else if (genProp.DataType == DataTypes.Int)
							{
								obj.Add($"{genProp.PropertyName}", Utils.GetRandomNumber(1111, 9999));
							}
						}
					}
					else
					{
						if (genProp.DataType == DataTypes.String)
						{
							obj.Add($"{genProp.PropertyName}", Guid.NewGuid().ToString());
						}
						else if (genProp.DataType == DataTypes.Int)
						{
							obj.Add($"{genProp.PropertyName}", Utils.GetRandomNumber(1111, 9999));
						}
						else if (genProp.DataType == DataTypes.Bool)
						{
							obj.Add($"{genProp.PropertyName}", true);
						}
						else if (genProp.DataType == DataTypes.DateTime)
						{
							obj.Add($"{genProp.PropertyName}", DateTime.UtcNow.AddMonths(p + 1));
						}
					}
				}

				objects.Add(obj);


			}

			string output = JsonConvert.SerializeObject(objects, Formatting.Indented);

			System.IO.File.WriteAllText(Path.Combine(filePath, synClass.ClassName.ToLower() + ".json"), output);
			
		}

		public static string GetConbinedPrimaryKey(Dictionary<string, List<string>> classIDs, List<GenClassProp> propsIncluded, int iteration)
		{
			StringBuilder sbCombined = new StringBuilder();

			var tempClassName = "";
			
			foreach (var prop in propsIncluded)
			{
				foreach (var key in classIDs.Keys)
				{
					//classIDs.Keys represent Class names. Like 'User'
					//If some other class contains a collection os users like: AdminUsers or just Users
					//Then we know we need to get that class's IDs
					if (prop.PropertyName.Contains(key, StringComparison.OrdinalIgnoreCase))
					{
						tempClassName = key;
												
						if (!string.IsNullOrEmpty(tempClassName))
						{
							sbCombined.Append(classIDs[tempClassName][iteration] + "|");
						}
					}
				}
			}

			var str = sbCombined.ToString().TrimEnd('|');
			return str;
		}

		private static void ProcessRegularClass(string filePath, Dictionary<string, List<string>> classIDs, SyncClass synClass)
		{
			StringBuilder tmpSb = new StringBuilder();

			var objects = new List<Dictionary<string, object>>();

			for (int i = 0; i < 5; i++)
			{
				var obj = new Dictionary<string, object>();

				tmpSb.Clear();
				tmpSb.AppendLine("");

				//var props = _meta.GetClassByName(synClass.ClassName).Properties.OrderBy(q => q.OrderNumber);
				var props = synClass.Properties.OrderBy(q => q.OrderNumber);

				foreach (var item in props) //_classes[className])                    
				{
					if (item.IsPrimaryKey)
					{
						if (item.DataType == DataTypes.String)
						{
							obj.Add($"{item.PropertyName}", classIDs[synClass.ClassName][i]);
						}
						else if (item.DataType == DataTypes.Int)
						{
							obj.Add($"{item.PropertyName}", Int32.Parse(classIDs[synClass.ClassName][i]));
						}
					}
					else
					{
						if (item.DataType == DataTypes.String)
						{
							if (item.IsMultivalue)
							{
								string tempClassName = null;
								//P.S. --> Here we need to check if this property name contains any other class names.
								//--> Like Property 'Groups' contains class name 'Group' - then it is a list of groups from the class.
								//--> Will need to get precalculated IDs.
								//--> If not - then as usual - just generate some values based on property name
								foreach (var key in classIDs.Keys)
								{
									if (item.PropertyName.Contains(key, StringComparison.OrdinalIgnoreCase))
									{
										tempClassName = key;
									}
								}

								if (!string.IsNullOrEmpty(tempClassName))
								{
									var num1 = Utils.GetRandomNumber(0, 5);
									var num2 = Utils.GetRandomNumber(0, 5, num1);

									obj.Add($"{item.PropertyName}", new object[] { classIDs[tempClassName][num1], classIDs[tempClassName][num2] });
								}
								else
								{
									//P.S. If property ends with 's' - like Groups - remove 's'.
									// Also number should be between 0 and 4 and two numbers should be different.
									var propName = item.PropertyName.Trim();
									if (propName.EndsWith('s'))
									{
										propName = propName.TrimEnd('s');
									}

									var num1 = Utils.GetRandomNumber(1, 6);
									var num2 = Utils.GetRandomNumber(1, 6, num1);

									obj.Add($"{item.PropertyName}", new object[] { $"Test{propName}{num1.ToString()}", $"Test{propName}{num2.ToString()}" });
								}
							}
							else
							{
								if (synClass.IsManyToMany)
								{
									string tempClassName = null;
									//P.S. --> Here we need to check if this property name contains any other class names.
									//--> Like Property 'Groups' contains class name 'Group' - then it is a list of groups from the class.
									//--> Will need to get precalculated IDs.
									//--> If not - then as usual - just generate some values based on property name
									foreach (var key in classIDs.Keys)
									{
										if (item.PropertyName.Contains(key, StringComparison.OrdinalIgnoreCase) &&
												!item.PropertyName.Contains(synClass.ClassName, StringComparison.OrdinalIgnoreCase))
										{
											tempClassName = key;
										}
									}

									if (!string.IsNullOrEmpty(tempClassName))
									{
										var num1 = Utils.GetRandomNumber(0, 5);

										obj.Add($"{item.PropertyName}", classIDs[tempClassName][num1]);
									}
									else
									{
										obj.Add($"{item.PropertyName}", $"Test{synClass.ClassName}{item.PropertyName}{(i + 1).ToString()}");
									}
								}
								else
								{
									obj.Add($"{item.PropertyName}", $"Test{synClass.ClassName}{item.PropertyName}{(i + 1).ToString()}");
								}								
							}
						}
						else if (item.DataType == DataTypes.Int)
						{
							if (item.IsMultivalue)
							{
								string tempClassName = null;
								//P.S. --> Here we need to check if this property name contains any other class names.
								//--> Like Property 'Groups' contains class name 'Group' - then it is a list of groups from the class.
								//--> Will need to get precalculated IDs.
								//--> If not - then as usual - just generate some values based on property name
								foreach (var key in classIDs.Keys)
								{
									if (item.PropertyName.Contains(key, StringComparison.OrdinalIgnoreCase))
									{
										tempClassName = key;
									}
								}

								if (!string.IsNullOrEmpty(tempClassName))
								{
									var num1 = Utils.GetRandomNumber(0, 5);
									var num2 = Utils.GetRandomNumber(0, 5, num1);

									obj.Add($"{item.PropertyName}", new int[] { Int32.Parse(classIDs[tempClassName][num1]),
																					Int32.Parse(classIDs[tempClassName][num2]) });
								}
								else
								{
									//P.S. If property ends with 's' - like Groups - remove 's'.
									// Also number should be between 0 and 4 and two numbers should be different.
									var propName = item.PropertyName.Trim();
									if (propName.EndsWith('s'))
									{
										propName = propName.TrimEnd('s');
									}

									var num1 = Utils.GetRandomNumber(1, 6);
									var num2 = Utils.GetRandomNumber(1, 6, num1);

									obj.Add($"{item.PropertyName}", new int[] { Int32.Parse($"{num1}{num1}{num1}{num1}"),
																					Int32.Parse($"{num2}{num2}{num2}{num2}")});
								}


							}
							else
							{
								if (synClass.IsManyToMany)
								{
									string tempClassName = null;
									//P.S. --> Here we need to check if this property name contains any other class names.
									//--> Like Property 'Groups' contains class name 'Group' - then it is a list of groups from the class.
									//--> Will need to get precalculated IDs.
									//--> If not - then as usual - just generate some values based on property name
									foreach (var key in classIDs.Keys)
									{
										if (item.PropertyName.Contains(key, StringComparison.OrdinalIgnoreCase))
										{
											tempClassName = key;
										}
									}

									if (!string.IsNullOrEmpty(tempClassName))
									{
										var num1 = Utils.GetRandomNumber(0, 5);

										obj.Add($"{item.PropertyName}", Int32.Parse(classIDs[tempClassName][num1]));
									}
									else
									{
										obj.Add($"{item.PropertyName}", Int32.Parse($"{i + 1}{i + 1}{i + 1}{i + 1}"));
									}
								}
								else
								{
									obj.Add($"{item.PropertyName}", Int32.Parse($"{i + 1}{i + 1}{i + 1}{i + 1}"));
								}
							}
						}
						else if (item.DataType == DataTypes.Bool)
						{
							obj.Add($"{item.PropertyName}", true);
						}
						else if (item.DataType == DataTypes.DateTime)
						{
							obj.Add($"{item.PropertyName}", DateTime.UtcNow.AddMonths(i + 1));
						}
					}
				}
				objects.Add(obj);
			}

			string output = JsonConvert.SerializeObject(objects, Formatting.Indented);

			System.IO.File.WriteAllText(Path.Combine(filePath, synClass.ClassName.ToLower() + ".json"), output);
		}

	}
}
