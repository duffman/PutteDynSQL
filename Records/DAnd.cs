using System;

namespace PutteDynSQL.Records {
	public class DAnd : IDRecord {
		public DAnd(string col, object equalValue = null) {
			Col = col;
			EqualValue = equalValue;
		}

		public string Col { get; set; }
		public object EqualValue { get; set; }

		public string ToSql(bool option) {
			throw new NotImplementedException();
		}
	}
}