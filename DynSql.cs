using System;
using System.Collections.Generic;
using System.Text;
using PutteDynSQL.Models;
using PutteDynSQL.Records;
using PutteDynSQL.Types;

namespace PutteDynSQL {
	/**
		* Simple Active Record implementation
		* Note: This does not add any intelligens, stupid behaviour such
		* as calling an SELECT after a SET, broken SQL will remain broken :)
		*/
	public class DynSql {
		public string PrepMySQLDate(DateTime dateObj) {
			//	dateObj.setHours(dateObj.getHours() - 2);
			//	return dateObj.toISOString().slice(0, 19).replace('T', ' ');
			return "";
		}

		public class DataColumn {
			FieldDataType DataType;
			public string Name { get; set; }
			int Length;
			public object ValueTuple { get; set; }
			public DataColumn(object value, int length) {
				Length = length;
			}
		}

		private string DbName { get; set; }
		private IList<IDRecord>  DRecords { get; set; }

		public DynSql(string dbName = "") {
			DbName = dbName;
			DRecords = new List<IDRecord>();
		}

		public void Clear() {
			DRecords.Clear();
		}

		public DynSql SelectAll(params string[] elements) {
			DRecords.Add(new DSelectAll(elements));
			return this;
		}

		public DynSql Select(params string[] elements) {
			DRecords.Add(new  DSelect(elements));
			return this;
		}

		public DynSql Select(params Field[] elements) {
			DRecords.Add(new DSelect(elements));
			return this;
		}

		public DynSql Update(string tableName, object multiValue) {
			DRecords.Clear();
			DRecords.Add(new DUpdate(tableName, multiValue));
			return this;
		}

		public DynSql Insert(string table, FieldValList data, bool replace = false) {
			DRecords.Clear();
			DRecords.Add(new DInsert(table, data, replace));
			return this;
		}

		public DynSql Insert(string table, object multiValue, bool replace = false) {
			DRecords.Clear();
			DRecords.Add(new DInsert(table, multiValue, replace));
			return this;
		}

		public DynSql With(params string[] elements) {
			foreach (var element in elements) {
				DRecords.Add(new DWith(element));
			}

			return this;
		}

		public DynSql Into(params string[] elements) {
			foreach (var element in elements) {
				DRecords.Add(new DInto(element));
			}

			return this;
		}

		public DynSql Set(string column, object value) {
			DRecords.Add(new DSet(column, value));
			return this;
		}

		public DynSql LeftJoin(string table, string on) {
			DRecords.Add(new DLeftJoin(table, on));
			return this;
		}

		/*
		public DynSQL InnerJoin(string table, string withTable) {
			DRecords.Add(new DLeftJoin(table, on));
			return this;
		}

		public DynSQL CrossJoin(string table, string withTable) {
			DRecords.Add(new DLeftJoin(table, on));
			return this;
		}
		*/

		public DynSql SelectAs(string fromTable, string alias = null) {
			DRecords.Add(new DSelect(fromTable, alias));
			return this;
		}

		public DynSql From(string table, string alias = null) {
			var rec = new DFrom(table, alias);
			DRecords.Add(rec);
			return this;
		}

		private string PrepValue(object value) {
			var strValue = Convert.ToString(value);

			if (TypeUtils.IsString(value)) {
				strValue = "'" + strValue + "'";
			}
			else if (TypeUtils.IsNumber(value)) {
				//strValue = strValue;
			}
			else if (TypeUtils.IsDate(strValue)) {
				strValue = TypeUtils.PrepMySQLDate(strValue);
			}

			return strValue;
		}

		public DynSql WhereField(string field, string matchField) {
			var rec = new DWhere(WhereType.Field, field, matchField);
			DRecords.Add(rec);

			return this;
		}

		public DynSql Where(string field, object value = null) {
			var rec = new DWhere(WhereType.Equal, field, value);
			DRecords.Add(rec);

			return this;
		}
		
		public DynSql Where(object multiValue) {
			var rec = new DWhere(multiValue);
			DRecords.Add(rec);

			return this;
		}
		
		public DynSql WhereBetween(string field, object rangeStart, object rangeEnd) {
			var rec = new DWhere(WhereType.Between, field, rangeStart, rangeEnd);
			DRecords.Add(rec);
				
			return this;
		}
		/************************
		 * ********************/


		public DynSql AndField(string field, string matchField) {
			var rec = new DWhere(WhereType.Field, field, matchField);
			DRecords.Add(rec);
			return this;
		}

		public DynSql And(string field, object value = null) {
			Where(field, value);
			return this;
		}

		public DynSql And(object multiValue) {
			Where(multiValue);
			return this;
		}

		public DynSql AndBetween(string field, object rangeStart, object rangeEnd) {
			WhereBetween(field, rangeStart, rangeEnd);
			return this;
		}

		public DynSql OrderBy(string col) {
			var rec = new DOrderBy(col);
			DRecords.Add(rec);
			return this;
		}

		public DynSql OrderByRandom() {
			var rec = new DOrderBy("RAND()");
			DRecords.Add(rec);
			return this;
		}

		public DynSql LimitBy(int fromValue, int? toValue = null) {
			var rec = new DLimit(fromValue, toValue);
			DRecords.Add(rec);
			return this;
		}

