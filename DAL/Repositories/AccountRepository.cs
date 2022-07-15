using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DAL.Repositories
{
    public class AccountRepository
    {
        //public AccountData GetAccountByUserId(int userId)
        //{
        //    var accountTable = _db.SelectNodes(accountNodePathXML);
        //    var accountData = new AccountData();

        //    foreach (XmlNode account in accountTable)
        //    {
        //        if ((int.Parse(account.Attributes.GetNamedItem("userId").Value!) == userId) && (int.Parse(account.Attributes.GetNamedItem("isActive").Value!) == 1))
        //        {
        //            accountData.Id = int.Parse(account.Attributes.GetNamedItem("id").Value!);
        //            accountData.UserId = int.Parse(account.Attributes.GetNamedItem("userId").Value!);
        //            accountData.Balance = decimal.Parse(account.Attributes.GetNamedItem("balance").Value!);
        //            accountData.IsActive = int.Parse(account.Attributes.GetNamedItem("isActive").Value!);
        //            accountData.OverDraft = decimal.Parse(account.Attributes.GetNamedItem("overdraft").Value!);
        //            break;
        //        }
        //    }
        //    return accountData;
        //}
    }
}
