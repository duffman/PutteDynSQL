namespace PutteDynSQL.Models {
	public class Field {
		public string Name { get; set; }
		public string Alias { get; set; }

		public Field(string name, string alias = null) {
			Name = name;
			Alias = alias;
		}
	}
}
