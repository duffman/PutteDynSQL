using System;
using System.Globalization;
using System.Text;

namespace PutteDynSQL.Types {
	public class FieldVariant {
		public string Str = null;
		public int? Int = null;
		public long? Long = null;
		public bool? Bool = null;
		public double? Double = null;
		public DateTime? Date = null;
		public DateField DateField = null;

		public FieldVariant(object value) {
			switch (value) {
				case string s:
					Str = s;
					break;
				case int intValue:
					Int = intValue;
					break;
				case long longValue:
					Long = longValue;
					break;
				case bool boolValue:
					Bool = boolValue;
					break;
				case double doubleValue:
					Double = doubleValue;
					break;
				case DateTime dateValue:
					Date = dateValue;
					break;
				case DateField dateField:
					DateField = dateField;
					break;
			}
		}
	
		public string Value(bool escapeString = true) {
			return GetValue(escapeString);
		}

		public string GetValue(bool escapeString = true) {
			if (Int != null) {
				return Convert.ToString(Int);
			}

			if (Long != null) {
				return Convert.ToString(Long);
			}

			if (Str != null) {
				var res = new StringBuilder();

				if (escapeString) {
					res.Append("'").Append(TypeUtils.Escape(Str)).Append("'");
				}
				else {
					res.Append(Str);
				}

				return res.ToString();
			}

			if (Bool != null) {
				var bVal = Bool.GetValueOrDefault();
				return bVal ? "true" : "false";
			}

			if (Double != null) {
				var dVal = Double.GetValueOrDefault();
				return Convert.ToString(dVal, CultureInfo.InvariantCulture);
			}

			if (Date != null) {
				var res = new StringBuilder();
				var dateStr = this.Date.ToString();
				
				if (escapeString) {
					res.Append("'").Append(dateStr).Append("'");
				}
				else {
					res.Append(dateStr);
				}

				return res.ToString();
			}

			if (DateField != null) {

				var res = new StringBuilder();
				var dateFieldValue = DateField.ToDateString();

				if (escapeString && !DateField.IsNull && !DateField.IsNow) {
					res.Append("'").Append(dateFieldValue).Append("'");
				}
				else {
					res.Append(dateFieldValue);
				}

				return res.ToString();
			}

			return null;

		}
	}
}
