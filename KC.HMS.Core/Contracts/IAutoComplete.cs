namespace KC.HMS.Core.Contracts
{
    public interface IAutoComplete
    {
        Task<IList<NameValuePair>> GetAutoCompleteData(string prefix, string type);
    }
}
