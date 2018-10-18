using System;
using System.Collections.Generic;
using System.Text;

namespace PutteDynSQL.Extensions {
	public static class DateTimeExtensions {
		public static string ToSqlDateString(this DateTime dt) {
			return dt.ToString("yyyy-MM-dd HH:mm:ss");
		}
	}
}
