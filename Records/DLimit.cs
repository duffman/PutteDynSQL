using System;
using System.Text;

namespace PutteDynSQL.Records {
	public class DLimit : IDRecord {
		public int FromValue { get; set; }
		public int? ToValue { get; set; }

		public DLimit(int fromValue, int? toValue) {
			FromValue = fromValue;
			ToValue = toValue;
		}

		public string ToSql(bool option = false) {
			var result = new StringBuilder();
			result.Append(" LIMIT ").Append(Convert.ToString(FromValue));

			if (ToValue != null) {
				result.Append(", ").Append(Convert.ToString(ToValue));
			}
			
			return result.ToString();
		}
	}
}