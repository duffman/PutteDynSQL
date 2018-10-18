using System;

namespace PutteDynSQL.Records {
	public class DLeftJoin : IDRecord {
		public DLeftJoin(string table, string on) {
			Table = table;
			On = on;
		}

		public string Table { get; set; }
		public string On { get; set; }

		public string ToSql(bool option) {
			throw new NotImplementedException();
		}
	}
}