using System;
using System.Collections.Generic;
using System.Text;

namespace PutteDynSQL.Utils {
	public class DateUtils {
		public static DateTime UnixTimeStampToDateTime(double timeStamp, bool milliseconds = false) {
			var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			dt = milliseconds ? dt.AddMilliseconds(timeStamp) : dt.AddSeconds(timeStamp);

			return dt.ToLocalTime();
		}
	}
}
