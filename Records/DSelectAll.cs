using System.Collections.Generic;
using System.Text;
using PutteDynSQL.Types;

namespace PutteDynSQL.Records {
	public class DSelectAll : IDRecord {
		public IList<string> TableList { get; set; }

		public DSelectAll(params string[] tables) {
			TableList = new List<string>();

			foreach (var table in tables) {
				TableList.Add(table);	
			}
		}

		public string Table { get; set; }

		private string ToSingleSql() {
			return string.Empty;
		}

		public string ToSql(bool option = false) {
			var result = new StringBuilder();
			result.Append(Const.DB_SELECT).Append(Const.SPACE);

			if (TableList.Count == 1) {
				result.Append("*");
			}
			else {
				result.Append(TableList.CommaSepString(".*"));
			}

			result.Append(Const.SPACE)
					.Append(Const.DB_FROM)
					.Append(Const.SPACE);

			result.Append(TableList.CommaSepString());

			return result.ToString();
		}
	}
}