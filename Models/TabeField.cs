namespace PutteDynSQL.Models {
	public class TabeField {
		public string Table { get; set; }
		public string TableField { get; set; }

		public TabeField(string table, string field) {
			Table = table;
			TableField = field;
		}
	}
}
