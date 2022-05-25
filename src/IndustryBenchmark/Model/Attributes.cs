using System;
namespace Benchmark
{
	[System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
	public class CompanyFundamentalsMetric : Attribute
    {
		public string Name { get; set; }
        public string Calculation { get; set; }
        public DisplayType DisplayType { get; set; }

        public CompanyFundamentalsMetric(string name)
        {
			Name = name;
			DisplayType = DisplayType.Number;
			Calculation = "";
        }
    }

	public enum DisplayType
    {
		Percent,
		Promille,
		Number,
		Thousands,
		Millions
    }
}

