using System;
namespace WebUI
{
	public class ChartModel
	{
        public string ElementId { get; set; }

        public ChartType Type { get; set; }

        public IEnumerable<string> Labels { get; set; }
        public IEnumerable<decimal?> Values { get; set; }
        public string ValueLabel { get; set; }

        public IEnumerable<string> LabelsForDisplay => GetDisplayLabels();
        public IEnumerable<decimal?> ValuesForDisplay => GetDisplayValues();


        private const int MAXDATAPOINTS = 30;

        private IEnumerable<string> GetDisplayLabels()
        {
            if (Labels.Count() < MAXDATAPOINTS)
                return Labels;

            return Labels.GroupInto(Labels.Count() / MAXDATAPOINTS).Select(e => e.First() + " - " + e.Last());
        }
        private IEnumerable<decimal?> GetDisplayValues()
        {
            if (Values.Count() < MAXDATAPOINTS)
                return Values;

            return Values.GroupInto(Values.Count() / MAXDATAPOINTS).Select(e => e.Average());
        }
    }

    public enum ChartType
    {
        line
    }

    public static class Extensions
    {
        public static IEnumerable<IEnumerable<T>> GroupInto<T>(
  this IEnumerable<T> source,
  int count)
        {

            using (var e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    yield return GroupIntoHelper(e, count);
                }
            }
        }

        private static IEnumerable<T> GroupIntoHelper<T>(
          IEnumerator<T> e,
          int count)
        {

            do
            {
                yield return e.Current;
                count--;
            } while (count > 0 && e.MoveNext());
        }
    }
}

