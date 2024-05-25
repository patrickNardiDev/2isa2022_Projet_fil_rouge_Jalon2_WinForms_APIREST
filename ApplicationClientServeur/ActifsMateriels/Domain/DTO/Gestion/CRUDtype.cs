using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Gestion
{
    public class CRUDtype
    {
        public enum MyType
        {
            Read,
            Add,
            Modify,
            Archive,
            Delete
        }
    }
}
