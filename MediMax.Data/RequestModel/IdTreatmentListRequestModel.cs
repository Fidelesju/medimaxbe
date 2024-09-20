using MediMax.Data.Enums;
using System.Runtime.InteropServices;

namespace MediMax.Data.RequestModels
{
    public class IdTreatmentListRequestModel
    {

        public List<int> Treatment_Id { get; set; }

        public int User_Id { get; set; }
    }
}
