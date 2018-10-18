using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using PutteDynSQL.Extensions;
using PutteDynSQL.Utils;

namespace PutteDynSQL.Types {
	public class DateField {
		public DateTime DateObj;
		public bool IsNull = true;
		public bool IsNow = false;

		public DateField(double timestamp) {
			SetValue(timestamp);
		}

		public DateField(object dateVal) {
			SetValue(dateVal);
		}

		private void SetValue(object value) {
			IsNull = false;

			if (value is string && (value as string).Trim().Length > 0) {

				if ((value as string).Equals("NOW")) {
					IsNow = true;
				} else
					DateObj = DateTime.Parse(value.ToString());
			}
			else if (value is double) {
				var doubleValue = Convert.ToDouble(value);
				DateObj = DateUtils.UnixTimeStampToDateTime(doubleValue);
			}
			else {
				IsNull = true;
			}
		}

		public string ToDateString() {
			var result = "";

			if (IsNow) {
				result = "NOW()";
			}
			else if (IsNull) {
				result = "NULL";
			}
			else {
				DateObj.ToSqlDateString();
			}

			return result;
		}
	}
}
