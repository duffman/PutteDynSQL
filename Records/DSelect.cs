using System.Text;
using PutteDynSQL.Models;
using PutteDynSQL.Types;

namespace PutteDynSQL.Records {
	public class DSelect : IDRecord {
		public Field[] Fields;

		public DSelect(string column, string alias = null) {
			HaveAlias = alias != null;
		}

		public DSelect(params string[] strFields) {
			Fields = new Field[strFields.Length];

			for (var i = 0; i < strFields.Length; i++) Fields[i] = new Field(strFields[i]);
		}

		public DSelect(params Field[] fields) {
			Fields = fields;
		}

		public bool HaveAlias { get; set; }
		public string Column { get; set; }

		public string ToSql(bool option = false) {
			var result = new StringBuilder("");

			result.Append(Const.DB_SELECT).Append(Const.SPACE);

			for (var i = 0; i < FieldCount(); i++) {
				var field = Fields[i];
				result.Append(field.Name);

				if (!string.IsNullOrEmpty(field.Alias)) {
					result.Append(" AS ").Append(field.Alias);
				}

				if (i + 1 >= FieldCount()) continue;
				result.Append(Const.KEY_DELIM);
			}

			var res = result.ToString();
			return res;
		}

		public int FieldCount() {
			return Fields?.Length ?? 0;
		}
	}
}