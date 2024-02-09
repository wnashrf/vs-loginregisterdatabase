Imports System.Data.OleDb
Imports System.Data
Public Class forgotPassword
    ReadOnly connection As New OleDbConnection(My.Settings.User_login_testConnectionString)
    Dim flag As Integer = 0

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If connection.State = ConnectionState.Closed Then
            connection.Open()
        End If

        If flag = 0 Then

            Using cmd As New OleDbCommand("SELECT COUNT(*) FROM Profile WHERE [Username] = @Username", connection)
                cmd.Parameters.AddWithValue("@Username", OleDbType.VarChar).Value = TextBox3.Text.Trim

                Dim count = Convert.ToInt32(cmd.ExecuteScalar())

                If count > 0 Then
                    flag = 1
                Else
                    MsgBox("ACCOUNT NOW FOUND.", 0, "PASSWORD RESET")
                End If

            End Using
        End If

        If flag = 1 Then
            If TextBox1.Text <> TextBox2.Text Then
                MsgBox("Password is not the same.")
                Exit Sub
            End If


            Using cmd As New OleDbCommand("UPDATE Profile SET [Password] = @Password WHERE [Username] = @Username", connection)
                cmd.Parameters.AddWithValue("@Password", OleDbType.VarChar).Value = TextBox1.Text.Trim
                cmd.Parameters.AddWithValue("@Username", OleDbType.VarChar).Value = TextBox3.Text.Trim

                If cmd.ExecuteNonQuery() Then
                    MsgBox("Password has been updated!", 0, "PASSWORD RESET")
                    flag = 0
                Else
                    MsgBox("ERROR!", 0, "PASSWORD RESET")
                End If

            End Using

        End If
        connection.Close()


    End Sub

    Private Sub forgotPassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'User_login_testDataSet.Profile' table. You can move, or remove it, as needed.
        Me.ProfileTableAdapter.Fill(Me.User_login_testDataSet.Profile)
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
    End Sub
End Class