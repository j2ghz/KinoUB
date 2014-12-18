Public Class Form1

    Sub parse()
        WebBrowser1.DocumentText = "<html><head>" & WebBrowser1.Document.GetElementsByTagName("head")(0).InnerHtml & "</head><body><table>" & WebBrowser1.Document.GetElementsByTagName("table")(3).InnerHtml & "</table></body></html>"
        Dim lines As HtmlElementCollection = WebBrowser1.Document.GetElementsByTagName("tr")
        For Each line As HtmlElement In lines
            If line.GetElementsByTagName("td").Count >= 3 Then
                TextBox1.Text &= downloadfile("http://api.trakt.tv/search/movies.json/ab1ae81b7de9b30d7f169079a4c7dd08?limit=1&query=") & line.GetElementsByTagName("td")(2).InnerText & vbCrLf & vbCrLf
            End If
        Next

        ' TextBox1.Text &= "http://api.trakt.tv/search/movies.json/ab1ae81b7de9b30d7f169079a4c7dd08?limit=1&query=" & line(i).InnerHtml
    End Sub

    Private Sub ParseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ParseToolStripMenuItem.Click
        parse()
    End Sub

    Function downloadfile(path As String) As String
        Dim temp = IO.Path.GetTempFileName
        My.Computer.Network.DownloadFile(path, temp, "", "", False, 1000000, True)
        Return My.Computer.FileSystem.ReadAllText(temp)
    End Function
End Class
