Namespace MapArbitraryDatabaseExample

    Partial Class Form1

        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (Me.components IsNot Nothing) Then
                Me.components.Dispose()
            End If

            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.session1 = New DevExpress.Xpo.Session()
            Me.gridControl1 = New DevExpress.XtraGrid.GridControl()
            Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
            Me.gridControl2 = New DevExpress.XtraGrid.GridControl()
            Me.gridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
            CType((Me.session1), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.gridControl1), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.gridView1), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.gridControl2), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.gridView2), System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' gridControl1
            ' 
            Me.gridControl1.Location = New System.Drawing.Point(0, 0)
            Me.gridControl1.MainView = Me.gridView1
            Me.gridControl1.Name = "gridControl1"
            Me.gridControl1.Size = New System.Drawing.Size(400, 328)
            Me.gridControl1.TabIndex = 0
            Me.gridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridView1})
            ' 
            ' gridView1
            ' 
            Me.gridView1.GridControl = Me.gridControl1
            Me.gridView1.Name = "gridView1"
            ' 
            ' gridControl2
            ' 
            Me.gridControl2.Location = New System.Drawing.Point(406, 0)
            Me.gridControl2.MainView = Me.gridView2
            Me.gridControl2.Name = "gridControl2"
            Me.gridControl2.Size = New System.Drawing.Size(400, 328)
            Me.gridControl2.TabIndex = 1
            Me.gridControl2.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridView2})
            ' 
            ' gridView2
            ' 
            Me.gridView2.GridControl = Me.gridControl2
            Me.gridView2.Name = "gridView2"
            Me.gridView2.OptionsView.ColumnAutoWidth = False
            ' 
            ' Form1
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(811, 332)
            Me.Controls.Add(Me.gridControl2)
            Me.Controls.Add(Me.gridControl1)
            Me.Name = "Form1"
            Me.Text = "Form1"
            CType((Me.session1), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.gridControl1), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.gridView1), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.gridControl2), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.gridView2), System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
        End Sub

#End Region
        Private session1 As DevExpress.Xpo.Session

        Private gridControl1 As DevExpress.XtraGrid.GridControl

        Private gridView1 As DevExpress.XtraGrid.Views.Grid.GridView

        Private gridControl2 As DevExpress.XtraGrid.GridControl

        Private gridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    End Class
End Namespace
