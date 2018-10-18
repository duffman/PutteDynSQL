namespace PutteDynSQL.Records {
	public class DSet : IDRecord {
		public string Column { get; set; }
		public object Value { get; set; }

		public DSet(string column, object value) {
			Column = column;
			Value = value;
		}

		public string ToSql(bool option) {
			throw new System.NotImplementedException();
		}
	}
}