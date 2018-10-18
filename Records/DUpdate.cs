using System.Collections.Generic;
using System.Reflection;
using System.Text;
using PutteDynSQL.Types;

namespace PutteDynSQL.Records {
	public class DUpdate : IDRecord {
		public string Table { get; set; }
		public IDictionary<string, FieldVariant> MultiField  = null;

		public DUpdate(string table) {
			Table = table;
		}

		public DUpdate(string table, object keyValues) {
			Table = table;
			MultiField = new Dictionary<string, FieldVariant>();

			foreach (var item in keyValues.GetType().GetRuntimeProperties()) {
				var value = new FieldVariant(item.GetValue(keyValues));
				MultiField.Add(item.Name, value);
			}
		}

		public string ToSql(bool option = false) {
			var result = new StringBuilder();
			var i = 0;

			result.Append("UPDATE ").Append(Table).Append(" SET ");

			foreach (var field in MultiField) {
				result.Append(field.Key);
				result.Append("=");
				result.Append(field.Value.Value(true));

				if (i + 1 < MultiField.Count) {
					result.Append(", ");
				}

				i++;
			}

			return result.ToString();
		}
	}
}