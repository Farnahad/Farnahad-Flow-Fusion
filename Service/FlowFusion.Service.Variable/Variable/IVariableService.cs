using FlowFusion.Action.Main.Variable;
using FlowFusion.Action.Variable.RemoveItemFromListBase;
using FlowFusion.Service.Variable.Variable.Base;

namespace FlowFusion.Service.Variable.Variable;

public interface IVariableService
{
    void AddItemToList(List<object> list, object value);
    void ClearList(List<object> list);
    List<object> CreateNewList();
    int GenerateRandomNumber(int minimumValue, int maximumValue);
    VariableType GetVariableType(string value);
    List<object> MergeLists(List<object> firstList, List<object> secondList);
    List<object> RemoveDuplicateItemsFromList(List<object> listToRemoveDuplicateItemsFrom,
        bool ignoreTextCaseWhileSearchingForDuplicateItems);
    void RemoveItemFromList(RemoveItemBy removeItemBy, int atIndex, object withValue,
        bool removeAllItemOccurrences, List<object> fromList);
    void ReverseList(List<object> listToReverse);
    void ShuffleList(List<object> listToShuffle);
    void SortList(List<object> listToSort);
    List<object> SubtractLists(List<object> firstList, List<object> secondList);
    int TruncateNumber(double numberToTruncate, TruncateNumberOperation truncateNumberOperation);
}