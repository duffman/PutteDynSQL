namespace PutteDynSQL.Types {
	public enum WhereType {
	    Unset,             //
		Field,             //
		And,               //

		Equal,             // =
		OtherThan,         // <> or in some verisons !=
		GreaterThan,       // >
		LessThan,          // <
		GreaterOrEqual,    // >=
		LessOrEqual,       // <=
		Between,           // Between an inclusive range
		Like,              // Search for a pattern
		In                 // To specify multiple possible values for a column
	}
}
