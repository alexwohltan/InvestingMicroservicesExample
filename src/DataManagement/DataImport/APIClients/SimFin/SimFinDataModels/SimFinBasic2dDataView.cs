using System;
namespace DataImport.APIClients.SimFin
{
	public class SimFinBasic2dDataView
	{
		public IList<string> columns { get; set; }
		public IList<IList<object>> data { get; set; }

		public object this[int row, int column]
        {
			get { return data[row][column]; }
			set { data[row][column] = value; }
        }
		public int this[string columnName]
        {
			get { return columns.IndexOf(columnName); }
        }
		public IEnumerable<object> this[int columnIndex]
        {
			get
            {
				return data.Select(e => e[columnIndex]);

			}
        }
	}
}

