using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologyNews.Core.Enum;

namespace TechnologyNews.Core.Entity
{
    public class CoreEntity
    {
        public CoreEntity()
        {
            this.Status = Status.Active;//Şu An ki Statusu
            this.CreatedDate = DateTime.Now;//Yaratılma Tarihi
            this.CreatedUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;//O An ki windows user'ı tarafından yaratılmış
            this.CreatedComputerName = Environment.MachineName;//Local bilgisayarın NETBIOS adını alır
            this.CreatedIP = "123";
        }
        public Guid ID { get; set; }
        public Guid MasterID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedComputerName { get; set; }
        public string CreatedUserName { get; set; }
        public Guid CreatedBy { get; set; }
        public string CreatedIP { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedComputerName { get; set; }
        public string ModifiedUserName { get; set; }
        public Guid ModifiedBy { get; set; }
        public string ModifiedIP { get; set; }

        public Status Status { get; set; }
    }
}
