using System;
namespace DataImport.APIClients.SimFin;

	public class SimFinBasicDataView
	{
		public IList<string> columns { get; set; }
		public IList<object> data { get; set; }

		public object this[int i]
		{
			get { return data[i]; }
			set { data[i] = value; }
		}
		public int this[string columnName]
		{
			get { return columns.IndexOf(columnName); }
		}
	}

