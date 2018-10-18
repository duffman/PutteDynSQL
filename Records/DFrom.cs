using System.Text;

namespace PutteDynSQL.Records {
	public class DFrom : IDRecord {
		public string Table { get; set; }
		public string Alias { get; set; }

		public DFrom(string table, string alias = null) {
			Table = table;
			Alias = alias;
		}

		/// <summary>
		/// Returns a SQL string representation
		/// </summary>
		/// <returns></returns>
		public string ToSql(bool isSecond) {
			var result = new StringBuilder();
			result.Append(!isSecond ? "FROM " : ", ").Append(Table);

			if (!string.IsNullOrEmpty(Alias)) {
				result.Append(" AS ").Append(Alias);
			}

			return result.ToString();
		}
	}
}