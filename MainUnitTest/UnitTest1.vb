Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class UnitTest1

    <TestMethod()> Public Sub AlwayPass()
        Assert.AreEqual(True, True)
    End Sub

End Class