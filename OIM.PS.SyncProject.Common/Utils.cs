using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OIM.PS.SyncProject.Common
{
	public class Utils
	{
		public static int GetRandomNumber(int from, int to, int? excludeNumber = null)
		{
			if (excludeNumber.HasValue)
			{
				return GetRandomNumber(from, to, new int[] { excludeNumber.Value });
			}
			else
			{
				return GetRandomNumber(from, to, new int[] { });
			}
		}
		
		public static int GetRandomNumber(int from, int to, int[] excludeNumbers)
		{
			int ret = 0;

			Random rnd = new Random();
			while (true)
			{

				if (excludeNumbers == null || excludeNumbers.Length == 0)
				{
					return rnd.Next(from, to);
				}
				else
				{
					ret = rnd.Next(from, to);

					if (!excludeNumbers.Contains(ret))
					{
						return ret;
					}
				}
			}
		}
	}
}