		public string ToSql() {
			var sql = new StringBuilder();

			/**
				* Iterate the array on loopback for each type, that´s the most system
				* efficient and readable, don´t get confused by compiler masturbations
				* and smart array functions, they will boil down to something much
				* worse if you look behind the curtain.
				*/

			/*
			sql = ParseInsert(sql);
			sql = ParseSelect(sql);
			sql = ParseSelectAll(sql);
			sql = ParseUpdate(sql);
			sql = ParseSet(sql);
			sql = ParseFrom(sql);
			sql = ParseLeftJoin(sql);
			sql = ParseWhere(sql);
			sql = ParseAnd(sql);
			sql = ParseOrderBy(sql);
			sql = ParseLimit(sql);
			*/

			IDRecord prevRecord = null;
			IDRecord currRecord = null;
			IDRecord nextRecord = null;

			for (var i = 0; i < DRecords.Count; i++) {
				prevRecord = currRecord;

				if (nextRecord != null) {
					sql.Append(Const.SPACE);
				}

				nextRecord = i + 1 < DRecords.Count ? DRecords[i + 1] : null;

				currRecord = DRecords[i];


				if (currRecord is DSelect) {
					sql.Append((currRecord as DSelect).ToSql());
				}

				if (currRecord is DSelectAll) {
					sql.Append((currRecord as DSelectAll).ToSql());
				}

				if (currRecord is DFrom) {
					var isSecond = (prevRecord is DFrom);
					sql.Append((currRecord as DFrom).ToSql(isSecond));
				}

				if (currRecord is DWhere) {
					var isSecond = (prevRecord is DWhere);
					sql.Append((currRecord as DWhere).ToSql(isSecond));
				}

				if (currRecord is DInsert) {
					sql.Append((currRecord as DInsert).ToSql());
					break;
				}

				if (currRecord is DUpdate) {
					sql.Append((currRecord as DUpdate).ToSql());
				}

				if (currRecord is DLimit) {
					sql.Append((currRecord as DLimit).ToSql());
				}
			}

			return sql.ToString();
		}

		////////////////////////////////////////
		// SELECT


		////////////////////////////////////////
		// SELECT

		string ParseInsert(string sql) {
			foreach (var record in DRecords) {
				if (record is DInsert) {
					sql += (record as DInsert).ToSql();
				}
			}
			
			return sql;
		}

		////////////////////////////////////////
		// SELECT

		string ParseSelect(string sql) {
			foreach (var record in DRecords) {
				if (record is DSelect) {
					sql +=(record as DSelect).ToSql();
				}
			}

			return sql;
		}

		string ParseSelectAll(string sql) {
			foreach (var record in DRecords) {
				if (record is DSelectAll) {
					sql += (record as DSelectAll).ToString();
				}
			}

			return sql;			
		}


		////////////////////////////////////////
		// FROM

		string ParseFrom(string sql) {
			var localCounter = 0;

			for (var i = 0; i< DRecords.Count; i++) {
				var record = DRecords[i];

				if (record is DFrom) {
					var rec = record as DFrom;

					if (localCounter == 0) {
						sql += " FROM";
					} else {
						sql += ",";
					}

					sql += " " + rec.Table;

					if (rec.Alias != null) {
						sql += " AS " + rec.Alias;
					}

					localCounter++;
				}
			}
			return sql;
		} // parseFrom

		////////////////////////////////////////
		// UPDATE

		string ParseUpdate(string sql) {
			for (var i = 0; i< DRecords.Count; i++) {
				var record = DRecords[i];

				if (record is DUpdate) {
					var rec = record as DUpdate;
					sql += "UPDATE " + rec.Table;
					break;
				}
			}
			return sql;
		}

		////////////////////////////////////////
		// SET

		string ParseSet(string sql) {
			DSet rec;

			var localCounter = 0;

			for (var i = 0; i< DRecords.Count; i++) {
				var record = DRecords[i];

				if (record is DSet) {
					rec = record as DSet;
					if (localCounter == 0) {
						sql += " SET";
					} else {
						sql += " ,";
 					}

					sql += " " + rec.Column + "='" + this.PrepValue(rec.Value)+"'";

					localCounter++;
				}
			} // end for

			return sql;
		}

		////////////////////////////////////////
		// LEFT JOIN

		string ParseLeftJoin(string sql) {
			var localCounter = 0;

			for (var i = 0; i< DRecords.Count; i++) {
				var record = DRecords[i];

				if (record is DLeftJoin) {
					var rec = record as DLeftJoin;

					sql += " LEFT JOIN " + rec.Table + " ON " + rec.On;
				}
			}
			return sql;
		} // parseLeftJoin


		////////////////////////////////////////
		// And
		string ParseAnd(string sql) {
			for (var i = 0; i< DRecords.Count; i++) {
				var record = DRecords[i];

				if (record is DAnd) {
					var rec = record as DAnd;
					sql += " AND " + rec.Col;
					sql += " = '" + PrepValue(rec.EqualValue) + "'";

					break;
				}
			}
			return sql;
		}

		////////////////////////////////////////
		// Order
		string ParseOrderBy(string sql) {
			for (var i = 0; i< DRecords.Count; i++) {
				var record = DRecords[i];

				if (record is DOrderBy) {
					var rec = record as DOrderBy;
					sql += " ORDER BY " + rec.Col;

					break;
				}
			}
			return sql;

		} // end parseOrderBy

		////////////////////////////////////////
		// Limit

		string ParseLimit(string sql) {
			for (var i = 0; i< DRecords.Count; i++) {
				var record = DRecords[i];

				if (record is DLimit) {
					var rec = record as DLimit;
					sql += " LIMIT " + rec.FromValue;

					if (rec.ToValue != null) {
						sql += ", " + rec.ToValue;
					}

					break;
				}
			}
			return sql;
		} // end parseLimit
	}
	//////////////////////////////////////////
}
