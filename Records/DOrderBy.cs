namespace PutteDynSQL.Records {
	public class DOrderBy : IDRecord {
		public string Col { get; set; }

		public DOrderBy(string col) {
			Col = col;
		}

		public string ToSql(bool option) {
			throw new System.NotImplementedException();
		}
	}
}