using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZPW.Util.Test.Samples {
    /// <summary>
    /// 测试类
    /// </summary>
    public class LambdaModelTest {

        public string Name { get; set; }
        public int Age { get; set; }
        public int? NullableInt { get; set; }
        public decimal? NullableDecimal { get; set; }
        public TestA A { get; set; }
        public class TestA {
            public int Integer { get; set; }
            public string Address { get; set; }
            public TestB B { get; set; }
            public class TestB {
                public string Name { get; set; }
            }
        }

    }
}
