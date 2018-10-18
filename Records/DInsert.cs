using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using PutteDynSQL.Models;
using PutteDynSQL.Types;

namespace PutteDynSQL.Records {
	public class DInsert : IDRecord {
		public string Table { get; set; }
		public bool MySQLReplace { get; set; }
		public FieldValList Data { get; set; }
		private IDictionary<string, FieldVariant> MultiField = null;

		public DInsert(string table, object keyValues, bool replace = false) {
			Table = table;
			MySQLReplace = replace;

			MultiField = new Dictionary<string, FieldVariant>();

			foreach (var item in keyValues.GetType().GetRuntimeProperties()) {
				var value = new FieldVariant(item.GetValue(keyValues));
				MultiField.Add(item.Name, value);
			}
		}

		public DInsert(string table, FieldValList data, bool replace) {
			Table = table;
			Data = data;
			MySQLReplace = replace;
		}

		public int FieldCount() {
			return Data.Fields.Length;
		}

		private string ParseMultiFields() {
			var result = new StringBuilder();
			var keys = new StringBuilder();
			var values = new StringBuilder();

			//TODO: FIX THE REPLACE
			var type = MySQLReplace ? result.Append(Const.DB_INSERT) : result.Append(Const.DB_INSERT);
			result.Append(" INTO ").Append(Table).Append(" (");
			
			var i = 0;

			foreach (var field in MultiField) {
				keys.Append(field.Key);
				values.Append(field.Value.Value() ?? "NULL");

				if (i + 1 < MultiField.Count) {
					keys.Append(", ");
					values.Append(", ");
				}

				i++;
			}

			result.Append(keys);
			result.Append(") VALUES (");
			result.Append(values);
			result.Append(")");

			return result.ToString();
		}

		private string ParseResularFixThis() {
			var result = new StringBuilder();
			var keys = new StringBuilder();
			var values = new StringBuilder();

			var type = MySQLReplace ? result.Append(Const.DB_MYSQL_REPLACE) : result.Append(Const.DB_INSERT);
			result.Append(" INTO ").Append(Table).Append(" (");

			for (var i = 0; i < FieldCount(); i++) {
				var field = Data.Fields[i];

				keys.Append(field.FieldName);

				values.Append(field.Value() ?? "NULL");

				if (i + 1 >= FieldCount()) continue;
				keys.Append(Const.KEY_DELIM);
				values.Append(Const.KEY_DELIM);
			}

			result.Append(keys);
			result.Append(") VALUES (");
			result.Append(values);
			result.Append(")");

			return result.ToString();
			
		}

		public string ToSql(bool option = false) {
			if (MultiField == null) {
				return ParseResularFixThis();
			}
			else {
				return ParseMultiFields();
			}
		}
	}
}