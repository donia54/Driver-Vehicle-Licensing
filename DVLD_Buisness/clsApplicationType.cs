using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsApplicationType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int ApplicationID { set; get; }
        public string ApplicationTypeTitel { set; get; }
        public float ApplicationTypeFees{ set; get; }

        public clsApplicationType()

        {
            this.ApplicationID = -1;
            this.ApplicationTypeTitel = "";
            this.ApplicationTypeFees = 0;
            Mode = enMode.AddNew;

        }

        public clsApplicationType(int ApplicationID, string ApplicationTypeTitel, float ApplicationTypeFees)

        {
            this.ApplicationID = ApplicationID;
            this.ApplicationTypeTitel = ApplicationTypeTitel;
            this.ApplicationTypeFees = ApplicationTypeFees;
            Mode = enMode.Update;
        }

        private bool _AddNewApplicationType()
        {
            this.ApplicationID = clsApplicationTypeData.AddNewApplicationType(this.ApplicationTypeTitel, this.ApplicationTypeFees);
            return (this.ApplicationID != -1);
        }
        private bool _UpdateApplicationType()
        {
            return clsApplicationTypeData.UpdateApplicationType(this.ApplicationID, this.ApplicationTypeTitel, this.ApplicationTypeFees);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateApplicationType();

            }

            return false;
        }

        public static clsApplicationType Find(int ApplicationID)
        {
            string ApplicationTypeTitel = ""; float ApplicationTypeFees = 0;
            if (clsApplicationTypeData.GetApplicationTypeInfoByID((int)ApplicationID, ref ApplicationTypeTitel, ref ApplicationTypeFees))

                return new clsApplicationType(ApplicationID, ApplicationTypeTitel, ApplicationTypeFees);
            else
                return null;

        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();

        }
    }
}
