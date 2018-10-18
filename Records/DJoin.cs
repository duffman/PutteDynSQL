using System;
using PutteDynSQL.Models;
using PutteDynSQL.Types;

namespace PutteDynSQL.Records {
	public class DJoin : IDRecord {
		public DJoin(TabeField joinTableField, Comparison compare, TabeField withTableField) {
			JoinTableField = joinTableField;
			WithTableField = withTableField;
		}

		public TabeField JoinTableField { get; set; }
		public TabeField WithTableField { get; set; }

		public string ToSql(bool option) {
			throw new NotImplementedException();
		}
	}
}