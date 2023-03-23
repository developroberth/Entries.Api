namespace Entries.Api.Models
{
    public class EntryModel
    {
        public int count { get; set; }
        public List<EntriesItemModel> entries { get; set; }
    }
}
