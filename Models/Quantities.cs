using System.Collections;

namespace Waterful.Models;

public class Quantities : IEnumerable<int>
{
    public int MiniGlass { get; set; }

    public int Glass { get; set; }

    public int Bottle { get; set; }

    public IEnumerator<int> GetEnumerator()
    {
        yield return MiniGlass;
        yield return Glass;
        yield return Bottle;
    }

    public IEnumerable<Water> GetLogs(Water baseLog)
    {
        var waterTypes = Enum.GetValues(typeof(WaterType)).Cast<WaterType>().ToList();
        baseLog.Date = DateTime.Now;

        foreach (var quantity in this)
        {

            if (quantity != 0)
                yield return new Water(baseLog)
                {
                    Quantity = quantity,
                    Type = waterTypes.First()
                };

            waterTypes.RemoveAt(0);
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
