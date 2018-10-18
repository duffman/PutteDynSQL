namespace PutteDynSQL.Records {
	public class DWith : IDRecord {
		public string[] Data;

		public DWith(params string[] data) {
			Data = data;
		}

		public string ToSql(bool option) {
			throw new System.NotImplementedException();
		}
	}
}