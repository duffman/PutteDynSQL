using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using PutteDynSQL.Extensions;
using PutteDynSQL.Types;

namespace PutteDynSQL.Records {
	public class DWhere : IDRecord {
		public WhereType WhereType = WhereType.Equal;
		public string FieldName { get; set; }
		public string CompareTo { get; set; }
		public FieldVariant CompareField = null;

		public object RangeStart = null;
		public object RangeEnd = null;

		private IDictionary<string, FieldVariant> MultiField = null;

		private object GetParamVal(IReadOnlyList<object> values, int index) {
			return index <= values.Count ? values[index] : "";
		}

		public DWhere(WhereType type, string fieldName, params object[] values) {
			WhereType = type;
			FieldName = fieldName;

			if (type == WhereType.Field) {
				CompareTo = Convert.ToString(GetParamVal(values, 0));
				return;
			}

			var firstValue = new FieldVariant(GetParamVal(values, 0));

			if (type == WhereType.Equal) {
				CompareTo = firstValue.Value();
			}
		}

		public DWhere(object keyValues) {
			MultiField = new Dictionary<string, FieldVariant>();

			foreach (var item in keyValues.GetType().GetRuntimeProperties()) {
				var value = new FieldVariant(item.GetValue(keyValues));
				MultiField.Add(item.Name, value);
			}
		}

		private string WhereTypeToString(WhereType where) {
			var result = "";

			switch (where) {
				case WhereType.Field:
					result = "=";
					break;
				case WhereType.Equal:
					result = "=";
					break;
				case WhereType.OtherThan:
					result = "<>";
					break;
				case WhereType.GreaterThan:
					result = ">";
					break;
				case WhereType.LessThan:
					result = "<";
					break;
				case WhereType.GreaterOrEqual:
					result = ">=";
					break;
				case WhereType.LessOrEqual:
					result = "<=";
					break;
				case WhereType.Between:
					result = "BETWEEN";
					break;
				case WhereType.Like:
					result = "LIKE";
					break;
				case WhereType.In:
					result = "IN";
					break;
			}

			return result;
		}

		private StringBuilder ParseMultiFields() {
			var result = new StringBuilder();
			var i = 0;

			foreach (var field in MultiField) {
				result.Append(field.Key);
				result.Append("=");
				result.Append(field.Value.Value());

				if (i + 1 < MultiField.Count) {
					result.Append(" AND ");
				}

				i++;
			}

			return result;
		}

		private StringBuilder ParseColValue() {
			var result = new StringBuilder();

			result.Append(FieldName);
			result.Append(" ").Append(WhereTypeToString(WhereType)).Append(" ");
			result.Append(CompareTo);

			return result;
		}

		public string ToSql(bool isSecond) {
			var result = new StringBuilder();

			result.Append(!isSecond ? "WHERE " : "AND ");
			result.Append(MultiField != null ? ParseMultiFields() : ParseColValue());

			var res = result.ToString();
			return res;
		}
	}
}