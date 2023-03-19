using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_IDEA_X.Interface
{
    public interface IRepo<cls,idt,st>
    {
        List<cls> Get();
        cls Get(idt id);
        cls Get(idt id,st name);
        cls Get(st name);
        bool Create(cls data);
        bool Delete(idt id);
        bool Delete(st name);
        bool Delete(idt id,st name);
        bool Update(cls data);
        bool Delete(cls data);
    }
}
