Imports System.Data.OleDb
Imports System.Data
Public Class Form1

    ReadOnly connection As New OleDbConnection(My.Settings.User_login_testConnectionString)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = Nothing Or TextBox2.Text = Nothing Then
            MsgBox("Enter credentials!")
            Exit Sub
        End If

        If connection.State = ConnectionState.Closed Then
            connection.Open()
        End If

        Using cmd As New OleDbCommand("SELECT COUNT(*) FROM Profile WHERE Username = @Username AND Password = @Password", connection)
            cmd.Parameters.AddWithValue("@Username", OleDbType.VarChar).Value = TextBox1.Text.Trim
            cmd.Parameters.AddWithValue("@Password", OleDbType.VarChar).Value = TextBox2.Text.Trim

            Dim count = Convert.ToInt32(cmd.ExecuteScalar())

            If (count > 0) Then
                MsgBox("Login succeed", MsgBoxStyle.Information)

            Else
                MsgBox("Account not found!", MsgBoxStyle.Critical)
            End If
        End Using
        connection.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'User_login_testDataSet.Profile' table. You can move, or remove it, as needed.
        Me.ProfileTableAdapter.Fill(Me.User_login_testDataSet.Profile)

        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox8.Clear()


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If TextBox4.Text = Nothing Or TextBox3.Text = Nothing Or TextBox6.Text = Nothing Or TextBox5.Text = Nothing Then
            MsgBox("Enter credentials!")
            Exit Sub
        End If

        If TextBox8.Text = TextBox5.Text Then
            If connection.State = ConnectionState.Closed Then
                connection.Open()
            End If

            Using create As New OleDbCommand("INSERT INTO Profile([Name], [Email], [Username], [Password]) VALUES(@Name, @Email, @Username, @Password)", connection)
                create.Parameters.AddWithValue("@Name", OleDbType.VarChar).Value = TextBox4.Text.Trim
                create.Parameters.AddWithValue("@Email", OleDbType.VarChar).Value = TextBox3.Text.Trim
                create.Parameters.AddWithValue("@Username", OleDbType.VarChar).Value = TextBox6.Text.Trim
                create.Parameters.AddWithValue("@Password", OleDbType.VarChar).Value = TextBox5.Text.Trim

                If create.ExecuteNonQuery Then
                    MsgBox("Account Created!")
                Else
                    MsgBox("Error!")
                End If
            End Using
            connection.Close()
        End If


    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        forgotPassword.Show()
        Me.Hide()
    End Sub
End Class
