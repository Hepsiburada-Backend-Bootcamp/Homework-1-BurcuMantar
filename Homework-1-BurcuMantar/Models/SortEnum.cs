using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_1_BurcuMantar.Models
{ //This class created for sorting list by the enum class parameters at the DoctorController's getSortedDr action
    public enum SortEnum
    {
        name=1,
        lastname=2,
        gender=3,
        clinic=4,
        hospitalname=5
    }
}
