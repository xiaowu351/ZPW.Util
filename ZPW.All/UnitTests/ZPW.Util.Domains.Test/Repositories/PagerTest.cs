using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZPW.Util.Domains.Repositories;

namespace ZPW.Util.Domains.Tests {
    [TestClass]
    public class PagerTest {
        private IPager _pager;

        /// <summary>
        /// 初始化
        /// </summary>
        [TestInitialize]
        public void TestInit() {
            _pager = new Pager();
        }

        /// <summary>
        /// 分页默认值
        /// </summary>
        [TestMethod]
        public void Test_Default() {
            Assert.AreEqual(1, _pager.Page);
            Assert.AreEqual(20, _pager.PageSize);
            Assert.AreEqual(0, _pager.TotalCount);
            Assert.AreEqual(0, _pager.PageCount);
        }

        #region PageCount(总页数)

        /// <summary>
        /// 总行数为0，每页20行，页数为0
        /// </summary>
        [TestMethod]
        public void TestPageCount_TotalCountIs0() {
            _pager.TotalCount = 0;
            Assert.AreEqual(0, _pager.PageCount);
        }

        /// <summary>
        /// 总行数为100，每页20行，页数为5
        /// </summary>
        [TestMethod]
        public void TestPageCount_TotalCountIs100() {
            _pager.TotalCount = 100;
            Assert.AreEqual(5, _pager.PageCount);
        }

        /// <summary>
        /// 总行数为1，每页20行，页数为1
        /// </summary>
        [TestMethod]
        public void TestPageCount_TotalCountIs1() {
            _pager.TotalCount = 1;
            Assert.AreEqual(1, _pager.PageCount);
        }

        /// <summary>
        /// 总行数为100，每页10行，页数为10
        /// </summary>
        [TestMethod]
        public void TestPageCount_PageSizeIs10_TotalCountIs100() {
            _pager.PageSize = 10;
            _pager.TotalCount = 100;
            Assert.AreEqual(10, _pager.PageCount);
        }

        #endregion

        #region Page(页索引)

        /// <summary>
        /// 页索引小于1，则修正为1
        /// </summary>
        [TestMethod]
        public void TestPage_Less1() {
            _pager.Page = 0;
            Assert.AreEqual(1, _pager.Page);

            _pager.Page = -1;
            Assert.AreEqual(1, _pager.Page);
        }

        #endregion

        #region SkipCount(跳过的行数)

        /// <summary>
        /// 跳过的行数
        /// </summary>
        [TestMethod]
        public void TestSkipCount() {
            _pager.TotalCount = 100;

            _pager.Page = 0;
            Assert.AreEqual(0, _pager.SkipCount);

            _pager.Page = 1;
            Assert.AreEqual(0, _pager.SkipCount);

            _pager.Page = 2;
            Assert.AreEqual(20, _pager.SkipCount);

            _pager.Page = 3;
            Assert.AreEqual(40, _pager.SkipCount);

            _pager.Page = 4;
            Assert.AreEqual(60, _pager.SkipCount);

            _pager.Page = 5;
            Assert.AreEqual(80, _pager.SkipCount);

            _pager.Page = 6;
            Assert.AreEqual(80, _pager.SkipCount);
        }

        /// <summary>
        /// 跳过的行数
        /// </summary>
        [TestMethod]
        public void TestSkipCount_2() {
            _pager.TotalCount = 99;

            _pager.Page = 0;
            Assert.AreEqual(0, _pager.SkipCount);

            _pager.Page = 1;
            Assert.AreEqual(0, _pager.SkipCount);

            _pager.Page = 2;
            Assert.AreEqual(20, _pager.SkipCount);

            _pager.Page = 3;
            Assert.AreEqual(40, _pager.SkipCount);

            _pager.Page = 4;
            Assert.AreEqual(60, _pager.SkipCount);

            _pager.Page = 5;
            Assert.AreEqual(80, _pager.SkipCount);

            _pager.Page = 6;
            Assert.AreEqual(80, _pager.SkipCount);
        }

        /// <summary>
        /// 跳过的行数
        /// </summary>
        [TestMethod]
        public void TestSkipCount_3() {
            _pager.TotalCount = 0;
            _pager.Page = 1;
            Assert.AreEqual(0, _pager.SkipCount);
        }

        #endregion
    }
}
