using Util.Datas.EF;

namespace Unit.Datas.EF.Tests {

    /// <summary>
    /// 
    /// </summary>
    public class TestUnitOfWork : EFUnitOfWork {

        

        protected TestUnitOfWork(string connectionName) : base(connectionName) {
        }

        public TestUnitOfWork():this("") {

        }

    }
}
