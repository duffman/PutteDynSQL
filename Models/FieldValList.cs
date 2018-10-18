namespace PutteDynSQL.Models {
	public class FieldValList {
		public FieldVal[] Fields;

		public FieldValList(params FieldVal[] fields) {
			Fields = fields;
		}
	}
}
