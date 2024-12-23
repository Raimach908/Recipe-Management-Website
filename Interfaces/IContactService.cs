using RecipeManagementApp.Data;
using System.Collections.Generic;

namespace RecipeManagementApp.Interfaces
{
    public interface IContactService
    {
        bool SaveMessage(Contacts contact);
        IEnumerable<Contacts> GetAllMessages();
        Contacts GetMessageById(int id);
    }
}
