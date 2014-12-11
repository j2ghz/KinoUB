Public Class Form1

    Sub parse()
        = WebBrowser1.Document.GetElementById("prgTab").ToString
    End Sub

    Private Sub ParseToo lStripMenuItem_Click(sender As Object, e As EventArgs) Handles ParseToolStripMenuItem.Click
        parse()
    End Sub
End Class
