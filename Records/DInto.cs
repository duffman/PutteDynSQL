namespace PutteDynSQL.Records {
	public class DInto : IDRecord {
		public string TableName { get; set; }

		public DInto(string tableName) {
			TableName = tableName;
		}

		public string ToSql(bool option) {
			throw new System.NotImplementedException();
		}
	}
}