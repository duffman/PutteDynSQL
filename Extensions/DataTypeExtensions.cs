using System;
using System.Collections.Generic;
using System.Text;
using PutteDynSQL.Types;

namespace PutteDynSQL.Extensions {
	public static class DataTypeExtensions {
		public static string PrepStr(this object obj) {
			return new FieldVariant(obj).Value();
		}
	}
}
