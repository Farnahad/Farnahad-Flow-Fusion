using FlowFusion.Action.Main.Variable;
using FlowFusion.Action.Variable.RemoveItemFromListBase;
using FlowFusion.Service.Variable.Variable.Base;

namespace FlowFusion.Service.Variable.Variable;

public class VariableService : IVariableService
{
    public void AddItemToList(List<object> list, object value)
    {
        list.Add(value);
    }

    public void ClearList(List<object> list)
    {
        list.Clear();
    }

    public List<object> CreateNewList()
    {
        return new List<object>();
    }

    public int GenerateRandomNumber(int minimumValue, int maximumValue)
    {
        return new Random().Next(minimumValue, maximumValue);
    }

    public VariableType GetVariableType(string value)
    {
        if (bool.TryParse(value, out _) &&
            (value.Equals("true", StringComparison.InvariantCultureIgnoreCase) ||
             value.Equals("false", StringComparison.InvariantCultureIgnoreCase)))
        {
            return VariableType.Boolean;
        }

        if (double.TryParse(value, out _))
            return VariableType.Number;

        return VariableType.Text;
        // return VariableType.CustomObject;
        // return VariableType.DataTable;
        // return VariableType.List;
    }

    public List<object> MergeLists(List<object> firstList, List<object> secondList)
    {
        return firstList.Union(secondList).ToList();
    }

    public List<object> RemoveDuplicateItemsFromList(List<object> listToRemoveDuplicateItemsFrom,
        bool ignoreTextCaseWhileSearchingForDuplicateItems)
    {
        if (ignoreTextCaseWhileSearchingForDuplicateItems)
        {
            // ReSharper disable once PossibleNullReferenceException
            return listToRemoveDuplicateItemsFrom.GroupBy(
                item => item.ToString().ToLower()).Select(g => g.First()).ToList();
        }
        else
        {
            return listToRemoveDuplicateItemsFrom.GroupBy(
                item => item).Select(g => g.First()).ToList();
        }
    }

    public void RemoveItemFromList(RemoveItemBy removeItemBy, int atIndex,
        object withValue, bool removeAllItemOccurrences, List<object> fromList)
    {
        switch (removeItemBy)
        {
            case RemoveItemBy.Index:
                fromList.RemoveAt(atIndex);
                break;
            case RemoveItemBy.Value:
                if (removeAllItemOccurrences)
                {
                    var removeItems = fromList.Where(item => item == withValue);
                    foreach (var removeItem in removeItems)
                        fromList.Remove(removeItem);
                }
                else
                {
                    fromList.Remove(withValue);
                }
                break;
        }
    }

    public void ReverseList(List<object> listToReverse)
    {
        listToReverse.Reverse();
    }

    public void ShuffleList(List<object> listToShuffle)
    {
        var random = new Random();
        var n = listToShuffle.Count;

        for (var i = n - 1; i > 0; i--)
        {
            var j = random.Next(i + 1);

            (listToShuffle[i], listToShuffle[j]) = (listToShuffle[j], listToShuffle[i]);
        }
    }

    public void SortList(List<object> listToSort)
    {
        listToSort.Sort();
    }

    public List<object> SubtractLists(List<object> firstList, List<object> secondList)
    {
        return firstList.Except(secondList).ToList();
    }

    public int TruncateNumber(double numberToTruncate, TruncateNumberOperation truncateNumberOperation)
    {
        return truncateNumberOperation switch
        {
            TruncateNumberOperation.GetIntegerPart => (int)Math.Floor(numberToTruncate),
            TruncateNumberOperation.GetDecimalPart => (int)(numberToTruncate % 1),
            _ => 0
        };
    }
}