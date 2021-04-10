////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ProgramTests
{
  public class AlphabeticalTestOrderer : ITestCaseOrderer
  {
    #region public members

    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(
      IEnumerable<TTestCase> testCases)
      where TTestCase : ITestCase
    {
      List<TTestCase> testsList = testCases.ToList();

      testsList.Sort((x, y) =>
        StringComparer.OrdinalIgnoreCase.Compare(
          x.TestMethod.Method.Name,
          y.TestMethod.Method.Name));

      return testsList;
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
