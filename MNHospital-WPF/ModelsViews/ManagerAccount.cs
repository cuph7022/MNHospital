using Microsoft.EntityFrameworkCore;
using MNHospital_WPF.Models;


namespace ManagerLibrary.ModelsViews
{
    internal class ManagerAccount
    {
        private static ManagerAccount _instance = null;
        private static readonly object _instanceLock = new object();

        private ManagerAccount() { }

        public static ManagerAccount Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ManagerAccount();
                    }
                    return _instance;
                }
            }
        }


        public void AddAccount(Account s)
        {
            lock (_instanceLock)
            {
                try
                {
                    using (var context = new MnHospitalContext())
                    {
                        context.Accounts.Add(s);
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public void UpdateAccount(Account d)
        {
            lock (_instanceLock)
            {
                try
                {
                    using (var context = new MnHospitalContext())
                    {
                        context.Entry(d).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

    }
}



