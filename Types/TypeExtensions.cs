using System.Collections.Generic;
using System.Text;

namespace PutteDynSQL.Types {
	public static class TypeExtensions {
		public static string CommaSepString(this IList<string> list, string suffix = "") {
			var result = new StringBuilder();

			for (var i = 0; i < list.Count; i++) {
				var listItem = list[i];
				result.Append(listItem);
				result.Append(suffix);


				if (i + 1 < list.Count) {
					result.Append(", ");
				}
			}

			return result.ToString();
		}
	}
}
