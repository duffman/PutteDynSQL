using System;
using PutteDynSQL.Types;

namespace PutteDynSQL.Models {
	public class FieldVal {
		public string FieldName { get; set; }
		public FieldVariant FieldValue { get; set; }
		
		public FieldVal(string field, int val) {
			FieldName = field;
			FieldValue = new FieldVariant(val);
		}

		public FieldVal(string field, string val) {
			FieldName = field;
			FieldValue = new FieldVariant(val);
		}

		public FieldVal(string field, bool val) {
			FieldName = field;
			FieldValue = new FieldVariant(val);
		}

		public FieldVal(string field, double val) {
			FieldName = field;
			FieldValue = new FieldVariant(val);
		}

		public FieldVal(string field, DateTime val) {
			FieldName = field;
			FieldValue = new FieldVariant(val);
		}

		public string Value() {
			return FieldValue != null ? FieldValue.Value() : string.Empty;
		}
	}
}
